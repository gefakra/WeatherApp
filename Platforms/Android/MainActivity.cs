using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.FirebasePushNotification;
using AndroidIntent = Android.Content.Intent;

namespace WeatherApp
{
    [Activity(
    Label = "WeatherApp",
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize,
    LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
    Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable },
    DataScheme = "yourappscheme")] // не обязательно, но для deep links

public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //FirebasePushNotificationManager.Initialize(this, true);
            FirebasePushNotificationManager.ProcessIntent(this, Intent, false);
        }

    protected override void OnNewIntent(AndroidIntent intent)
    {
        base.OnNewIntent(intent);
        FirebasePushNotificationManager.ProcessIntent(this, intent, false);
    }
}
}
