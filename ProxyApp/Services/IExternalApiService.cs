namespace ProxyApp.Services
{
    public interface IExternalApiService
    {
        string GetResource(string resource);
        string GetResourceById(string resource, int id);
    }
}
