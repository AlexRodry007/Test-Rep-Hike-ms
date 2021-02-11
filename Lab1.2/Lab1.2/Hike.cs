using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Hike
    {
        public Hike()
        {
            Characters = new HashSet<Character>();
        }

        public int Id { get; set; }
        public string HikeName { get; set; }
        public int? Path { get; set; }

        public virtual Path PathNavigation { get; set; }
        public virtual Path Path1 { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
    }
}
