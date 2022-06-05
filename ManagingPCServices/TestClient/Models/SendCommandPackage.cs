namespace TestClient.Models
{
    public class SendCommandPackage
    {
        public int TypeCommand { get; set; }

        public int TypeAction { get; set; }

        public string ArgsForAction { get; set; }

        public string[] ArgsForRule { get; set; }
    }
}
