using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Notice
    {
        [Key]
        public int NoticeId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string NoticeType { get; set; }

        public string NoticeInfo { get; set; }
    }
}
