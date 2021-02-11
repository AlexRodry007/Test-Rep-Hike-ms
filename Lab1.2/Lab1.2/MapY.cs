using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class MapY
    {
        public MapY()
        {
            MapXes = new HashSet<MapX>();
        }

        public int Y { get; set; }

        public virtual ICollection<MapX> MapXes { get; set; }
    }
}
