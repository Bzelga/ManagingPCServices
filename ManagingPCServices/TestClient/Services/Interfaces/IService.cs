using TestClient.Models;

namespace TestClient.Services.Interfaces
{
    public interface IService
    {
        ServiceAndStatusModel[] GetAllServices();

        string StopService(string name);

        string StartService(string name);

        string PauseService(string name);

        string RefreshService(string name);
    }
}
