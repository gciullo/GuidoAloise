using GuidoAloise.Models;
using Microsoft.AspNetCore.Components.Forms;
using static System.Net.WebRequestMethods;

public interface IDataService
{
    Task<List<Opera>> GetOpereAsync();
    Task AddOperaAsync(Opera opera);
    Task RemoveOperaAsync(Opera opera);
    Task<string?> UploadImageAsync(IBrowserFile file);
    Task<List<NewsItem>> GetNewsAsync();
    Task AddNewsItemAsync(NewsItem newsItem);
    Task RemoveNewsItemAsync(NewsItem newsItem);
}
