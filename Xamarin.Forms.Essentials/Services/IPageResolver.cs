using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsUtils.Services
{
    public interface IPageResolver
    {
        Page Resolve<TPage>() where TPage : Page;

        Page Resolve<TPage, TArgs>(TArgs arguments) where TPage : Page;
    }
}
