using ManagingPCServices.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ManagingPCServices.Hubs
{
    public class ServiceHub : Hub<IServiceHub>
    {
        public async Task GetResponseClient(ReceiveCommandPackage package)
        {
            this.Clients.All.Result(package);
        }

        public async Task GetUsersCommand(SendCommandPackage package)
        {
            this.Clients.All.Do(package);

        }
    }
}
