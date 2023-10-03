using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string MembershipName { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? MembershipDateStart { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? MembershipDateEnd { get; set; }

        public string MembershipInfo { get; set; }
    }
}
