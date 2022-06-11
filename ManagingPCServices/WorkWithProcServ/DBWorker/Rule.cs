using System;
using System.Collections.Generic;

namespace ManagingPCServices.DBWorker
{
    public partial class Rule
    {
        public long Id { get; set; }
        public string Predicate { get; set; }
        public long Action { get; set; }

        public virtual TypeAction ActionNavigation { get; set; }
    }
}
