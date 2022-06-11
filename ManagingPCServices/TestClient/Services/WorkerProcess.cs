using System;
using System.Diagnostics;
using TestClient.Models;
using TestClient.Services.Interfaces;

namespace TestClient.Services
{
    public class WorkerProcess : IProcess
    {
        public ProcessIdStatusModel[] GetAllProcesses()
        {
            var processes = Process.GetProcesses();

            int countProcesses = processes.Length;
            ProcessIdStatusModel[] processIdStatusModels = new ProcessIdStatusModel[countProcesses];

            for (int i = 0; i < countProcesses; i++)
            {
                processIdStatusModels[i] = new ProcessIdStatusModel
                {
                    IdProcess = processes[i].Id,
                    NameProcess = processes[i].ProcessName,
                    StatusProcess = processes[i].Responding
                };
            }

            return processIdStatusModels;
        }

        public ProcessIdStatusModel GetProcess(int id)
        {
            var process = Process.GetProcessById(id);
            return new ProcessIdStatusModel
            {
                IdProcess = id,
                NameProcess = process.ProcessName,
                StatusProcess = process.Responding
            };
        }

        public string KillProcess(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);

            try
            {
                foreach (var process in processes)
                {
                    process.Kill();
                }
                return "Успешно";
            }
            catch (Exception ex)
            {
                return "Не удалось убить процесс " + ex.Message;
            }
        }

        public string KillProcess(int id)
        {
            try
            {
                Process process = Process.GetProcessById(id);
                process.Kill();
                return "Успешно";
            }
            catch (Exception ex)
            {
                return "Не удалось убить процесс " + ex.Message;
            }
        }

        public string SuspendProcess(string name)
        {
            SuspendResumeProcessKernel32 SRPK32 = new SuspendResumeProcessKernel32();
            Process[] processes = Process.GetProcessesByName(name);
            try
            {
                foreach (var process in processes)
                {
                    foreach (ProcessThread pT in process.Threads)
                    {
                        nint pOpenThread = SRPK32.OpenThread(SuspendResumeProcessKernel32.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                        if (pOpenThread == 0)
                        {
                            continue;
                        }

                        SRPK32.SuspendThread(pOpenThread);
                        SRPK32.CloseHandle(pOpenThread);
                    }
                }
                return "Успешно";
            }
            catch (Exception ex)
            {
                return "Не удалось остановить процесс " + ex.Message;
            }
        }

        public string SuspendProcess(int id)
        {
            SuspendResumeProcessKernel32 SRPK32 = new SuspendResumeProcessKernel32();
            Process process = Process.GetProcessById(id);

            try
            {

                foreach (ProcessThread pT in process.Threads)
                {
                    nint pOpenThread = SRPK32.OpenThread(SuspendResumeProcessKernel32.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                    if (pOpenThread == 0)
                    {
                        continue;
                    }

                    SRPK32.SuspendThread(pOpenThread);
                    SRPK32.CloseHandle(pOpenThread);
                }

                return "Успешно";
            }
            catch (Exception ex)
            {
                return "Не удалось остановить процесс " + ex.Message;
            }

        }

        public string ResumeProcess(string name)
        {
            SuspendResumeProcessKernel32 SRPK32 = new SuspendResumeProcessKernel32();
            Process[] processes = Process.GetProcessesByName(name);
            try
            {
                foreach (var process in processes)
                {
                    foreach (ProcessThread pT in process.Threads)
                    {
                        nint pOpenThread = SRPK32.OpenThread(SuspendResumeProcessKernel32.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                        if (pOpenThread == 0)
                        {
                            continue;
                        }

                        var suspendCount = 0;
                        do
                        {
                            suspendCount = SRPK32.ResumeThread(pOpenThread);
                        } while (suspendCount > 0);

                        SRPK32.CloseHandle(pOpenThread);
                    }
                }
                return "Успешно";
            }
            catch (Exception ex)
            {
                return "Не удалось возабновить процесс " + ex.Message;
            }
        }

        public string ResumeProcess(int id)
        {
            SuspendResumeProcessKernel32 SRPK32 = new SuspendResumeProcessKernel32();
            Process process = Process.GetProcessById(id);

            try
            {
                foreach (ProcessThread pT in process.Threads)
                {
                    nint pOpenThread = SRPK32.OpenThread(SuspendResumeProcessKernel32.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                    if (pOpenThread == 0)
                    {
                        continue;
                    }

                    var suspendCount = 0;
                    do
                    {
                        suspendCount = SRPK32.ResumeThread(pOpenThread);
                    } while (suspendCount > 0);

                    SRPK32.CloseHandle(pOpenThread);
                }

                return "Успешно";
            }
            catch (Exception ex)
            {
                return "Не удалось возабновить процесс " + ex.Message;
            }
        }
    }
}
