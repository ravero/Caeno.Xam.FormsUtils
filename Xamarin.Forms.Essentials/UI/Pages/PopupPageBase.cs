using System;
using System.Threading.Tasks;
using FormsUtils.UI.ViewModels;
using Rg.Plugins.Popup.Pages;

namespace FormsUtils.UI.Pages
{
    public class PopupPageBase : PopupPage
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
