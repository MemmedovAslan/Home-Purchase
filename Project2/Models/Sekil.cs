using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Models
{
    public partial class Sekil
    {
        public int SekilId { get; set; }
        public int? SekilElanId { get; set; }
        public string SekilAd { get; set; }

        public virtual Elan SekilElan { get; set; }
    }
}
