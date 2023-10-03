using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class InfoController : Controller
    {
        private APSContext db = new APSContext();
        public IActionResult Info()
        {
            return View();
        }

        [HttpGet("Info/{AcademicianId}")]
        public IEnumerable<Info> GetInfos(int AcademicianId)
        {
            return db.Infos.Where(e => e.AcademicianId == AcademicianId).ToList();

        }


        [HttpGet("Info/Info/Info/{infoId}")]
        public IEnumerable<Info> GetInfo(int infoId)
        {
            return db.Infos.Where(e => e.InfoId == infoId).ToList();

        }

        [HttpPut("Info/Info/Info/")]
        public async Task<IActionResult> UpdateInfo([FromBody] Info info)
        {

            var inf = await db.Infos.FindAsync(info.InfoId);

            if (inf == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(inf.InfoInstitute) || inf.InfoInstitute == " " || String.IsNullOrEmpty(inf.InfoResearch) || inf.InfoResearch == " " || String.IsNullOrEmpty(inf.InfoBio) || inf.InfoBio == " " || String.IsNullOrEmpty(inf.InfoEmail) || inf.InfoEmail == " " || String.IsNullOrEmpty(inf.InfoWebsite) || inf.InfoWebsite == " " || String.IsNullOrEmpty(inf.InfoAddress) || inf.InfoAddress == " " )
            {

                return NotFound();
            }
            else
            {
                inf.InfoInstitute = info.InfoInstitute;

                inf.InfoResearch = info.InfoResearch;

                inf.InfoBio = info.InfoBio;

                inf.InfoEmail = info.InfoEmail;

                inf.InfoWebsite = info.InfoWebsite;

                inf.InfoAddress = info.InfoAddress;

            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(inf);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Info/Info/Info/")]
        public async Task<IActionResult> AddInfo([FromBody] Info info)
        {

            try
            {
                db.Infos.Add(info);
                await db.SaveChangesAsync();
                return Ok(info);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Info/Info/Info/{id}")]
        public async Task<IActionResult> DeleteInfo(int id)
        {
            var inf = await db.Infos.FindAsync(id);
            if (inf == null)
            {
                return NotFound();
            }

            try
            {
                db.Infos.Remove(inf);
                await db.SaveChangesAsync();
                return Ok(inf);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }
    }
}
