using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Models
{
    public partial class Rol
    {
        public Rol()
        {
            People = new HashSet<Person>();
        }

        public int RolId { get; set; }
        public string RolAd { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
