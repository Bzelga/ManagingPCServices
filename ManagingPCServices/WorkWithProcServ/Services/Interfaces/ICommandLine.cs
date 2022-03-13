namespace ManagingPCServices.Services
{
    public interface ICommandLine
    {
        string ExecuteCommandCMD(string command);

        string ExecuteCommandPowerShell(string command);
    }
}
