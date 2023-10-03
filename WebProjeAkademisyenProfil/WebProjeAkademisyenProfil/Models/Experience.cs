using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Experience
    {
        [Key]
        public int ExperienceId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string ExperienceName { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ExperienceDateStart { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ExperienceDateEnd { get; set; }

        
    }
}
