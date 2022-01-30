using System.Collections.Generic;
using ManagingPCServices.Models;

namespace ManagingPCServices.Services
{
    public interface INetworkCard
    {
        List<NetworkCardModel> GetAllNetworkCards();
        string DisableAllNetwork();
        string EnableAllNetwork();
        string DisableNetworkByName(string name);
        string EnableNetworkByName(string name);
    }
}
