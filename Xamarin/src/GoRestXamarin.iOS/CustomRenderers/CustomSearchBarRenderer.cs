using System;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(GoRestXamarin.iOS.CustomRenderers.CustomSearchBarRenderer))]
namespace GoRestXamarin.iOS.CustomRenderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {

        public override void Draw(CGRect rect)
        {
            var csb = (SearchBar)Element;
           
            Control.BarTintColor = Color.FromHex("#F0F0F0").ToUIColor();
            Control.BarStyle = UIBarStyle.Default;
            Control.SearchBarStyle = UISearchBarStyle.Minimal;
            Control.BarTintColor = Color.FromHex("#F0F0F0").ToUIColor();
            Control.BackgroundColor = Color.FromHex("#F0F0F0").ToUIColor();
            Control.ShowsCancelButton = false;
            base.Draw(rect);
        }

       
    }
}

