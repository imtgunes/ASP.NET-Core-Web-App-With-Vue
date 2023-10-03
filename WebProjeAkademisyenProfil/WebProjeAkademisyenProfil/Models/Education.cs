using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Education
    {
        [Key]
        public int EducationId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string EducationLevel { get; set; }

        public string EducationInfo { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? EducationDateStart { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? EducationDateEnd { get; set; }
    }
}
