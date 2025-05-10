using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class FirebaseService
{
    private static readonly string FCM_URL = "https://fcm.googleapis.com/fcm/send";
    private static readonly string ServerKey = "-bzmfO5iyNowgLx18TxzfNZvm19g4hKif5z6hI_9pLw";  // Вставьте ваш Server Key сюда

    // Метод для отправки уведомлений
    public async Task SendPushNotificationAsync(string deviceToken, string title, string body)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"key={ServerKey}");

            var payload = new
            {
                to = deviceToken,  // Токен устройства, на которое отправляется уведомление
                notification = new
                {
                    title,
                    body
                }
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(FCM_URL, content);
            response.EnsureSuccessStatusCode(); // Если запрос не удастся, выбросит исключение
        }
    }

    // Метод для получения токена устройства
    public async Task<string> GetDeviceTokenAsync()
    {
        using (var client = new HttpClient())
        {
            // Вставьте логику для получения токена устройства, например через Firebase SDK или запрос на REST API.
            // Для Android используется SDK Firebase, но для вашего проекта используем API Firebase.

            // Пример для получения токена:
            var response = await client.GetAsync("https://fcm.googleapis.com/v1/projects/YOUR_PROJECT_ID/messages:send");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;  // Вернуть токен (или обработать JSON)
            }

            return string.Empty;
        }
    }
}
