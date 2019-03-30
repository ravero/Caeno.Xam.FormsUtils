using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace FormsUtils.UI.Behaviors
{
    public class ListViewSelectedItemBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command),
                typeof(ICommand),
                typeof(ListViewSelectedItemBehavior),
                null);

        public ICommand Command {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ListView AssociatedObject { get; private set; }

        protected override void OnAttachedTo(ListView bindable) {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;;
            bindable.ItemSelected += OnItemSelected;
        }

        protected override void OnDetachingFrom(ListView bindable) {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.ItemSelected -= OnItemSelected;
            AssociatedObject = null;
        }

        void OnBindingContextChanged(object sender, EventArgs e) => OnBindingContextChanged();

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e) {
            if (Command == null || e.SelectedItem == null)
                return;

            if (Command.CanExecute(e.SelectedItem)) {
                AssociatedObject.SelectedItem = null;
                Command.Execute(e.SelectedItem);
            }
        }

        protected override void OnBindingContextChanged() {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}
