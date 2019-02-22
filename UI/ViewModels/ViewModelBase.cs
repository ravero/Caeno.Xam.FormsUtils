﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FormsUtils.Abstractions;
using FormsUtils.Models;
using Xamarin.Forms;

namespace FormsUtils.UI.ViewModels
{
    /// <summary>
    /// A base class for View Models.
    /// </summary>
    public abstract class ViewModelBase : ObservableBase, IOperationContainer
    {
        IProgressDialog progressDialog;

        protected bool IsRunningProcessing { get; private set; }

        string _loadingTitle = "Carregando";
        public string LoadingTitle {
            get => _loadingTitle;
            set => SetProperty(ref _loadingTitle, value);
        }

        public virtual ICommand AppearingCommand => new Command(() => { }, () => false);

        public virtual ICommand DisappearingCommand => new Command(() => { }, () => false);

        public ViewModelBase() { }

        protected async Task Process(Func<Task> action, string loadingTitle = null) {
            loadingTitle = loadingTitle ?? LoadingTitle;
            using (var progress = UserDialogs.Instance.Loading(loadingTitle)) {
                await action();
            }
        }

        protected async Task<T> ProcessWithResult<T>(Func<Task<T>> action, string loadingTitle = null) {
            loadingTitle = loadingTitle ?? LoadingTitle;
            T result;
            using (var progress = UserDialogs.Instance.Loading(loadingTitle)) {
                result = await action();
            }
            return result;
        }

        protected async Task DisplayAlert(string title, string message, string cancel = "OK") {
            await UserDialogs.Instance.AlertAsync(message, title, cancel);
        }

        protected async Task<bool> DisplayConfirm(string title, string message, string ok, string cancel) {
            var confirmConfig = new ConfirmConfig {
                Title = title,
                Message = message,
                OkText = ok,
                CancelText = cancel,
            };

            var result = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
            return result;
        }


        #region IOperationContainer Implementation

        public bool TryStartProcessing() {
            if (progressDialog != null)
                return false;

            progressDialog = UserDialogs.Instance.Loading(LoadingTitle);
            return true;
        }

        public void StartProcessing() {
            if (progressDialog != null)
                StopProcessing();

            progressDialog = UserDialogs.Instance.Loading(LoadingTitle);
        }

        public void StopProcessing() {
            progressDialog?.Dispose();
            progressDialog = null;
        }

        #endregion
    }
}