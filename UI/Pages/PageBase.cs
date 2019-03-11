using System;

using Xamarin.Forms;
using FormsUtils.UI.ViewModels;
using System.Threading.Tasks;

namespace FormsUtils.UI.Pages
{
    /// <summary>
    /// A base class for Content Pages, to be used with ViewModelBase BindingContext's
    ///     to allow decoupled page lifetime event proxying.
    /// </summary>
    public abstract class PageBase : ContentPage
    {
        protected override async void OnAppearing() {
            if (BindingContext is ViewModelBase viewModel) {
                viewModel.AppearingAction?.Invoke();
                if (viewModel.AppearingTask != null)
                    await viewModel.AppearingTask();
            }

            base.OnAppearing();
        }

        protected override async void OnDisappearing() {
            if (BindingContext is ViewModelBase viewModel) {
                viewModel.DisappearingAction?.Invoke();
                if (viewModel.DisappearingTask != null)
                    await viewModel.DisappearingTask();
            }
                
            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed() {
            if (BindingContext is ViewModelBase viewModel)
                return viewModel.OnBackPressed();

            return base.OnBackButtonPressed();
        }
    }
}

