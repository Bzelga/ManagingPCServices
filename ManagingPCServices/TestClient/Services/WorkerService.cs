using System;
using System.ServiceProcess;
using TestClient.Models;
using TestClient.Services.Interfaces;

namespace TestClient.Services
{
    class WorkerService : IService
    {
        public ServiceAndStatusModel[] GetAllServices()
        {
            var services = ServiceController.GetServices();

            int countService = services.Length;
            ServiceAndStatusModel[] serviceAndStatusModels = new ServiceAndStatusModel[countService];


            for(int i = 0; i < countService; i++)
            {
                serviceAndStatusModels[i] = new ServiceAndStatusModel
                {
                    NameService = services[i].DisplayName,
                    StatusService = services[i].Status.ToString()
                };
            }

            return serviceAndStatusModels;
        }

        public ServiceAndStatusModel GetService(string displayName)
        {
            var services = ServiceController.GetServices();
            ServiceAndStatusModel foundService = new ServiceAndStatusModel();

            foreach (var service in services)
            {
                if(service.DisplayName == displayName)
                {
                    foundService = new ServiceAndStatusModel
                    {
                        NameService = service.DisplayName,
                        StatusService = service.Status.ToString()
                    };
                }
            }

            return foundService;
        }

        public string StopService(string name)
        {
            ServiceController service = new ServiceController(name);
            string answer;

            try
            {
                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                }
                answer = "Сервис успешно остановлен";
            }
            catch(Exception ex)
            {
                answer = "Не удалось остановить сервис " + ex.Message;
            }

            service.Close();

            return answer;
        }

        public string PauseService(string name)
        {
            ServiceController service = new ServiceController(name);
            string answer;

            try
            {
                if (service.Status != ServiceControllerStatus.Paused)
                {
                    service.Pause();
                }
                answer = "Сервис успешно поставлен на паузу";
            }
            catch(Exception ex)
            {
                answer = "Не удалось поставить на паузу сервис " + ex.Message;
            }

            service.Close();

            return answer;
        }

        public string RefreshService(string name)
        {
            ServiceController service = new ServiceController(name);
            string answer;

            try
            {
                service.Refresh();
                answer = "Сервис успешно перезагружен";
            }
            catch(Exception ex)
            {
                answer = "Не удалось перезагрузить сервис " + ex.Message;
            }

            service.Close();

            return answer;
        }

        public string StartService(string name)
        {
            ServiceController service = new ServiceController(name);
            string answer;

            try
            {
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();
                }
                answer = "Сервис успешно запущен";
            }
            catch(Exception ex)
            {
                answer = "Не удалось запустить сервис " + ex.Message;
            }

            service.Close();

            return answer;
        }
    }
}
