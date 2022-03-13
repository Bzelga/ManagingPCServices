using Microsoft.Win32;
using System;

namespace ManagingPCServices.Services
{
    class WorkerRegistry : IRegistry
    {
        //может быть не только с автозагрузками придется работать
        public string[] GetRegistryKeyNames()
        {
            RegistryKey localMachineKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            var result = localMachineKey.GetValueNames();

            localMachineKey.Close();

            return result;
        }

        public string RemoveFromRegistry(string name)
        {
            RegistryKey localMachineKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string answer;

            try
            {
                localMachineKey.DeleteValue(name);

                answer = "Успешно";
            }
            catch(Exception ex)
            {
                answer = "Не удалось удалить + " + ex.Message;
            }

            localMachineKey.Close();

            return answer;
        }
    }
}
