namespace Commons.Integrations.Interfaces;

public interface IHttpClientProvider
{
    IHttpClientIntegration CreateClient(HttpClient httpClient);
}