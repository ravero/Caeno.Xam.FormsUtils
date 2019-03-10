using System;
using System.Threading.Tasks;
using FormsUtils.UI.ViewModels;
using Rg.Plugins.Popup.Pages;

namespace FormsUtils.UI.Pages
{
    public class PopupPageBase : PopupPage
    {
        protected override void OnAppearing() {
            if (BindingContext is ViewModelBase viewModel) {
                viewModel.AppearingAction?.Invoke();
                if (viewModel.AppearingTask != null)
                    Task.Run(viewModel.AppearingTask);
            }

            base.OnAppearing();
        }

        protected override void OnDisappearing() {
            if (BindingContext is ViewModelBase viewModel) {
                viewModel.DisappearingAction?.Invoke();
                if (viewModel.DisappearingTask != null)
                    Task.Run(viewModel.DisappearingTask);
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
