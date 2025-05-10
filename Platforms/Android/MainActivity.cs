using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Firebase.Messaging;
using Firebase;
using Android.Util;
using Android.Gms.Extensions;


namespace WeatherApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            FirebaseApp.InitializeApp(this);

            FirebaseMessaging.Instance.SubscribeToTopic("weather-updates");
            GetFirebaseTokenAsync();
        }
    

         private async void GetFirebaseTokenAsync()
            {
                 var token = await FirebaseMessaging.Instance.GetToken();
                 Console.WriteLine($"Device token: {token}");
            }
    }
}
