using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Models
{
    public partial class Seher
    {
        public Seher()
        {
            Elans = new HashSet<Elan>();
            Rayons = new HashSet<Rayon>();
        }

        public int SeherId { get; set; }
        public string SeherAd { get; set; }

        public virtual ICollection<Elan> Elans { get; set; }
        public virtual ICollection<Rayon> Rayons { get; set; }
    }
}
