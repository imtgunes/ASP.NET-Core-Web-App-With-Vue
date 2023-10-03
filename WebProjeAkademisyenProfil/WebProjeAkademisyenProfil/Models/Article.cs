using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string ArticleName { get; set; }

        public string ArticleIndex { get; set; }

        public string ArticleWriters { get; set; }

        public string ArticleSource { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ArticleDate { get; set; }
    }
}
