namespace TestClient.Services.Interfaces
{
    public interface IRegistry
    {
        string[] GetRegistryKeyNames();

        string RemoveFromRegistry(string name);
    }
}
