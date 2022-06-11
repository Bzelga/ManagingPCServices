using System;
using System.Collections.Generic;

namespace ManagingPCServices.DBWorker
{
    public partial class Parameter
    {
        public long Id { get; set; }
        public string Designation { get; set; }
        public long TypeParameter { get; set; }
        public long Source { get; set; }
        public long TypeValue { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public virtual Source SourceNavigation { get; set; }
        public virtual TypeParameter TypeParameterNavigation { get; set; }
        public virtual TypeValue TypeValueNavigation { get; set; }
    }
}
