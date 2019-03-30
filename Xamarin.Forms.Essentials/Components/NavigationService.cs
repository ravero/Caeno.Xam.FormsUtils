using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using FormsUtils.Services;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Contracts;

namespace FormsUtils.Components
{
    /// <summary>
    /// Base implementation of the Navigation Service.
    /// </summary>
    public class NavigationService : INavigationService
    {
        readonly INavigation navigation;
        readonly IPopupNavigation popupNavigation;
        readonly IPageResolver pageResolver;
        readonly INavigationPageFactory navigationPageFactory;

        public NavigationService(INavigation navigation, 
                IPopupNavigation popupNavigation,
                IPageResolver pageResolver, 
                INavigationPageFactory navigationPageFactory) {
            this.navigation = navigation;
            this.popupNavigation = popupNavigation;
            this.pageResolver = pageResolver;
            this.navigationPageFactory = navigationPageFactory;
        }

        public async Task PresentAsync<TPage>() where TPage : Page {
            var page = pageResolver.Resolve<TPage>();
            await navigation.PushAsync(page);
        }

        public async Task PresentAsync<TPage, TArgs>(TArgs args) where TPage : Page {
            var page = pageResolver.Resolve<TPage, TArgs>(args);
            await navigation.PushAsync(page);
        }

        public async Task PresentModalAsync<TPage>(bool addToNavigationPage) where TPage : Page {
            var page = pageResolver.Resolve<TPage>();
            if (addToNavigationPage)
                await navigation.PushModalAsync(navigationPageFactory.CreateNavigationPage(page));
            else
                await navigation.PushModalAsync(page);
        }

        public async Task PresentModalAsync<TPage, TArgs>(TArgs args, bool addToNavigationPage = false) where TPage : Page {
            var page = pageResolver.Resolve<TPage, TArgs>(args);
            if (addToNavigationPage)
                await navigation.PushModalAsync(navigationPageFactory.CreateNavigationPage(page));
            else
                await navigation.PushModalAsync(page);
        }

        public async Task DismissAsync() => await navigation.PopAsync();

        public async Task DismissModalAsync() => await navigation.PopModalAsync();

        public async Task PresentPopupAsync<TPage>() where TPage : PopupPage {
            var page = (PopupPage)pageResolver.Resolve<TPage>();
            await popupNavigation.PushAsync(page, true);
        }

        public async Task PresentPopupAsync<TPage, TArgs>(TArgs args) where TPage : PopupPage {
            var page = (PopupPage)pageResolver.Resolve<TPage, TArgs>(args);
            await popupNavigation.PushAsync(page, true);
        }

        public async Task DismissPopupAsync() => await popupNavigation.PopAsync();
    }
}
