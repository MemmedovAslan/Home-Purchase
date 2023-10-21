using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Models
{
    public partial class Elan
    {
        public Elan()
        {
            Reys = new HashSet<Rey>();
            Sekils = new HashSet<Sekil>();
        }

        public int ElanId { get; set; }
        public int? ElanNovId { get; set; }
        public int? ElanPersonId { get; set; }
        public int? ElanSkid { get; set; }
        public int? ElanTipId { get; set; }
        public int? ElanSeherId { get; set; }
        public int? ElanRayonId { get; set; }
        public string ElanUnvan { get; set; }
        public float? ElanSahe { get; set; }
        public string ElanMelumat { get; set; }
        public long? ElanQiymet { get; set; }
        public int? ElanOtaqsayi { get; set; }
        public int? ElanMertebesayi { get; set; }
        public int? ElanUmumiMertebesayi { get; set; }
        public DateTime? ElanVaxt { get; set; }
        public bool? ElanInternet { get; set; }
        public bool? ElanLift { get; set; }
        public bool? ElanQaz { get; set; }
        public bool? ElanKupça { get; set; }
        public bool? ElanTelefon { get; set; }
        public bool? ElanKombi { get; set; }
        public bool? ElanKondisioner { get; set; }
        public bool? ElanTemir { get; set; }
        public bool? ElanStatus { get; set; }
        public bool? ElanAktivlik { get; set; }

        public virtual Nov ElanNov { get; set; }
        public virtual Person ElanPerson { get; set; }
        public virtual Rayon ElanRayon { get; set; }
        public virtual Seher ElanSeher { get; set; }
        public virtual Sk ElanSk { get; set; }
        public virtual Tip ElanTip { get; set; }
        public virtual ICollection<Rey> Reys { get; set; }
        public virtual ICollection<Sekil> Sekils { get; set; }
    }
}
