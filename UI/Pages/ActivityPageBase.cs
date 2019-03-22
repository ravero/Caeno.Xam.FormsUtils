using System;
using Xamarin.Forms;
using FormsUtils.UI.Controls;
namespace FormsUtils.UI.Pages
{
    public class ActivityPageBase : PageBase
    {
        static readonly ControlTemplate contentTemplate = new ControlTemplate(typeof(ActivityLayoutTemplate));

        public static readonly BindableProperty FadedBackgroundColorProperty =
            BindableProperty.Create(nameof(FadedBackgroundColor),
                typeof(Color),
                typeof(ActivityPageBase),
                Color.FromHex("#80000000"));

        public Color FadedBackgroundColor {
            get => (Color)GetValue(FadedBackgroundColorProperty);
            set => SetValue(FadedBackgroundColorProperty, value);
        }

        public static readonly BindableProperty IndicatorColorProperty =
            BindableProperty.Create(nameof(IndicatorColor),
                typeof(Color),
                typeof(ActivityPageBase),
                Color.Black);

        public Color IndicatorColor {
            get => (Color)GetValue(IndicatorColorProperty);
            set => SetValue(IndicatorColorProperty, value);
        }

        public static readonly BindableProperty IndicatorVisualProperty =
            BindableProperty.Create(nameof(IndicatorVisual),
                typeof(IVisual),
                typeof(ActivityPageBase),
                VisualMarker.Material);

        public IVisual IndicatorVisual {
            get => (IVisual)GetValue(IndicatorVisualProperty);
            set => SetValue(IndicatorVisualProperty, value);
        }

        public ActivityPageBase() : base() {
            ControlTemplate = contentTemplate;
        }
    }
}
