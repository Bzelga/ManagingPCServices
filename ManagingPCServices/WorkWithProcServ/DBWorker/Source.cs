using System;
using System.Collections.Generic;

namespace ManagingPCServices.DBWorker
{
    public partial class Source
    {
        public Source()
        {
            Parameters = new HashSet<Parameter>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Parameter> Parameters { get; set; }
    }
}
