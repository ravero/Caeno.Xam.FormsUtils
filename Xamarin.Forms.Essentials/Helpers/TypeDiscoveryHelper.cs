using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace FormsUtils.Helpers
{
    public static class TypeDiscoveryHelper
    {
        /// <summary>
        /// Retrieve a Enumerable of Types representing Xamarin.Forms Pages.
        /// </summary>
        /// <returns>The page types.</returns>
        /// <param name="assembly">The Assembly to Extract the Types.</param>
        /// <remarks>
        /// This method is based on the convetions set by Caeno to define Pages in
        ///     Xamarin.Forms assembly:
        /// 
        ///     - Page Types should Ends With "Page" suffix.
        /// </remarks>
        public static IEnumerable<Type> GetPageTypes(this Assembly assembly) => 
            assembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Page", StringComparison.Ordinal));

        /// <summary>
        /// Create a Dictionary that relates Page Types to its View Model Types
        ///     based on the conventions set by Caeno.
        /// </summary>
        /// <returns>The pages to models.</returns>
        /// <param name="assembly">The Assembly to Extract the Types.</param>
        /// <param name="pageTypes">A Enumeration of the Page Types that the related View Model will be seek.</param>
        /// <param name="viewModelsNamespace">The Namespace where the View Models are declared.</param>
        public static Dictionary<Type, Type> CreatePagesToModels(this Assembly assembly, IEnumerable<Type> pageTypes, string viewModelsNamespace) {
            var result = new Dictionary<Type, Type>();

            pageTypes
                .Select(t => new {
                    PageType = t,
                    ViewModelType = GetViewModelOfPage(assembly, t, viewModelsNamespace),
                })
                .Where(i => i.ViewModelType != null)
                .ToList()
                .ForEach(i => result[i.PageType] = i.ViewModelType);

            return result;
        }

        public static Type GetViewModelOfPage(Assembly assembly, Type page, string viewModelsNamespace) {
            var baseName = page.Name.Substring(0, page.Name.IndexOf("Page", StringComparison.Ordinal));
            var viewModelName = $"{viewModelsNamespace}.{baseName}ViewModel";
            return assembly.GetType(viewModelName);
        }
    }
}
