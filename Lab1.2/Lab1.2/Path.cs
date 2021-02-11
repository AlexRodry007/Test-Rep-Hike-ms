using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Path
    {
        public Path()
        {
            Hikes = new HashSet<Hike>();
        }

        public int Id { get; set; }
        public string Coords { get; set; }

        public virtual MapX CoordsNavigation { get; set; }
        public virtual Hike IdNavigation { get; set; }
        public virtual ICollection<Hike> Hikes { get; set; }
    }
}
