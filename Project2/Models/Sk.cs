using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Models
{
    public partial class Sk
    {
        public Sk()
        {
            Elans = new HashSet<Elan>();
        }

        public int Skid { get; set; }
        public string Skad { get; set; }

        public virtual ICollection<Elan> Elans { get; set; }
    }
}
