using HRMaster_MVC_Project.Areas.AdminPanel.Models.AdminViewModels;
using HRMaster_MVC_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace HRMaster_MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _httpClient.GetFromJsonAsync<List<ShowCompanyVM>>("api/Company/get-all-companies");
            if (companies == null || !companies.Any())
            {
                return NotFound(); // veya View("Error"); þeklinde bir hata sayfasý dönebilirsin
            }

            return View(companies);
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
