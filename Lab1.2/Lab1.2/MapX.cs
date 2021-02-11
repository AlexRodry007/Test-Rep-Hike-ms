using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class MapX
    {
        public MapX()
        {
            Paths = new HashSet<Path>();
        }

        public string YX { get; set; }
        public int? Y { get; set; }
        public int? X { get; set; }

        public virtual MapY YNavigation { get; set; }
        public virtual Tile Tile { get; set; }
        public virtual ICollection<Path> Paths { get; set; }
    }
}
