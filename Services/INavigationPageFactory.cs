using System;
using Xamarin.Forms;
namespace FormsUtils.Services
{
    public interface INavigationPageFactory
    {
        NavigationPage CreateNavigationPage(Page rootPage);
    }
}
