using System;

using Xamarin.Forms;
using FormsUtils.UI.ViewModels;

namespace FormsUtils.UI.Pages
{
    /// <summary>
    /// A base class for Content Pages, to be used with ViewModelBase BindingContext's
    ///     to allow decoupled page lifetime event proxying.
    /// </summary>
    public abstract class PageBase : ContentPage
    {
        protected override void OnAppearing() {
            if (BindingContext is ViewModelBase viewModel &&
                viewModel.AppearingCommand.CanExecute(null)) 
                viewModel.AppearingCommand.Execute(null);

            base.OnAppearing();
        }

        protected override void OnDisappearing() {
            if (BindingContext is ViewModelBase viewModel &&
                viewModel.DisappearingCommand.CanExecute(null))
                viewModel.DisappearingCommand.Execute(null);

            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed() {
            if (BindingContext is ViewModelBase viewModel)
                return viewModel.OnBackPressed();

            return base.OnBackButtonPressed();
        }
    }
}

