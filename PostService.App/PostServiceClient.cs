using System.Net;
using System.Net.Http.Json;
using PostService.Dtos;

namespace PostService.App;

public class PostServiceClient : IPostServiceClient
{
    private readonly HttpClient _httpClient;

    public PostServiceClient()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5028") };
    }

    private async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var problem = await response.Content.ReadFromJsonAsync<ValidationErrorResponse>();
            if (problem != null)
            {
                throw new PostingValidationException(problem.Errors);
            }
        }

        throw new HttpRequestException($"Помилка сервера: {(int)response.StatusCode}", null, response.StatusCode);
    }

    public async Task<PostingGetDto[]> GetPostingsAsync()
    {
        using var response = await _httpClient.GetAsync("postings");
        await EnsureSuccessAsync(response);
        var result = await response.Content.ReadFromJsonAsync<PostingGetDto[]>();
        return result ?? throw new System.Text.Json.JsonException("Сервер повернув порожню відповідь.");
    }

    public async Task<PostingGetDto> GetPostingByIdAsync(int id)
    {
        using var response = await _httpClient.GetAsync($"postings/{id}");
        await EnsureSuccessAsync(response);
        var result = await response.Content.ReadFromJsonAsync<PostingGetDto>();
        return result ?? throw new System.Text.Json.JsonException("Сервер повернув порожню відповідь.");
    }

    public async Task<PostingGetDto> CreatePostingAsync(PostingPostDto dto)
    {
        using var response = await _httpClient.PostAsJsonAsync("postings", dto);
        await EnsureSuccessAsync(response);
        return await response.Content.ReadFromJsonAsync<PostingGetDto>() ?? throw new System.Text.Json.JsonException("Сервер повернув порожню відповідь.");
    }

    public async Task<PostingGetDto> UpdatePostingAsync(int id, PostingPutDto dto)
    {
        using var response = await _httpClient.PutAsJsonAsync($"postings/{id}", dto);
        await EnsureSuccessAsync(response);
        return await response.Content.ReadFromJsonAsync<PostingGetDto>() ?? throw new System.Text.Json.JsonException("Сервер повернув порожню відповідь.");
    }

    public async Task DeletePostingAsync(int id)
    {
        using var response = await _httpClient.DeleteAsync($"postings/{id}");
        await EnsureSuccessAsync(response);
    }
}
