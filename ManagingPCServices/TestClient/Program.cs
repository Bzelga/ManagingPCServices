using ManagingPCServices.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using TestClient.Enums;
using TestClient.Models;

namespace TestClient
{
    public class Program
    {
        private static ServiceManager _serviceManager;
        static void Main(string[] args)
        {
            Console.WriteLine("Введите ip");
            string path = Console.ReadLine();
            connectToServer(path);
            Console.ReadKey();
        }

        private static async void connectToServer(string path)
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl($"http://{path}/servicehub")
                .Build();


            hubConnection.StartAsync().Wait();

            _serviceManager = new ServiceManager(hubConnection);

            hubConnection.On("Do", (SendCommandPackage package) =>
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
                        _serviceManager.WorkProcess(package.TypeAction, package.ArgsForAction);
                        break;
                    case TypeCommand.RegystryProgramm:
                        _serviceManager.WorkRegystryProgramm(package.ArgsForAction);
                        break;
                    case TypeCommand.GetData:
                        switch ((TypeGetData)Convert.ToInt32(package.ArgsForAction))
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
                            case TypeGetData.ParametersCompterSystem:
                                _serviceManager.GetParametersComputerSystem();
                                break;
                        }
                        break;
                }
            });
        }
    }
}
