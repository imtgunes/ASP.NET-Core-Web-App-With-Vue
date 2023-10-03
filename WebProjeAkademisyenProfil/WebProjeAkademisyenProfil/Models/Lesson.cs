using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string LessonName { get; set; }

        public string LessonType { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? LessonDateStart { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? LessonDateEnd { get; set; }

        public string lessonLanguage { get; set; }

        public int lessonClass { get; set; }

        

       

    }
}
