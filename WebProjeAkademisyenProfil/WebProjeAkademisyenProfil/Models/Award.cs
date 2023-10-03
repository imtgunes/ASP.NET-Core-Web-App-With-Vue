using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Award
    {
        [Key]
        public int AwardId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string AwardName { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? AwardDate { get; set; }
       
        public string AwardInstitution { get; set; }

        public string AwardCountry { get; set; }

    }
}
