using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Models
{
    public partial class Rey
    {
        public int ReyId { get; set; }
        public int? ReyElanId { get; set; }
        public int? ReyPersonId { get; set; }
        public string ReyOzu { get; set; }

        public virtual Elan ReyElan { get; set; }
        public virtual Person ReyPerson { get; set; }
    }
}
