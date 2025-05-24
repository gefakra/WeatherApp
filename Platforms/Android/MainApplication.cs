using Android.App;
using Android.Runtime;
using Plugin.FirebasePushNotification;

namespace WeatherApp;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    { }

    public override void OnCreate()
    {
        base.OnCreate();

        // Инициализируем плагин при старте приложения:
        FirebasePushNotificationManager.Initialize(this, true);

        // (опционально) сразу подпишитесь на событие, чтобы отладить получение токена:
        CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
        {
            Android.Util.Log.Debug("FCM", $"Token (init): {p.Token}");
        };
    }

    protected override MauiApp CreateMauiApp() =>
        MauiProgram.CreateMauiApp();
}

