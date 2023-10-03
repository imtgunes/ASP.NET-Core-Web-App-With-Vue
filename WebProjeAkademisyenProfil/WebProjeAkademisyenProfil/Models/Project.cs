using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string ProjectName{ get; set; }

        public string ProjectState { get; set; }

        public string ProjectInfo { get; set; }

        public string ProjectScope { get; set; }
    }
}
