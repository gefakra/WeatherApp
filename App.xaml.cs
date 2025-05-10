using WeatherApp.Resources.Styles;
using WeatherApp.Services;
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

            GetFirebaseTokenAsync();
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

        private async void GetFirebaseTokenAsync()
        {
            var firebaseService = new FirebaseService();
            var token = await firebaseService.GetDeviceTokenAsync();

            // Вывод токена в консоль (или использовать его для отправки уведомлений)
            Console.WriteLine($"Device token: {token}");

            // Вы можете сохранить этот токен для дальнейшего использования или отправки уведомлений.
        }

    }
}