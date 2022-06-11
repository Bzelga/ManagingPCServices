using System;
using System.Collections.Generic;

namespace ManagingPCServices.DBWorker
{
    public partial class TypeValue
    {
        public TypeValue()
        {
            Parameters = new HashSet<Parameter>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Parameter> Parameters { get; set; }
    }
}
