using GuidoAloise.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace GuidoAloise.Services
{
    public partial class DataService : IDataService
    {
        public async Task<List<NewsItem>> GetNewsAsync()
        {
            var result = await _http.GetFromJsonAsync<List<NewsItem>>("api/news");
            return result ?? new List<NewsItem>();
        }

        public async Task AddNewsItemAsync(NewsItem newsItem)
        {
            await _http.PostAsJsonAsync("api/news/aggiungi", newsItem);
        }

        public async Task RemoveNewsItemAsync(NewsItem newsItem)
        {
            await _http.PostAsJsonAsync("api/news/rimuovi", newsItem);
        }
    }
}
