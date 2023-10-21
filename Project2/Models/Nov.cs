using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Models
{
    public partial class Nov
    {
        public Nov()
        {
            Elans = new HashSet<Elan>();
        }

        public int NovId { get; set; }
        public string NovAd { get; set; }

        public virtual ICollection<Elan> Elans { get; set; }
    }
}
