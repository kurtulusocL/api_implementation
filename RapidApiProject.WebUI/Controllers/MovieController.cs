using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiProject.Entities;
using System.Net.Http;

namespace RapidApiProject.WebUI.Controllers
{
    public class MovieController : Controller
    {
        List<MovieDto> movies = new List<MovieDto>();
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
                {
                 { "X-RapidAPI-Key", "2b44d18838msh17657b55ff77ee8p1bae9ejsnb2112bc3937d" },
                 { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                movies = JsonConvert.DeserializeObject<List<MovieDto>>(body);
                return View(movies);
            }
        }
    }
}
