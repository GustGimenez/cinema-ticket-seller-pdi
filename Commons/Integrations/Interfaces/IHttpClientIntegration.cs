namespace Commons.Integrations.Interfaces;

public interface IHttpClientIntegration
{
    Task<T?> Get<T>(RequestData request)
        where T : class;

    Task<T?> Post<T>(RequestData request)
        where T : class;

    Task<T?> Put<T>(RequestData request)
        where T : class;

    Task<T?> Delete<T>(RequestData request)
        where T : class;

    Task<HttpResponseMessage> GetAsync(RequestData request);

    Task<HttpResponseMessage> PostAsync(RequestData request);

    Task<HttpResponseMessage> PutAsync(RequestData request);

    Task<HttpResponseMessage> DeleteAsync(RequestData request);
}