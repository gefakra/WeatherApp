using WeatherApp.Resources.Styles;
using WeatherApp.Services;
using Plugin.FirebasePushNotification;


namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Устанавливаем начальную тему в зависимости от системной темы
            SetTheme(Application.Current.RequestedTheme);

            // Подписываемся на событие изменения темы
            Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;

            MainPage = new AppShell();

            //var token = CrossFirebasePushNotification.Current.Token;
           // Console.WriteLine($"Firebase token: {token}");

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                var token = p.Token;
                // Console.WriteLine($"New FCM Token: {newToken}");

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Current.MainPage.DisplayAlert("New FCM Token",token, "OK");
                });
            };

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Current.MainPage.DisplayAlert("Push!", "Получено уведомление", "OK");
                });
            };

        }

        private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            SetTheme(e.RequestedTheme);
        }
        private void SetTheme(AppTheme theme)
        {
            ResourceDictionary themeDictionary;

            if (theme == AppTheme.Dark)
            {
                themeDictionary = new DarkTheme(); // Просто создание словаря из XAML
            }
            else
            {
                themeDictionary = new LightTheme();
            }

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(themeDictionary);
        }
    }
}