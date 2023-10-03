using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProjeAkademisyenProfil.Models
{
    public class Certificate
    {
        [Key]
        public int CertificateId { get; set; }

        [ForeignKey("Academician")]
        public int AcademicianId { get; set; }

        public string CertificateScope { get; set; }

        public string CertificateName { get; set; }

        public string CertificateInfo { get; set; }



        [Column(TypeName = "Date")]
        public DateTime CertificateDateStart { get; set; }

        [Column(TypeName = "Date")]
        public DateTime CertificateDateEnd { get; set; }
    }
}
