using TestClient.Models;

namespace TestClient.Services.Interfaces
{
    public interface IProcess
    {
        ProcessIdStatusModel[] GetAllProcesses();

        ProcessIdStatusModel GetProcess(int id);

        string KillProcess(string name);

        string KillProcess(int id);

        string SuspendProcess(string name);

        string SuspendProcess(int id);

        string ResumeProcess(string name);

        string  ResumeProcess(int id);
    }
}
