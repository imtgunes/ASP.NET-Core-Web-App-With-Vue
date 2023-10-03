using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjeAkademisyenProfil.Models
{
    public class Info
    {
        [Key]
        public int InfoId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string InfoInstitute { get; set; }

        public string InfoResearch { get; set; }

        public string InfoBio { get; set; }

        public string InfoEmail { get; set; }

        public string InfoWebsite { get; set; }
        public string InfoAddress { get; set; }

    }
}
