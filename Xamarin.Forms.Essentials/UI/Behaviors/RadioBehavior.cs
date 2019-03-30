using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace FormsUtils.UI.Behaviors
{
    public class RadioBehavior : Behavior<View>
    {
        static List<RadioBehavior> defaultGroup = new List<RadioBehavior>();
        static Dictionary<string, List<RadioBehavior>> radioGroups = new Dictionary<string, List<RadioBehavior>>();

        TapGestureRecognizer tapRecognizer;
        View associatedObject;

        #region Properties

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create("IsChecked",
                                    typeof(bool),
                                    typeof(RadioBehavior),
                                    false,
                                    BindingMode.TwoWay,
                                    propertyChanged: OnIsCheckedChanged);

        public bool IsChecked {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue) {
            var behavior = (RadioBehavior)bindable;

            if ((bool)newValue) {
                var groupName = behavior.GroupName;
                List<RadioBehavior> behaviors = null;

                if (string.IsNullOrEmpty(groupName))
                    behaviors = defaultGroup;
                else
                    behaviors = radioGroups[groupName];

                foreach (var otherBehavior in behaviors) {
                    if (otherBehavior != behavior)
                        otherBehavior.IsChecked = false;
                }
            }

            var eventArgs = new RadioCheckedChangedEventArgs(behavior.IsChecked, behavior.Value);
            behavior.CheckedChanged?.Invoke(behavior, eventArgs);

            if (behavior.CheckedChangedCommand?.CanExecute(eventArgs) ?? false)
                behavior.CheckedChangedCommand.Execute(eventArgs);
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(
                nameof(Value),
                typeof(object),
                typeof(RadioBehavior),
                null,
                BindingMode.TwoWay);

        public object Value {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly BindableProperty GroupNameProperty =
            BindableProperty.Create(nameof(GroupName),
                typeof(string),
                typeof(RadioBehavior),
                null,
                BindingMode.TwoWay,
                propertyChanged: OnGroupNameChanged);

        public string GroupName {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        static void OnGroupNameChanged(BindableObject bindable, object oldValue, object newValue) {
            var behavior = (RadioBehavior)bindable;
            string oldGroupName = (string)oldValue;
            string newGroupName = (string)newValue;

            if (string.IsNullOrEmpty(oldGroupName)) {
                // Remove the Behavior from the default group
                defaultGroup.Remove(behavior);
            } else {
                // Remove the RadioBehavior from the radioGroups collection.
                var behaviors = radioGroups[oldGroupName];
                behaviors.Remove(behavior);

                // Get rid of the collection if it's empty.
                if (behaviors.Count == 0)
                    radioGroups.Remove(oldGroupName);
            }

            if (string.IsNullOrEmpty(newGroupName)) {
                // Add the new Behavior to the default group.
                defaultGroup.Add(behavior);
            } else {
                List<RadioBehavior> behaviors = null;

                if (radioGroups.ContainsKey(newGroupName)) {
                    // Get the named group.
                    behaviors = radioGroups[newGroupName];
                } else {
                    // If that group doesn't exist, create it.
                    behaviors = new List<RadioBehavior>();
                    radioGroups.Add(newGroupName, behaviors);
                }

                // Add the Behavior to the group.
                behaviors.Add(behavior);
            }
        }

        public event EventHandler<RadioCheckedChangedEventArgs> CheckedChanged;

        public static readonly BindableProperty CheckedChangedCommandProperty =
            BindableProperty.Create(nameof(CheckedChangedCommand),
                typeof(ICommand),
                typeof(RadioBehavior),
                null,
                propertyChanged: (b, o, n) => {
                    Console.WriteLine(n);
                });

        public ICommand CheckedChangedCommand {
            get => (ICommand)GetValue(CheckedChangedCommandProperty);
            set => SetValue(CheckedChangedCommandProperty, value);
        }

        #endregion

        public RadioBehavior() {
            defaultGroup.Add(this);
        }

        protected override void OnAttachedTo(View bindable) {
            base.OnAttachedTo(bindable);
            associatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;

            if (bindable is Button) {
                ((Button)bindable).Clicked += ItemSelectedHandler;
            } else {
                tapRecognizer = new TapGestureRecognizer();
                tapRecognizer.Tapped += ItemSelectedHandler;
                bindable.GestureRecognizers.Add(tapRecognizer);
            }
        }

        protected override void OnDetachingFrom(View bindable) {
            base.OnDetachingFrom(bindable);

            if (bindable is Button) {
                ((Button)bindable).Clicked -= ItemSelectedHandler;
            } else {
                bindable.GestureRecognizers.Remove(tapRecognizer);
                tapRecognizer.Tapped -= ItemSelectedHandler;
            }

            bindable.BindingContextChanged -= OnBindingContextChanged;
            associatedObject = null;
        }

        void ItemSelectedHandler(object sender, EventArgs args) {
            IsChecked = true;
        }

        void OnBindingContextChanged(object sender, EventArgs e) => OnBindingContextChanged();

        protected override void OnBindingContextChanged() {
            base.OnBindingContextChanged();
            BindingContext = associatedObject.BindingContext;
        }
    }

    public class RadioCheckedChangedEventArgs : EventArgs
    {
        public bool IsChecked { get; }

        public object Value { get; }

        public RadioCheckedChangedEventArgs(bool isChecked, object value) {
            IsChecked = isChecked;
            Value = value;
        }
    }
}
