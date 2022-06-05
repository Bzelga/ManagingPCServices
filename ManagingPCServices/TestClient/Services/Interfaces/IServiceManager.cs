namespace TestClient.Services.Interfaces
{
    public interface IServiceManager
    {
        public void WorkNetworkCard(int input, string name);

        public void WorkCommandLine(int input, string command);

        public void WorkService(int input, string nameService);

        public void WorkProcess(int input, int id);

        public void WorkRegystryProgramm(string nameProgramm);

        public void GetAllNetworkCards();

        public void GetAllService();

        public void GetAllProcess();

        public void GetAllProgrammInAutorun();
    }
}
