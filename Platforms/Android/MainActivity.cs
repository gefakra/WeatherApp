using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Firebase.Messaging;
using Firebase;
using Android.Util;
using Plugin.Firebase.Core.Platforms.Android;

namespace WeatherApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            FirebaseApp.InitializeApp(this);

            FirebaseMessaging.Instance.GetToken()
                .AddOnCompleteListener(new OnCompleteListener());
        }
    }

    public class OnCompleteListener : Java.Lang.Object, Android.Gms.Tasks.IOnCompleteListener
    {
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                string token = task.Result?.ToString();
                Log.Debug("FCM", "FCM Token: " + token);

                // Здесь ты можешь сохранить токен в Preferences или передать в C# MAUI код
            }
            else
            {
                Log.Warn("FCM", "Не удалось получить токен");
            }
        }
    }
}
