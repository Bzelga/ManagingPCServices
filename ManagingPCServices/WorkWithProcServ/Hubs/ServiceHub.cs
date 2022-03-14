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

        public async Task Do(SendCommandPackage package)
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
    }
}
