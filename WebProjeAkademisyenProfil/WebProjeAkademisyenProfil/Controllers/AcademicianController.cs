using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class AcademicianController : Controller
    {
        private APSContext db = new APSContext();
        public IActionResult Academician()
        {
            
            return View();

        }
        [HttpGet("Academician/Academician/Academician")]
        public IEnumerable<Academician> GetAcademicians()
        {
            return db.Academicians.ToList();

        }

        [HttpGet("Academician/Academician/Academician/{academicianId}")]
        public IEnumerable<Academician> GetAcademician(int academicianId)
        {
            return db.Academicians.Where(a => a.AcademicianId == academicianId).ToList();

        }

        [HttpPut("Academician/Academician/Academician/")]
        public async Task<IActionResult> UpdateAcademician(Academician academician, IFormFile academicianImagePath)
        {

            var acd = await db.Academicians.FindAsync(academician.AcademicianId);

            if (acd == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(acd.AcademicianName) || acd.AcademicianName == " " || String.IsNullOrEmpty(acd.AcademicianSurName) || acd.AcademicianSurName == " " || String.IsNullOrEmpty(acd.AcademicianImage) || acd.AcademicianImage == " ")
            {

                return NotFound();
            }
            else
            {
                if (academicianImagePath == null)
                {
                    acd.AcademicianImage = academician.AcademicianImage;
                }
                else
                {
                    string imageExtension = Path.GetExtension(academicianImagePath.FileName);
                    string imageName = Guid.NewGuid() + imageExtension;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{imageName}");
                    using var stream = new FileStream(path, FileMode.Create);
                    await academicianImagePath.CopyToAsync(stream);


                    acd.AcademicianImage = "/images/" + imageName;
                }


                acd.AcademicianName = academician.AcademicianName;

                acd.AcademicianSurName = academician.AcademicianSurName;



            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(acd);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Academician/Academician/Academician/")]
        public async Task<IActionResult> AddAcademician(Academician academician, IFormFile academicianImagePath)
        {

            try
            {
                if(academicianImagePath == null)
                {
                    academician.AcademicianImage = "/images/defaultProfileImage.png";
                }
                else
                {
                    string imageExtension = Path.GetExtension(academicianImagePath.FileName);
                    string imageName = Guid.NewGuid() + imageExtension;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{imageName}");
                    using var stream = new FileStream(path, FileMode.Create);
                    await academicianImagePath.CopyToAsync(stream);
                    academician.AcademicianImage = "/images/" + imageName;
                }
                

                db.Academicians.Add(academician);
                await db.SaveChangesAsync();
                return Ok(academician);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Academician/Academician/Academician/{id}")]
        public async Task<IActionResult> DeleteAcademician(int id)
        {
            var acd = await db.Academicians.FindAsync(id);
            if (acd == null)
            {
                return NotFound();
            }

            try
            {
                if (!acd.AcademicianImage.Equals("/images/defaultProfileImage.png"))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{acd.AcademicianImage}");

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                

                db.Academicians.Remove(acd);
                await db.SaveChangesAsync();
                return Ok(acd);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }
    }
}
