using System;
using Xamarin.Forms;

namespace FormsUtils.UI.Controls
{
    public class ActivityLayoutTemplate : Grid
    {
        public ActivityLayoutTemplate() {
            var contentPresenter = new ContentPresenter();
            Children.Add(contentPresenter);

            // Create the Activity Indicator
            var activityIndicator = new ActivityIndicator();
            activityIndicator.SetBinding(VisualProperty, new TemplateBinding("IndicatorVisual"));
            activityIndicator.SetBinding(ActivityIndicator.ColorProperty, new TemplateBinding("IndicatorColor"));
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, new TemplateBinding("BindingContext.IsProcessing"));

            // Create the container for the activity indicator
            var activityIndicatorContainer = new Grid();
            activityIndicatorContainer.SetBinding(BackgroundColorProperty, new TemplateBinding("FadedBackgroundColor"));
            activityIndicatorContainer.SetBinding(IsVisibleProperty, new TemplateBinding("BindingContext.IsProcessing"));
            activityIndicatorContainer.Children.Add(activityIndicator);
            Children.Add(activityIndicatorContainer);
        }
    }
}
