using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Academician
    {
        [Key]
        public int AcademicianId { get; set; }
        public string AcademicianName{ get; set; }
        public string AcademicianSurName { get; set; }

        public string AcademicianImage { get; set; }
    }
}
