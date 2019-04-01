using System;
using FormsUtils.Services;
using Xamarin.Forms;

namespace FormsUtils.Components
{
    public class DefaultPageNavigationFactory : INavigationPageFactory
    {
        public NavigationPage CreateNavigationPage(Page rootPage) => new NavigationPage(rootPage);
    }
}
