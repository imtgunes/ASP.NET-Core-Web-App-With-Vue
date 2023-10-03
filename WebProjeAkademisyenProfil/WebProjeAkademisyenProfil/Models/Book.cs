using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string BookName { get; set; }

        public string BookType{ get; set; }

        public string BookScope { get; set; }

        public string BookAuthor { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? BookDate { get; set; }

        public int BookPage { get; set; }

        public string BookLanguage { get; set; }

        public string BookISBN { get; set; }

    }
}
