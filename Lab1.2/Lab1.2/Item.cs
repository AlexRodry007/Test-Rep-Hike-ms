using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Item
    {
        public Item()
        {
            Characters = new HashSet<Character>();
            PassiveUses = new HashSet<PassiveUse>();
            Uses = new HashSet<Use>();
        }

        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int? Mass { get; set; }
        public int? Volume { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<PassiveUse> PassiveUses { get; set; }
        public virtual ICollection<Use> Uses { get; set; }
    }
}
