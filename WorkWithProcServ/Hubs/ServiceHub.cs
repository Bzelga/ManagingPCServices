using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ManagingPCServices.Services;

namespace ManagingPCServices.Hubs
{
    public class ServiceHub : Hub<IServiceHub>
    {
        ServiceManager _serviceManager;

        public ServiceHub(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task WorkWithNetworkCard(int input, string name)
        {
            _serviceManager.WorkNetworkCard(input, name);
        }

        public async Task WorkCommandLine(int input, string command)
        {
            _serviceManager.WorkCommandLine(input, command);
        }

        public async Task WorkWithService(int input, string nameService)
        {
            _serviceManager.WorkService(input, nameService);
        }

        public async Task WorkWithProcess(int input, int id)
        {
            _serviceManager.WorkProcess(input, id);
        }

        public async Task WorkWithRegysty(string name)
        {
            _serviceManager.WorkRegystryProgramm(name);
        }

        public async Task GetNetworkCards()
        {
            _serviceManager.GetAllNetworkCards();
        }

        public async Task GetServices()
        {
            _serviceManager.GetAllService();
        }

        public async Task GetProcess()
        {
            _serviceManager.GetAllProcess();
        }

        public async Task GetRegistryAutorunProgramm()
        {
            _serviceManager.GetAllProgrammInAutorun();
        }
    }
}
