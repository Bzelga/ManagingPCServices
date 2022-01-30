namespace ManagingPCServices.Services
{
    public interface IRegistry
    {
        string[] GetRegistryKeyNames();

        string RemoveFromRegistry(string name);
    }
}
