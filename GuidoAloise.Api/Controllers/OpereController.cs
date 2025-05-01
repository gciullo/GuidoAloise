using Microsoft.AspNetCore.Mvc;
using GuidoAloise.Api.Models;
using System.Text.Json;

namespace GalleriaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OpereController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly string _jsonPath;
    private readonly string _imgFolder;

    public OpereController(IWebHostEnvironment env)
    {
        _env = env;
        _jsonPath = Path.Combine(_env.WebRootPath, "opere.json");
        _imgFolder = Path.Combine(_env.WebRootPath, "img", "opere");

        if (!Directory.Exists(_imgFolder))
            Directory.CreateDirectory(_imgFolder);

        if (!System.IO.File.Exists(_jsonPath))
            System.IO.File.WriteAllText(_jsonPath, "[]");
    }

    [HttpGet]
    public IActionResult GetOpere()
    {
        var json = System.IO.File.ReadAllText(_jsonPath);
        var opere = JsonSerializer.Deserialize<List<Opera>>(json) ?? new();
        return Ok(opere);
    }

    [HttpPost("aggiungi")]
    public IActionResult AggiungiOpera([FromBody] Opera opera)
    {
        var opere = GetOpereList();
        opere.Add(opera);
        SaveOpere(opere);
        return Ok();
    }

    [HttpPost("rimuovi")]
    public IActionResult RimuoviOpera([FromBody] Opera opera)
    {
        var opere = GetOpereList();

        // Rimuove tutte le opere che corrispondono
        var daRimuovere = opere.Where(o => o.Titolo == opera.Titolo && o.Anno == opera.Anno).ToList();

        foreach (var o in daRimuovere)
        {
            if (!string.IsNullOrWhiteSpace(o.UrlImmagine))
            {
                var pathSplitted = o.UrlImmagine.Split('/');

                var imgPath = Path.Combine(_imgFolder, pathSplitted.Last());

                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
            }
        }


        opere.RemoveAll(o => o.Titolo == opera.Titolo && o.Anno == opera.Anno);
        SaveOpere(opere);
        return Ok();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File non valido");

        var fileName = Path.GetFileNameWithoutExtension(file.FileName)
                    + "_" + Guid.NewGuid().ToString("N").Substring(0, 8)
                    + Path.GetExtension(file.FileName);

        var filePath = Path.Combine(_imgFolder, fileName);

        await using var stream = System.IO.File.Create(filePath);
        await file.CopyToAsync(stream);

        var relativePath = $"img/opere/{fileName}";
        return Ok(new { path = relativePath });
    }

    // Helpers
    private List<Opera> GetOpereList()
    {
        var json = System.IO.File.ReadAllText(_jsonPath);
        return JsonSerializer.Deserialize<List<Opera>>(json) ?? new();
    }

    private void SaveOpere(List<Opera> opere)
    {
        var json = JsonSerializer.Serialize(opere, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText(_jsonPath, json);
    }
}
