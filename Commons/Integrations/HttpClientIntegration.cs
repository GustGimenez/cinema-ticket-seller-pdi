using System.Net.Http.Json;
using Commons.Extensions;
using Commons.Integrations.Interfaces;

namespace Commons.Integrations;

public class HttpClientIntegration : IHttpClientIntegration
{
    private readonly HttpClient _httpClient;

    public HttpClientIntegration(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> Get<T>(RequestData request)
        where T : class
    {
        if (request.Headers is not null)
        {
            AddHeaders(request.Headers);
        }

        var response = await _httpClient.GetAsync(request.Path!);

        return await ReadContent<T>(response);
    }

    public async Task<T?> Post<T>(RequestData request)
        where T : class
    {
        if (request.Headers is not null)
        {
            AddHeaders(request.Headers);
        }

        var content = request.Body is not null ? JsonContent.Create(request.Body) : null;
        var response = await _httpClient.PostAsync(request.Path!, content);

        return await ReadContent<T>(response);
    }

    public async Task<T?> Put<T>(RequestData request)
        where T : class
    {
        if (request.Headers is not null)
        {
            AddHeaders(request.Headers);
        }

        var content = request.Body is not null ? JsonContent.Create(request.Body) : null;
        var response = await _httpClient.PutAsync(request.Path!, content);

        return await ReadContent<T>(response);
    }

    public async Task<T?> Delete<T>(RequestData request) where T : class
    {
        if (request.Headers is not null)
        {
            AddHeaders(request.Headers);
        }

        var response = await _httpClient.DeleteAsync(request.Path!);

        return await ReadContent<T>(response);
    }

    public async Task<HttpResponseMessage> GetAsync(RequestData request)
    {
        if (request.Headers is not null)
        {
            AddHeaders(request.Headers);
        }

        return await _httpClient.GetAsync(request.Path!);
    }

    public async Task<HttpResponseMessage> PostAsync(RequestData request)
    {
        if (request.Headers is not null)
        {
            AddHeaders(request.Headers);
        }

        var content = request.Body is not null ? JsonContent.Create(request.Body) : null;
        return await _httpClient.PostAsync(request.Path!, content);
    }

    public async Task<HttpResponseMessage> PutAsync(RequestData request)
    {
        if (request.Headers is not null)
        {
            AddHeaders(request.Headers);
        }

        var content = request.Body is not null ? JsonContent.Create(request.Body) : null;
        return await _httpClient.PutAsync(request.Path!, content);
    }

    public async Task<HttpResponseMessage> DeleteAsync(RequestData request)
    {
        if (request.Headers is not null)
        {
            AddHeaders(request.Headers);
        }

        return await _httpClient.DeleteAsync(request.Path!);
    }

    private async Task<T?> ReadContent<T>(HttpResponseMessage response)
        where T : class
    {
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var responseContent = await response.Content.ReadAsStreamAsync();

        return await responseContent.DeserializeJsonAsync<T>();
    }

    private void AddHeaders(Dictionary<string, string> headers)
    {
        foreach (var header in headers)
        {
            if (_httpClient.DefaultRequestHeaders.Any())
            {
                var values = _httpClient.DefaultRequestHeaders.GetValues(header.Key);

                if (values.Any())
                {
                    continue;
                }
            }

            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }
}