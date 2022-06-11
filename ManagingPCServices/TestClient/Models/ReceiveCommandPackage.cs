namespace TestClient.Models
{
    public class ReceiveCommandPackage
    {
        public int TypeCommand { get; set; }

        public string ReturtAnswer { get; set; }

        public string[] ReturnProgrammInRegistry {get; set;}

        public NetworkCardModel[] ReturnNetworkCards { get; set; }

        public ProcessIdStatusModel[] ReturnProcess { get; set; }

        public ServiceAndStatusModel[] ReturnServices { get; set; }

        public NetworkCardModel ReturnNetworkCard { get; set; }

        public ProcessIdStatusModel ReturnProces { get; set; }

        public ServiceAndStatusModel ReturnService { get; set; }

        public string[] ArgsComputerSystem { get; set; }
    }
}
