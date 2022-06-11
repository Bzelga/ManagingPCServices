using ManagingPCServices.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ManagingPCServices.Hubs
{
    public class ServiceHub : Hub<IServiceHub>
    {
        RuleChecker _ruleChecker;
        public ServiceHub(RuleChecker ruleChecker)
        {
            _ruleChecker = ruleChecker;
        }

        public async Task GetResponseClient(ReceiveCommandPackage package)
        {
            if (package.TypeCommand == 3)
                await Clients.All.Result(_ruleChecker.CheckRule(package.ArgsComputerSystem));
            else
                await Clients.All.Result(package);
        }

        public async Task GetUsersCommand(SendCommandPackage package)
        {
            await Clients.All.Do(package);

        }
    }
}
