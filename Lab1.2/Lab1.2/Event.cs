using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1._2
{
    public partial class Event
    {
        public Event()
        {
            Choices = new HashSet<Choice>();
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public int? Safeness { get; set; }
        public string Tile { get; set; }

        public virtual Tile TileNavigation { get; set; }
        public virtual ICollection<Choice> Choices { get; set; }
    }
}
