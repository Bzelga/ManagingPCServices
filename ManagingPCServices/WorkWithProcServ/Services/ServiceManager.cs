using ManagingPCServices.Hubs;
using ManagingPCServices.Models;
using ManagingPCServices.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace ManagingPCServices.Services
{
    public class Rules
    {
        public int Action { get; set; }
        public string[] Args { get; set; }
    }

    public class ServiceManager : IServiceManager
    {
        internal readonly WorkerNetworkCard _network;
        internal readonly WorkerProcess _process;
        internal readonly WorkerRegistry _registry;
        internal readonly WorkerService _service;
        internal readonly WorkerCommandLine _commandLine;
        internal readonly IHubContext<ServiceHub, IServiceHub> _hub;

        private readonly Rules[] rules;

        public ServiceManager(IHubContext<ServiceHub, IServiceHub> hub)
        {
            _network = new WorkerNetworkCard();
            _process = new WorkerProcess();
            _registry = new WorkerRegistry();
            _service = new WorkerService();
            _commandLine = new WorkerCommandLine();
            _hub = hub;

            rules = new Rules[]
            {
                new Rules
                {
                    Action = 1,
                    Args = new string[] {"10", "20"}
                },
               new Rules
                {
                    Action = 2,
                    Args = new string[] { "30", "40", "50" }
                }
            };
        }

        public void WorkNetworkCard(int input, string name)
        {
            string answer = "";

            switch (input)
            {
                case 0:
                    answer = _network.DisableAllNetwork();
                    break;
                case 1:
                    answer = _network.EnableAllNetwork();
                    break;
                case 2:
                    answer = _network.DisableNetworkByName(name);
                    break;
                case 3:
                    answer = _network.EnableNetworkByName(name);
                    break;
            }

            _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 1, ReturtAnswer = answer });

            if (input == 2 || input == 3)
                _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 2, ReturnNetworkCard = _network.GetNetworkCard(name) });
        }

        public void WorkCommandLine(int input, string command)
        {
            string answer = "";

            switch (input)
            {
                case 0:
                    answer = _commandLine.ExecuteCommandCMD(command);
                    break;
                case 1:
                    answer = _commandLine.ExecuteCommandPowerShell(command);
                    break;
            }

            _hub.Clients.All.Result(answer);
        }

        public void WorkService(int input, string nameService)
        {
            string answer = "";

            switch (input)
            {
                case 0:
                    answer = _service.StopService(nameService);
                    break;
                case 1:
                    answer = _service.StartService(nameService);
                    break;
                case 2:
                    answer = _service.PauseService(nameService);
                    break;
                case 3:
                    answer = _service.RefreshService(nameService);
                    break;
            }

            _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 1, ReturtAnswer = answer });

            _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 2, ReturnService = _service.GetService(nameService) });
        }

        public void WorkProcess(int input, int id)
        {
            string answer = "";

            switch (input)
            {
                case 0:
                    answer = _process.KillProcess(id);
                    break;
                case 1:
                    answer = _process.SuspendProcess(id);
                    break;
                case 2:
                    answer = _process.ResumeProcess(id);
                    break;
            }

            _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 1, ReturtAnswer = answer });

            _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 2, ReturnProces = _process.GetProcess(id) });
        }

        public void WorkRegystryProgramm(string nameProgramm)
        {
            _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 1, ReturtAnswer = _registry.RemoveFromRegistry(nameProgramm) });
        }

        public void EnforceRule(string[] args)
        {
            var action = checkArgs(args);

            switch (action)
            {
                case 1:
                    _network.DisableAllNetwork();
                    break;
                case 2:
                    _network.EnableAllNetwork();
                    break;
            }

        }

        private int checkArgs(string[] args)
        {
            foreach (var rule in rules)
            {
                if (rule.Args.Length == args.Length)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (args[i] != rule.Args[i])
                            break;
                        if (i == args.Length - 1)
                            return rule.Action;
                    }
                }
            }

            return 0;
        }

        public async void GetAllNetworkCards()
        {
            await _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 0, ReturnNetworkCards = _network.GetAllNetworkCards() });
        }

        public async void GetAllService()
        {
            await _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 0, ReturnServices = _service.GetAllServices() });
        }

        public async void GetAllProcess()
        {
            await _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 0, ReturnProcess = _process.GetAllProcesses() });
        }

        public async void GetAllProgrammInAutorun()
        {
            await _hub.Clients.All.Result(new ReceiveCommandPackage { TypeCommand = 0, ReturnProgrammInRegistry = _registry.GetRegistryKeyNames() });
        }
    }
}
