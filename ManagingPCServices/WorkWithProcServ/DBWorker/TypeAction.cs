using System;
using System.Collections.Generic;

namespace ManagingPCServices.DBWorker
{
    public partial class TypeAction
    {
        public TypeAction()
        {
            Rules = new HashSet<Rule>();
        }

        public long Id { get; set; }
        public long TypeCommand { get; set; }
        public long NumberAction { get; set; }
        public string TextAction { get; set; }

        public virtual ICollection<Rule> Rules { get; set; }
    }
}
