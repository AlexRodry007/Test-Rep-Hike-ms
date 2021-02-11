using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Action
    {
        public int ActionValue { get; set; }
        public string ActionName { get; set; }
        public int? Tactic { get; set; }

        public virtual Tactic TacticNavigation { get; set; }
    }
}
