using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ManagingPCServices.Services;
using ManagingPCServices.Models;
using System;

namespace ManagingPCServices.Hubs
{
    public class ServiceHub : Hub<IServiceHub>
    {
        ServiceManager _serviceManager;

        public ServiceHub(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task Do(CommandPackage package)
        {
            switch (package.TypeCommand)
            {
                case 0:
                    _serviceManager.WorkNetworkCard(package.TypeAction, package.Args);
                    break;
                case 1:
                    _serviceManager.WorkCommandLine(package.TypeAction, package.Args);
                    break;
                case 2:
                    _serviceManager.WorkService(package.TypeAction, package.Args);
                    break;
                case 3:
                    _serviceManager.WorkProcess(package.TypeAction, Convert.ToInt32(package.Args));
                    break;
                case 4:
                    _serviceManager.WorkRegystryProgramm(package.Args);
                    break;
                case 5:
                    switch(Convert.ToInt32(package.Args))
                    {
                        case 0:
                            _serviceManager.GetAllNetworkCards();
                            break;
                        case 1:
                            _serviceManager.GetAllService();
                            break;
                        case 2:
                            _serviceManager.GetAllProcess();
                            break;
                        case 3:
                            _serviceManager.GetAllProgrammInAutorun();
                            break;
                    }
                    break;
            }
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
