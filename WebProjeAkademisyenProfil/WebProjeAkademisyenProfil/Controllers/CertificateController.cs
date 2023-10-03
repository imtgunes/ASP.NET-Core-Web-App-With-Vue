using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class CertificateController : Controller
    {
        private APSContext db = new APSContext();
        public IActionResult Certificate()
        {
            string userId = Request.Cookies["userID"];
            int acdId;
            if (!String.IsNullOrEmpty(userId))
            {
                acdId = int.Parse(userId);
            }
            else
            {
                acdId = 1;
            }

            ViewBag.Name = db.Academicians.Find(acdId).AcademicianName.ToString();
            ViewBag.SurName = db.Academicians.Find(acdId).AcademicianSurName.ToString();
            ViewBag.Image =  db.Academicians.Find(acdId).AcademicianImage.ToString();
            return View(db.Categories.ToList());
        }

        [HttpGet("Certificate/Certificate/{AcademicianId}")]
        public IEnumerable<Certificate> GetCertificates(int AcademicianId)
        {
            return db.Certificates.Where(e => e.AcademicianId == AcademicianId).ToList();

        }

        [HttpGet("Certificate/Certificate/Certificate/{certificateId}")]
        public IEnumerable<Certificate> GetCertificate(int certificateId)
        {
            return db.Certificates.Where(c => c.CertificateId == certificateId).ToList();

        }

        [HttpPut("Certificate/Certificate/Certificate/")]
        public async Task<IActionResult> UpdateCertificate([FromBody] Certificate certificate)
        {

            var crt = await db.Certificates.FindAsync(certificate.CertificateId);

            if (crt == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(crt.CertificateScope) || crt.CertificateScope == " " || String.IsNullOrEmpty(crt.CertificateName) || crt.CertificateName == " " || String.IsNullOrEmpty(crt.CertificateInfo) || crt.CertificateInfo == " " )
            {

                return NotFound();
            }
            else
            {
                crt.CertificateScope = certificate.CertificateScope;

                crt.CertificateName = certificate.CertificateName;

                crt.CertificateInfo = certificate.CertificateInfo;

                crt.CertificateDateStart = certificate.CertificateDateStart;

                crt.CertificateDateEnd = certificate.CertificateDateEnd;


            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(crt);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Certificate/Certificate/Certificate/")]
        public async Task<IActionResult> AddCertificate([FromBody] Certificate certificate)
        {

            try
            {
                db.Certificates.Add(certificate);
                await db.SaveChangesAsync();
                return Ok(certificate);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Certificate/Certificate/Certificate/{id}")]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var crt = await db.Certificates.FindAsync(id);
            if (crt == null)
            {
                return NotFound();
            }

            try
            {
                db.Certificates.Remove(crt);
                await db.SaveChangesAsync();
                return Ok(crt);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }
    }
}
