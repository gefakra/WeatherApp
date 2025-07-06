using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherInfo> GetWeatherAsync(string city);              
    }
}
