using System.Threading.Tasks;

namespace ManagingPCServices.Hubs
{
    public interface IServiceHub
    {
        Task ReciveArrayData<T>(T data);

        Task ReceiveChangeStatus<T>(T process);

        Task ReceiveResult(string result);
    }
}
