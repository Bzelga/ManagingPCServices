using TestClient.Models;

namespace TestClient.Services.Interfaces
{
    public interface IProcess
    {
        ProcessIdStatusModel[] GetAllProcesses();

        ProcessIdStatusModel GetProcess(int id);

        void KillProcess(string name);

        string KillProcess(int id);

        void SuspendProcess(string name);

        string SuspendProcess(int id);

        void ResumeProcess(string name);

        string  ResumeProcess(int id);
    }
}
