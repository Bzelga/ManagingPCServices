using System;
using System.Diagnostics;
using System.IO;

namespace ManagingPCServices.Services
{
    public class WorkerCommandLine : ICommandLine
    {
        public string ExecuteCommandCMD(string command)
        {
            //CreateNoWindow = true - чтобы создавать консольное окно
            //UseShellExecute = true - чтобы показывать оболочку, где исполняет
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C " + command;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            StreamReader reader = process.StandardOutput;

            return reader.ReadToEnd();
        }

        public string ExecuteCommandPowerShell(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.Arguments = command;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            StreamReader reader = process.StandardOutput;

            return reader.ReadToEnd();
        }
    }
}
