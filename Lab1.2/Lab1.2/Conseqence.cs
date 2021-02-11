using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Conseqence
    {
        public string ConsName { get; set; }
        public int? ConsValue { get; set; }
        public int? ItemUse { get; set; }
        public int? UseName { get; set; }
        public int? Choise { get; set; }

        public virtual Choice ChoiseNavigation { get; set; }
        public virtual Use UseNameNavigation { get; set; }
    }
}
