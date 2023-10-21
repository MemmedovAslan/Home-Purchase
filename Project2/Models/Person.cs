using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Models
{
    public partial class Person
    {
        public Person()
        {
            Elans = new HashSet<Elan>();
            Reys = new HashSet<Rey>();
        }

        public int PersonId { get; set; }
        public int? PersonRolId { get; set; }
        public string PersonAd { get; set; }
        public string PersonSoyad { get; set; }
        public string PersonIstifadeciAdi { get; set; }
        public string PersonNomre { get; set; }
        public string PersonEmail { get; set; }
        public string PersonParol { get; set; }
        public bool? PersonStatus { get; set; }

        public virtual Rol PersonRol { get; set; }
        public virtual ICollection<Elan> Elans { get; set; }
        public virtual ICollection<Rey> Reys { get; set; }
    }
}
