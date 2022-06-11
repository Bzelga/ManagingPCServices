using System;

namespace TestClient.Services
{
    public class RecipientParameters
    {
        private Random random;

        public RecipientParameters()
        {
            random = new Random();
        }

        public string[] GetParameters()
        {
            string[] parameters = new string[]
            {
                $"process,firefox,used ram,{random.NextDouble() * (10 - 1) + 1}",
                $"process,some,used ram,{random.NextDouble() * (10 - 1) + 1}",
                $"network,someapp,quantity packet,{random.Next(1,20)}",
                $"sensors,cpu,load,{random.NextDouble() * (100 - 1) + 1}",
                $"sensors,cpu,temperature,{random.NextDouble() * (120 - 1) + 1}",
                $"sensors,gpu,load,{random.NextDouble() * (100 - 1) + 1}"
            };

            return parameters;
        }
    }
}
