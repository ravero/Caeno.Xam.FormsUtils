# Dependencies
This document describe the dependencies of the project and reason why they're added.

## NuGet Dependencies
* **Xamarin.Forms**: Since this library is intended to be essential extensions for Xamarin.Forms projects, this is the basic and self-explained dependency.
* **Autofac**: Autofac is used on the default implementation of `IPageResolver`.
* **Acr.UserDialogs**: This library is used to present User Dialogs from `ViewModelBase` class.
* **System.ComponentModel.Annotations**: This library is used to provided built-in Validation on `ViewModelBase` based on default attributes such as `RequiredAttribute`.
* **Rg.Plugins.Popup**: This library adds support to presenting Pages as Popups.