using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using FormsUtils.Services;

namespace FormsUtils.Components
{
    /// <summary>
    /// Base implementation of the Navigation Service.
    /// </summary>
    public class NavigationService : INavigationService
    {
        readonly INavigation navigation;
        readonly IPageResolver pageResolver;
        readonly INavigationPageFactory navigationPageFactory;

        public NavigationService(INavigation navigation, IPageResolver pageResolver, INavigationPageFactory navigationPageFactory) {
            this.navigation = navigation;
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

        public async Task DismissAsync() {
            await navigation.PopAsync();
        }

        public async Task DismissModalAsync() {
            await navigation.PopModalAsync();
        }
    }
}
