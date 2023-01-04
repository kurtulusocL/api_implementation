using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiProject.Entities;

namespace RapidApiProject.WebUI.Controllers
{
    public class CurrencyController : Controller
    {
        List<BookingCurrencyDto> currency = new List<BookingCurrencyDto>();
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency=TRY&locale=en-gb"),
                Headers =
    {
        { "X-RapidAPI-Key", "your api key" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var currency = JsonConvert.DeserializeObject<BookingCurrencyDto>(body);
                return View(currency.exchange_rates);
            }
        }
    }
}
