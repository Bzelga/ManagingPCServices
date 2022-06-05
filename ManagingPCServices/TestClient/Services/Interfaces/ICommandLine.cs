namespace TestClient.Services.Interfaces
{
    public interface ICommandLine
    {
        string ExecuteCommandCMD(string command);

        string ExecuteCommandPowerShell(string command);
    }
}
