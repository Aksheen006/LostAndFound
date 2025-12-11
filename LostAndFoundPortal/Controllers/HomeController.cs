using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using LostAndFoundPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFoundPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        private const string ApiBaseUrl = "http://localhost:5039";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient { BaseAddress = new Uri(ApiBaseUrl) };
        }

        // -------------------------
        // SHOW ALL ITEMS
        // -------------------------
        public async Task<IActionResult> Index()
        {
            var items = await _httpClient.GetFromJsonAsync<List<Item>>("/api/Item/all");
            return View(items ?? new List<Item>());
        }

        // -------------------------
        // ADD ITEM - GET
        // -------------------------
        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }

        // -------------------------
        // ADD ITEM - POST
        // -------------------------
        [HttpPost]
        public async Task<IActionResult> AddItem(Item item)
        {
            if (!ModelState.IsValid)
                return View(item);

            var list = new List<Item> { item };

            await _httpClient.PostAsJsonAsync("/api/Item/addItems", list);

            return RedirectToAction(nameof(Index));
        }

        // -------------------------
        // EDIT ITEM - GET
        // -------------------------
        [HttpGet]
        public async Task<IActionResult> EditItem(Guid id)
        {
            var item = await _httpClient.GetFromJsonAsync<Item>($"/api/Item/{id}");

            if (item == null)
                return NotFound();

            return View(item);
        }

        // -------------------------
        // EDIT ITEM - POST
        // -------------------------
        [HttpPost]
        public async Task<IActionResult> EditItem(Item item)
        {
            if (!ModelState.IsValid)
                return View(item);

            await _httpClient.PutAsJsonAsync($"/api/Item/updateItem/{item.Id}", item);

            return RedirectToAction(nameof(Index));
        }

        // -------------------------
        // DELETE ITEM
        // -------------------------
        [HttpPost]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var itemList = new List<Item>
            {
                new Item { Id = id }
            };

            await _httpClient.SendAsync(
                new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri("/api/Item/removeItems", UriKind.Relative),
                    Content = JsonContent.Create(itemList)
                }
            );

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
