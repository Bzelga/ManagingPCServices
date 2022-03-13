using ManagingPCServices.Models;
using System;
using System.Collections.Generic;
using System.Management;

namespace ManagingPCServices.Services
{
    //поменять сбор всех сетевых карт на  ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(string.Format("SELECT * FROM Win32_PnpEntity WHERE Name = \"{0}\"", name)); только на GUID

    class WorkerNetworkCard : INetworkCard
    {
        public List<NetworkCardModel> GetAllNetworkCards()
        {
            ManagementObjectSearcher searchProcedure1 = new ManagementObjectSearcher("SELECT Name, Status FROM Win32_PnpEntity WHERE ClassGuid = \"{4d36e972-e325-11ce-bfc1-08002be10318}\"");
            List<NetworkCardModel> cards = new List<NetworkCardModel>();

            foreach (ManagementObject item in searchProcedure1.Get())
            {
                var newCards = new NetworkCardModel();
                foreach (var prop in item.Properties)
                {
                    if (prop.Name == "Name")
                    {
                        newCards.Name = prop.Value.ToString();
                    }

                    if (prop.Name == "Status")
                    {
                        newCards.Status = prop.Value.ToString();
                    }
                }
                cards.Add(newCards);
            }

            return cards;
        }

        public NetworkCardModel GetNetworkCard(string name)
        {
            ManagementObjectSearcher searchProcedure1 = new ManagementObjectSearcher(String.Format("SELECT Status FROM Win32_PnpEntity WHERE Name = \"{0}\"", name));
            foreach (ManagementObject item in searchProcedure1.Get())
            {
                var newCards = new NetworkCardModel()
                {
                    Name = name
                };

                foreach (var prop in item.Properties)
                {
                    if (prop.Name == "Status")
                    {
                        newCards.Status = prop.Value.ToString();
                        return newCards;
                    }
                }
            }

            return null;
        }

        public string DisableAllNetwork()
        {
            try
            {
                ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
                foreach (ManagementObject item in searchProcedure.Get())
                {
                    item.InvokeMethod("Disable", null);
                }

                return "Успешно";
            }
            catch (Exception ex)
            {
                return "Ошибка " + ex.Message;
            }
        }

        public string EnableAllNetwork()
        {
            try
            {
                ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
                foreach (ManagementObject item in searchProcedure.Get())
                {
                    item.InvokeMethod("Enable", null);
                }

                return "Успешно";
            }
            catch (Exception ex)
            {
                return "Ошибка " + ex.Message;
            }
        }

        public string DisableNetworkByName(string name)
        {
            try
            {
                ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(string.Format("SELECT * FROM Win32_PnpEntity WHERE Name = \"{0}\"", name));

                foreach (ManagementObject item in searchProcedure.Get())
                {
                    item.InvokeMethod("Disable", new object[] { false });
                    break;
                }

                return "Успешно";
            }
            catch (Exception ex)
            {
                return "Ошибка " + ex.Message;
            }
        }

        public string EnableNetworkByName(string name)
        {
            try
            {
                ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(string.Format("SELECT * FROM Win32_PnpEntity WHERE Name = \"{0}\"", name));

                foreach (ManagementObject item in searchProcedure.Get())
                {
                    item.InvokeMethod("Enable", new object[] { false });
                    break;
                }

                return "Успешно";
            }
            catch (Exception ex)
            {
                return "Ошибка " + ex.Message;
            }
        }
    }
}
