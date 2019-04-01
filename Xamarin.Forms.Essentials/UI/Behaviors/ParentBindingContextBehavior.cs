using System;
using Xamarin.Forms;

namespace FormsUtils.UI.Behaviors
{
    public abstract class ParentBindingContextBehavior<T> : Behavior<T> where T : BindableObject
    {
        BindableObject associatedObject;

        protected override void OnAttachedTo(T bindable) {
            base.OnAttachedTo(bindable);
            associatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(T bindable) {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            associatedObject = null;
        }

        void OnBindingContextChanged(object sender, EventArgs e) => OnBindingContextChanged();

        protected override void OnBindingContextChanged() {
            base.OnBindingContextChanged();
            BindingContext = associatedObject.BindingContext;
        }
    }
}
