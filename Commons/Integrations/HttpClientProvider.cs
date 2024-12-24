using Commons.Integrations.Interfaces;

namespace Commons.Integrations;

public class HttpClientProvider : IHttpClientProvider
{
    public IHttpClientIntegration CreateClient(HttpClient httpClient) {
        return new HttpClientIntegration(httpClient);
    }
}