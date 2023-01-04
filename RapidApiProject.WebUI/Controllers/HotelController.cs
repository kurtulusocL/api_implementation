using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiProject.Entities;

namespace RapidApiProject.WebUI.Controllers
{
    public class HotelController : Controller
    {
        List<BookingHotelSearchDto> hotel = new List<BookingHotelSearchDto>();
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v2/hotels/search?room_number=1&dest_type=city&order_by=popularity&dest_id=-3366242&locale=en-gb&checkin_date=2023-05-27&filter_by_currency=USD&checkout_date=2023-05-28&adults_number=2&units=metric&children_ages=5%2C0&include_adjacency=true&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&children_number=2&page_number=0"),
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
                var replaceForBody = body.Replace(".", "");
                var hotel = JsonConvert.DeserializeObject<BookingHotelSearchDto>(replaceForBody);
                return View(hotel.result);
            }
        }

        [HttpGet]
        public IActionResult GetCityId()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCityId(string p)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?locale=en-gb&name={p}"),
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

                return View();
            }

        }
    }
}
