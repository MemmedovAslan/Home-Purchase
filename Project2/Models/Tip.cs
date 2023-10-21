using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Models
{
    public partial class Tip
    {
        public Tip()
        {
            Elans = new HashSet<Elan>();
        }

        public int TipId { get; set; }
        public string TipAd { get; set; }

        public virtual ICollection<Elan> Elans { get; set; }
    }
}
