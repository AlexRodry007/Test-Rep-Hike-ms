using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Choice
    {
        public Choice()
        {
            Conseqences = new HashSet<Conseqence>();
            Fights = new HashSet<Fight>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? Event { get; set; }

        public virtual Event EventNavigation { get; set; }
        public virtual ICollection<Conseqence> Conseqences { get; set; }
        public virtual ICollection<Fight> Fights { get; set; }
    }
}
