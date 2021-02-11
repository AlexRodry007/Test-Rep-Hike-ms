using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class PassiveUse
    {
        public int Id { get; set; }
        public string UseName { get; set; }
        public int? Level { get; set; }
        public int? Item { get; set; }

        public virtual Item ItemNavigation { get; set; }
    }
}
