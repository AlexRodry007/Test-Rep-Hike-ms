using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Tile
    {
        public Tile()
        {
            Events = new HashSet<Event>();
        }

        public string Coords { get; set; }
        public int? Id { get; set; }
        public string TileName { get; set; }
        public int? Length { get; set; }

        public virtual MapX CoordsNavigation { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
