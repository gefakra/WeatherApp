using WeatherApp.Resources.Styles;
using Plugin.Firebase.CloudMessaging;
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

            // Инициализируем Firebase
            FirebaseCloudMessaging.Current.OnTokenRefresh += (s, token) =>
            {
                Console.WriteLine($"FCM Token: {token}");
            };

            FirebaseCloudMessaging.Current.OnNotificationReceived += (s, notification) =>
            {
                var title = notification?.Title ?? "Нет заголовка";
                var body = notification?.Body ?? "Нет текста";
                Console.WriteLine($"Уведомление: {title} - {body}");
            };

            MainPage = new AppShell();
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