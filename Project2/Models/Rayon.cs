using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Models
{
    public partial class Rayon
    {
        public Rayon()
        {
            Elans = new HashSet<Elan>();
        }

        public int RayonId { get; set; }
        public int? RayonSeherId { get; set; }
        public string RayonAd { get; set; }

        public virtual Seher RayonSeher { get; set; }
        public virtual ICollection<Elan> Elans { get; set; }
    }
}
