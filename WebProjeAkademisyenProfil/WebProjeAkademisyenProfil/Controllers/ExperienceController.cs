using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class ExperienceController : Controller
    {
        private APSContext db = new APSContext();
        public IActionResult Experience()
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

        [HttpGet("Experience/Experience/{AcademicianId}")]
        public IEnumerable<Experience> GetExperiences(int AcademicianId)
        {
            return db.Experiences.Where(e => e.AcademicianId == AcademicianId).ToList();

        }

        [HttpGet("Experience/Experience/Experience/{experienceId}")]
        public IEnumerable<Experience> GetExperience(int experienceId)
        {
            return db.Experiences.Where(e => e.ExperienceId == experienceId).ToList();

        }

        [HttpPut("Experience/Experience/Experience/")]
        public async Task<IActionResult> UpdateExperience([FromBody] Experience experience)
        {

            var exp = await db.Experiences.FindAsync(experience.ExperienceId);

            if (exp == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(exp.ExperienceName) || exp.ExperienceName == " " )
            {

                return NotFound();
            }
            else
            {
                exp.ExperienceName = experience.ExperienceName;

                exp.ExperienceDateStart = experience.ExperienceDateStart;

                exp.ExperienceDateEnd = experience.ExperienceDateEnd;


            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(exp);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Experience/Experience/Experience/")]
        public async Task<IActionResult> AddExperience([FromBody] Experience experience)
        {

            try
            {
                db.Experiences.Add(experience);
                await db.SaveChangesAsync();
                return Ok(experience);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Experience/Experience/Experience/{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var exp = await db.Experiences.FindAsync(id);
            if (exp == null)
            {
                return NotFound();
            }

            try
            {
                db.Experiences.Remove(exp);
                await db.SaveChangesAsync();
                return Ok(exp);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }
    }
}
