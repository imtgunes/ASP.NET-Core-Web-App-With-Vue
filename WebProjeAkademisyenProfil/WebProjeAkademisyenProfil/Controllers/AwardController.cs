using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class AwardController : Controller
    {
        private APSContext db = new APSContext();
        public IActionResult Award()
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

        [HttpGet("Award/Award/{AcademicianId}")]
        public IEnumerable<Award> GetAwards(int AcademicianId)
        {
            return db.Awards.Where(e => e.AcademicianId == AcademicianId).ToList();

        }

        [HttpGet("Award/Award/Award/{awardId}")]
        public IEnumerable<Award> GetAward(int awardId)
        {
            return db.Awards.Where(a => a.AwardId == awardId).ToList();

        }

        [HttpPut("Award/Award/Award/")]
        public async Task<IActionResult> UpdateAward([FromBody] Award award)
        {

            var awd = await db.Awards.FindAsync(award.AwardId);

            if (awd == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(awd.AwardName) || awd.AwardName == " " || String.IsNullOrEmpty(awd.AwardInstitution) || awd.AwardInstitution == " " || String.IsNullOrEmpty(awd.AwardCountry) || awd.AwardCountry == " " )
            {

                return NotFound();
            }
            else
            {
                awd.AwardName = award.AwardName;

                awd.AwardDate = award.AwardDate;

                awd.AwardInstitution = award.AwardInstitution;

                awd.AwardCountry = award.AwardCountry;

               

            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(awd);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Award/Award/Award/")]
        public async Task<IActionResult> AddAward([FromBody] Award award)
        {

            try
            {
                db.Awards.Add(award);
                await db.SaveChangesAsync();
                return Ok(award);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Award/Award/Award/{id}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            var awd = await db.Awards.FindAsync(id);
            if (awd == null)
            {
                return NotFound();
            }

            try
            {
                db.Awards.Remove(awd);
                await db.SaveChangesAsync();
                return Ok(awd);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }
    }
}
