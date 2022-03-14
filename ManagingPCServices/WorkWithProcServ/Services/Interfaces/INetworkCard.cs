using System.Collections.Generic;
using ManagingPCServices.Models;

namespace ManagingPCServices.Services
{
    public interface INetworkCard
    {
        NetworkCardModel[] GetAllNetworkCards();
        string DisableAllNetwork();
        string EnableAllNetwork();
        string DisableNetworkByName(string name);
        string EnableNetworkByName(string name);
    }
}
