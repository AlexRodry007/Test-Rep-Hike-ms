using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Use
    {
        public Use()
        {
            Conseqences = new HashSet<Conseqence>();
        }

        public int Id { get; set; }
        public string UseName { get; set; }
        public string Consumable { get; set; }
        public int? Item { get; set; }

        public virtual Item ItemNavigation { get; set; }
        public virtual ICollection<Conseqence> Conseqences { get; set; }
    }
}
