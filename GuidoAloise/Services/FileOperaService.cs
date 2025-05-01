using System.Net.Http.Json;
using GuidoAloise.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace GuidoAloise.Services;

public partial class DataService: IDataService
{
    private readonly HttpClient _http;

    public DataService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Opera>> GetOpereAsync()
    {
        var result = await _http.GetFromJsonAsync<List<Opera>>("api/opere");
        return result ?? new List<Opera>();
    }

    public async Task AddOperaAsync(Opera opera)
    {
        await _http.PostAsJsonAsync("api/opere/aggiungi", opera);
    }

    public async Task RemoveOperaAsync(Opera opera)
    {
        await _http.PostAsJsonAsync("api/opere/rimuovi", opera);
    }

    public async Task<string?> UploadImageAsync(IBrowserFile file)
    {
        var content = new MultipartFormDataContent();

        var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024); // 5MB
        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

        content.Add(fileContent, "file", file.Name);

        var response = await _http.PostAsync("api/opere/upload", content);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadFromJsonAsync<UploadResponse>();
            return json?.Path;
        }

        return null;
    }

    private class UploadResponse
    {
        public string Path { get; set; }
    }
}
