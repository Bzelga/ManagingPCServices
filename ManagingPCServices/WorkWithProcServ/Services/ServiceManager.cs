using ManagingPCServices.Hubs;
using ManagingPCServices.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ManagingPCServices.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly WorkerNetworkCard _network;
        private readonly WorkerProcess _process;
        private readonly WorkerRegistry _registry;
        private readonly WorkerService _service;
        private readonly WorkerCommandLine _commandLine;
        private readonly IHubContext<ServiceHub, IServiceHub> _hub;

        public ServiceManager(IHubContext<ServiceHub, IServiceHub> hub)
        {
            _network = new WorkerNetworkCard();
            _process = new WorkerProcess();
            _registry = new WorkerRegistry();
            _service = new WorkerService();
            _commandLine = new WorkerCommandLine();
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

            _hub.Clients.All.ReceiveResult(answer);

            _hub.Clients.All.ReceiveChangeStatus(_network.GetNetworkCard(name));
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

            _hub.Clients.All.ReceiveResult(answer);
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

            _hub.Clients.All.ReceiveResult(answer);

            _hub.Clients.All.ReceiveChangeStatus(_service.GetService(nameService));
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

            _hub.Clients.All.ReceiveResult(answer);

            _hub.Clients.All.ReceiveChangeStatus(_process.GetProcess(id));
        }

        public void WorkRegystryProgramm(string nameProgramm)
        {
            _hub.Clients.All.ReceiveResult(_registry.RemoveFromRegistry(nameProgramm));
        }

        public async void GetAllNetworkCards()
        {
            await _hub.Clients.All.ReciveArrayData(_network.GetAllNetworkCards());
        }

        public async void GetAllService()
        {
            await _hub.Clients.All.ReciveArrayData(_service.GetAllServices());
        }

        public async void GetAllProcess()
        {
            await _hub.Clients.All.ReciveArrayData(_process.GetAllProcesses());
        }

        public async void GetAllProgrammInAutorun()
        {
            await _hub.Clients.All.ReciveArrayData(_registry.GetRegistryKeyNames());
        }
    }
}
