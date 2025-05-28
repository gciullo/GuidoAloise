using Microsoft.AspNetCore.Mvc;
using GuidoAloise.Api.Models;
using System.Text.Json;

namespace GalleriaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly string _jsonPath;

    public NewsController(IWebHostEnvironment env)
    {
        _env = env;
        _jsonPath = Path.Combine(_env.WebRootPath, "news.json");

        if (!System.IO.File.Exists(_jsonPath))
            System.IO.File.WriteAllText(_jsonPath, "[]");
    }

    [HttpGet]
    public IActionResult GetNews()
    {
        var json = System.IO.File.ReadAllText(_jsonPath);
        var news = JsonSerializer.Deserialize<List<NewsItem>>(json) ?? new();
        return Ok(news);
    }

    [HttpPost("aggiungi")]
    public IActionResult AggiungiNews([FromBody] NewsItem newsItem)
    {
        var news = GetNewsList();
        news.Add(newsItem);
        SaveNews(news);
        return Ok();
    }

    [HttpPost("rimuovi")]
    public IActionResult RimuoviNews([FromBody] NewsItem newsItem)
    {
        var news = GetNewsList();

        news.RemoveAll(o => o.Id == newsItem.Id);
        SaveNews(news);
        return Ok();
    }

    // Helpers
    private List<NewsItem> GetNewsList()
    {
        var json = System.IO.File.ReadAllText(_jsonPath);
        return JsonSerializer.Deserialize<List<NewsItem>>(json) ?? new();
    }

    private void SaveNews(List<NewsItem> news)
    {
        var json = JsonSerializer.Serialize(news, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText(_jsonPath, json);
    }
}
