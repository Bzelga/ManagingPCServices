﻿namespace ManagingPCServices.Models
{
    public class SendCommandPackage
    {
        public int TypeCommand { get; set; }

        public int TypeAction { get; set; }

        public string ArgsForAction { get; set; }
    }
}
