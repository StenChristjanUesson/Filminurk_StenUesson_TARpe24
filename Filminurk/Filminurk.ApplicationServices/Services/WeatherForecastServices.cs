using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Filminurk.Core.Dto.AccuWeatherDtos;
using Filminurk.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Filminurk.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDTO> AccuWeatherResult(AccuLocationWeatherResultDTO dto)
        {
            string apikey = Filminurk.Data.Environment.accuweatherkey;
            var baseUrl = "https://dataservice.accuweather.com/forecasts/v1/daily/1day";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync($"{dto.CityCode}?apikey={apikey}&details=true");
                var jsonResponse = await response.Content.ReadAsStringAsync();
                List<AccuCityCodeRootDto> weatherData = JsonSerializer.Deserialize<List<AccuCityCodeRootDto>>(jsonResponse);
                dto.CityNames = weatherData[0].LocalizedName;
                dto.CityCode = weatherData[0].Key;
            }

            string weatherResponse = baseUrl + $"{dto.CityCode}?apikey={apikey}&details=true";

            using (var ClientWeather = new HttpClient())
            {
                var httpResponseWeather = await ClientWeather.GetAsync(weatherResponse);
                string jsonWeather = await httpResponseWeather.Content.ReadAsStringAsync();

                AccuLoactionRootDto weatherRootDto = JsonSerializer.Deserialize<AccuLoactionRootDto>(jsonWeather);

                dto.EffectiveDate = weatherRootDto.Headline.EffectiveDate;
                dto.EffectiveEpochDate = weatherRootDto.Headline.EffectiveEpochDate;
                dto.Severity = weatherRootDto.Headline.Severity;
                dto.Text = weatherRootDto.Headline.Text;
                dto.Category = weatherRootDto.Headline.Category;
                dto.EndDate = weatherRootDto.Headline.EndDate;
                dto.EndEpochDate = weatherRootDto.Headline.EndEpochDate;

            }
        }
    }
}
