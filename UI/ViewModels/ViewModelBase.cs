using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FormsUtils.Abstractions;
using FormsUtils.Models;
using FormsUtils.Validatables;
using Xamarin.Forms;

namespace FormsUtils.UI.ViewModels
{
    /// <summary>
    /// A base class for View Models.
    /// </summary>
    public abstract class ViewModelBase : ObservableBase, IOperationContainer
    {
        IProgressDialog progressDialog;

        bool _isProcessing;
        public bool IsProcessing {
            get => _isProcessing;
            set => SetProperty(ref _isProcessing, value);
        }

        string _loadingTitle = "Carregando";
        public string LoadingTitle {
            get => _loadingTitle;
            set => SetProperty(ref _loadingTitle, value);
        }

        /// <summary>
        /// An Action to be executed synchronously on the OnAppearing event of the page lifecycle.
        /// Override it to provide the Action implementation.
        /// </summary>
        public virtual Action AppearingAction { get; }

        /// <summary>
        /// A Task to be executed asynchronously on the OnAppearing event of the page lifecycle.
        /// Override it to provide the Task implementation.
        /// </summary>
        public virtual Func<Task> AppearingTask { get; }

        /// <summary>
        /// An Action to be executed synchronously on the OnDisappearing event of the page lifecycle.
        /// Override it to provide the Action implementation.
        /// </summary>
        public virtual Action DisappearingAction { get; }

        /// <summary>
        /// A Task to be executed asynchronously on the OnDisappearing event of the page lifecycle.
        /// Override it to provide the Task implementation.
        /// </summary>
        public virtual Func<Task> DisappearingTask { get; }

        /// <summary>
        /// Allow the default behavior of the Back Button to be overriden.
        /// </summary>
        /// <returns>
        ///     <c>true</c> to let the operating system handle the Back behavior, 
        ///     <c>false</c> otherwise.
        /// </returns>
        public virtual bool OnBackPressed() => true;

        protected async Task Process(Func<Task> action, bool isShowDialog = true, string loadingTitle = null) {
            loadingTitle = loadingTitle ?? LoadingTitle;
            if (isShowDialog) {
                using (var progress = UserDialogs.Instance.Loading(loadingTitle)) {
                    IsProcessing = true;
                    await action();
                    IsProcessing = false;
                }
            } else {
                IsProcessing = true;
                await action();
                IsProcessing = false;
            }
        }

        protected async Task<T> ProcessWithResult<T>(Func<Task<T>> action, string loadingTitle = null) {
            loadingTitle = loadingTitle ?? LoadingTitle;
            T result;
            using (var progress = UserDialogs.Instance.Loading(loadingTitle)) {
                IsProcessing = true;
                result = await action();
                IsProcessing = false;
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

        protected async Task<string> DisplayActionSheet(string title, string cancel, string destructive, params string[] options) => 
            await UserDialogs.Instance.ActionSheetAsync(title, cancel, destructive, null, options);

        #region Validation

        /// <summary>
        /// Applies validation to this View model instance, based on the
        ///     attributes declared on its properties.
        /// </summary>
        /// <returns>True if this View Model is Valid.</returns>
        protected virtual async Task<bool> Validate(string alertTitle = "Atenção") {
            //
            // 1. Get Properties with validation attributes
            var thisType = GetType();
            var validatableProps = thisType.GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(RequiredAttribute)));

            //
            // 2. Check each value
            foreach (var prop in validatableProps) {
                var value = (string)prop.GetValue(this);
                if (string.IsNullOrWhiteSpace(value)) {
                    var attribute = (RequiredAttribute)prop.GetCustomAttribute(typeof(RequiredAttribute));
                    await DisplayAlert(alertTitle, attribute.ErrorMessage);
                    return false;
                }
            }

            //
            // 3. If it get here then it's valid
            return true;
        }

        protected virtual bool CheckValidatables() {
            var thisType = GetType();
            var validatableType = typeof(Validatable<>);
            var validatableProps = thisType.GetProperties()
                .Where(prop => IsSubclassOfOpenGeneric(validatableType, prop.PropertyType));

            var result = true;
            foreach (var prop in validatableProps) {
                var @object = prop.GetValue(this);
                var validateMethod = prop.PropertyType.GetMethod(nameof(Validatable<object>.Validate));
                validateMethod.Invoke(@object, null);

                var isValidProperty = prop.PropertyType.GetProperty(nameof(Validatable<object>.IsValid));
                result = (bool)isValidProperty.GetValue(@object) && result;
            }
            return result;
        }

        static bool IsSubclassOfOpenGeneric(Type generic, Type toCheck) {
            while (toCheck != null && toCheck != typeof(object)) {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                    return true;

                toCheck = toCheck.BaseType;
            }
            return false;
        }

        #endregion


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
