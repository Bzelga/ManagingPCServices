using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ManagingPCServices.Services;
using ManagingPCServices.Models;
using System;
using ManagingPCServices.Enums;

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
            switch ((TypeCommand)package.TypeCommand)
            {
                case TypeCommand.NetworkCard:
                    _serviceManager.WorkNetworkCard(package.TypeAction, package.ArgsForAction);
                    break;
                case TypeCommand.CommandLine:
                    _serviceManager.WorkCommandLine(package.TypeAction, package.ArgsForAction);
                    break;
                case TypeCommand.Service:
                    _serviceManager.WorkService(package.TypeAction, package.ArgsForAction);
                    break;
                case TypeCommand.Process:
                    _serviceManager.WorkProcess(package.TypeAction, Convert.ToInt32(package.ArgsForAction));
                    break;
                case TypeCommand.RegystryProgramm:
                    _serviceManager.WorkRegystryProgramm(package.ArgsForAction);
                    break;
                case TypeCommand.GetData:
                    switch((TypeGetData)Convert.ToInt32(package.ArgsForAction))
                    {
                        case TypeGetData.NetworkCards:
                            _serviceManager.GetAllNetworkCards();
                            break;
                        case TypeGetData.Service:
                            _serviceManager.GetAllService();
                            break;
                        case TypeGetData.Processes:
                            _serviceManager.GetAllProcess();
                            break;
                        case TypeGetData.ProgrammAutorun:
                            _serviceManager.GetAllProgrammInAutorun();
                            break;
                    }
                    break;
                case TypeCommand.ArgsForRules:
                    _serviceManager.EnforceRule(package.ArgsForRule);
                    break;
            }
        }
    }
}
