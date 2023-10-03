using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class EducationController : Controller
    {
        private APSContext db = new APSContext();

        public IActionResult Education()
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
        
        [HttpGet("Education/Education/{AcademicianId}")]
        public IEnumerable<Education> GetEducationInfo(int AcademicianId)
        {
            return db.Educations.Where(e => e.AcademicianId == AcademicianId).ToList(); 
            
        }


        [HttpGet("Education/Education/Education/{educationId}")]
        public IEnumerable<Education> GetEducation(int educationId)
        {
            return db.Educations.Where(e => e.EducationId == educationId).ToList();

        }

        [HttpPut("Education/Education/Education/")]
        public async Task<IActionResult> UpdateEducation([FromBody] Education education)
        {

            var ed = await db.Educations.FindAsync(education.EducationId);

            if (ed == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(ed.EducationLevel) || ed.EducationLevel == " " || String.IsNullOrEmpty(ed.EducationInfo) || ed.EducationInfo == " " )
            {

                return NotFound();
            }
            else
            {
                ed.EducationLevel = education.EducationLevel;

                ed.EducationInfo = education.EducationInfo;

                ed.EducationDateStart = education.EducationDateStart;

                ed.EducationDateEnd = education.EducationDateEnd;



            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(ed);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Education/Education/Education/")]
        public async Task<IActionResult> AddEducation([FromBody] Education education)
        {

            try
            {
                db.Educations.Add(education);
                await db.SaveChangesAsync();
                return Ok(education);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Education/Education/Education/{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var ed = await db.Educations.FindAsync(id);
            if (ed == null)
            {
                return NotFound();
            }

            try
            {
                db.Educations.Remove(ed);
                await db.SaveChangesAsync();
                return Ok(ed);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }
    }
}
