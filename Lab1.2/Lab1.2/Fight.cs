using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Fight
    {
        public Fight()
        {
            Tactics = new HashSet<Tactic>();
        }

        public int Id { get; set; }
        public int? Choises { get; set; }
        public int? ActionPoints { get; set; }
        public int? Round { get; set; }
        public int? CritStatus { get; set; }

        public virtual Choice ChoisesNavigation { get; set; }
        public virtual ICollection<Tactic> Tactics { get; set; }
    }
}
