using Microsoft.AspNetCore.SignalR.Client;
using TestClient.Models;
using TestClient.Services;
using TestClient.Services.Interfaces;

namespace ManagingPCServices.Services
{

    public class ServiceManager : IServiceManager
    {
        internal readonly WorkerNetworkCard _network;
        internal readonly WorkerProcess _process;
        internal readonly WorkerRegistry _registry;
        internal readonly WorkerService _service;
        internal readonly WorkerCommandLine _commandLine;
        internal readonly RecipientParameters _recipientParameters;
        internal readonly HubConnection _hub;

        public ServiceManager(HubConnection hub)
        {
            _network = new WorkerNetworkCard();
            _process = new WorkerProcess();
            _registry = new WorkerRegistry();
            _service = new WorkerService();
            _commandLine = new WorkerCommandLine();
            _recipientParameters = new RecipientParameters();
            _hub = hub;
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

            _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 1, ReturtAnswer = answer } });

            if (input == 2 || input == 3)
                _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 2, ReturnNetworkCard = _network.GetNetworkCard(name) } });

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

            _hub.InvokeCoreAsync("GetResponseClient", args: new[] { answer });
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

            _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 1, ReturtAnswer = answer } });

            _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 2, ReturnService = _service.GetService(nameService) } });
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

            _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 1, ReturtAnswer = answer } });

            _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 2, ReturnProces = _process.GetProcess(id) } });
        }

        public void WorkProcess(int input, string name)
        {
            string answer = "";

            switch (input)
            {
                case 0:
                    answer = _process.KillProcess(name);
                    break;
                case 1:
                    answer = _process.SuspendProcess(name);
                    break;
                case 2:
                    answer = _process.ResumeProcess(name);
                    break;
            }

            _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 1, ReturtAnswer = answer } });

            //_hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 2, ReturnProces = _process.GetProcess(name) } });
        }

        public void WorkRegystryProgramm(string nameProgramm)
        {
            _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 1, ReturtAnswer = _registry.RemoveFromRegistry(nameProgramm) } });
        }

        public async void GetAllNetworkCards()
        {
            await _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 0, ReturnNetworkCards = _network.GetAllNetworkCards() } });
        }

        public async void GetAllService()
        {
            await _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 0, ReturnServices = _service.GetAllServices() } });
        }

        public async void GetAllProcess()
        {
            await _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 0, ReturnProcess = _process.GetAllProcesses() } });
        }

        public async void GetAllProgrammInAutorun()
        {
            await _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 0, ReturnProgrammInRegistry = _registry.GetRegistryKeyNames() } });
        }

        public async void GetParametersComputerSystem()
        {
            await _hub.InvokeCoreAsync("GetResponseClient", args: new[] { new ReceiveCommandPackage { TypeCommand = 3, ArgsComputerSystem = _recipientParameters.GetParameters() } });
        }
    }
}
