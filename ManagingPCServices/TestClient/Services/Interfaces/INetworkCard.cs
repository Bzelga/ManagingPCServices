using System.Collections.Generic;
using TestClient.Models;

namespace TestClient.Services.Interfaces
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
