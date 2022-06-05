using ManagingPCServices.Models;
using System.Threading.Tasks;

namespace ManagingPCServices.Hubs
{
    public interface IServiceHub
    {
        Task Result<T>(T result);

        Task Do(SendCommandPackage package);
    }
}
