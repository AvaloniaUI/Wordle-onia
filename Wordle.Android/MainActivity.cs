using Android.App;
using Android.OS;
using Android.Content.PM;
using Avalonia.Android;

namespace Wordle.Android
{
    [Activity(Label = "Wordle.Android", Theme = "@style/MyTheme.NoActionBar", Icon = "@drawable/icon", LaunchMode = LaunchMode.SingleInstance, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : AvaloniaActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Content = new MainView();
        }
    }
}
