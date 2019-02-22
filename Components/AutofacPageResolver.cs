﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Autofac;
using FormsUtils.Services;
using FormsUtils.Components;

namespace FazendaDigital.Components
{
    /// <summary>
    /// An Implementation of the Page Resolver using Autofac DI Container.
    /// </summary>
    public class AutofacPageResolver : IPageResolver
    {
        readonly IContainer container;
        readonly Dictionary<Type, Type> pagesToModels;

        public AutofacPageResolver(IContainer container, Dictionary<Type, Type> pagesToModels) {
            this.container = container;
            this.pagesToModels = pagesToModels;
        }

        public Page Resolve<TPage>() where TPage : Page {
            using (var scope = container.BeginLifetimeScope()) {
                var pageT = typeof(TPage);
                var page = (TPage)scope.Resolve(pageT, new TypedParameter(typeof(IPageResolver), this));

                if (pagesToModels.ContainsKey(pageT))
                    page.BindingContext = ResolveModel(scope, page);

                return page;
            }
        }

        public Page Resolve<TPage, TArgs>(TArgs arguments) where TPage : Page {
            using (var scope = container.BeginLifetimeScope()) {
                var pageT = typeof(TPage);
                var page = (TPage)scope.Resolve(pageT, new TypedParameter(typeof(IPageResolver), this));

                if (pagesToModels.ContainsKey(pageT))
                    page.BindingContext = ResolveModel(scope, page, arguments);

                return page;
            }
        }

        object ResolveModel<TPage>(ILifetimeScope scope, TPage page) where TPage : Page {
            var navigationService = new NavigationService(page.Navigation, this);
            var viewModelT = pagesToModels[typeof(TPage)];
            var viewModel = scope.Resolve(viewModelT,
                new TypedParameter(typeof(INavigationService), navigationService),
                new TypedParameter(typeof(IPageResolver), this));

            return viewModel;
        }

        object ResolveModel<TPage, TArgs>(ILifetimeScope scope, TPage page, TArgs args) where TPage : Page {
            var navigationService = new NavigationService(page.Navigation, this);
            var viewModelT = pagesToModels[typeof(TPage)];
            var viewModel = scope.Resolve(viewModelT,
                new TypedParameter(typeof(INavigationService), navigationService),
                new TypedParameter(typeof(IPageResolver), this),
                new TypedParameter(typeof(TArgs), args));

            return viewModel;
        }
    }
}