using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text;
using WebPokeApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebPokeApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            PokemonList? results = null;
            Root? root = null;
            string apiResponse = "";

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    results = JsonConvert.DeserializeObject<PokemonList>(apiResponse);
                }
            }
            
            foreach (Pokemon pokemon in results.results)
            {
                using (var httpClient2 = new HttpClient())
                {
                    using (var response = await httpClient2.GetAsync(pokemon.url))
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();

                        root = JsonConvert.DeserializeObject<Root>(apiResponse);

                        pokemon.root = root;
                    }
                }
            }

            ViewBag.PokemonList = results;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}