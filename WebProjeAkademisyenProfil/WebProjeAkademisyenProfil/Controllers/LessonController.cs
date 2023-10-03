using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Reflection.Metadata;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class LessonController : Controller
    {
        private APSContext db = new APSContext();
        public IActionResult Lesson()
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

        [HttpGet("Lesson/Lesson/{AcademicianId}")]
        public IEnumerable<Lesson> GetLessons(int AcademicianId)
        {
            return db.Lessons.Where(e => e.AcademicianId == AcademicianId).ToList();

        }

        [HttpGet("Lesson/Lesson/Lesson/{lessonId}")]
        public IEnumerable<Lesson> GetLesson(int lessonId)
        {
            return db.Lessons.Where(e => e.LessonId == lessonId).ToList();

        }

        [HttpPut("Lesson/Lesson/Lesson/")]
        public async Task<IActionResult> UpdateLesson([FromBody] Lesson lesson)
        {
            
            var les = await db.Lessons.FindAsync(lesson.LessonId);

            if (les == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(lesson.LessonName) || les.LessonName == " " || String.IsNullOrEmpty(lesson.LessonType) || les.LessonType == " " || String.IsNullOrEmpty(lesson.lessonLanguage) || les.lessonLanguage == " " )
            {
                
                return NotFound();
            }
            else
            {
                 les.LessonName = lesson.LessonName;

                 les.LessonType = lesson.LessonType;

                 les.LessonDateStart = lesson.LessonDateStart;

                 les.LessonDateEnd = lesson.LessonDateEnd;

                 les.lessonLanguage = lesson.lessonLanguage;

                 les.lessonClass = lesson.lessonClass;
                
            }
            
            try
            {
                await db.SaveChangesAsync();
                return Ok(les);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Lesson/Lesson/Lesson/")]
        public async Task<IActionResult> AddLesson([FromBody] Lesson lesson)
        {
            
            try
            {
                db.Lessons.Add(lesson);
                await db.SaveChangesAsync();
                return Ok(lesson);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            
            
        }

        [HttpDelete("Lesson/Lesson/Lesson/{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var les = await db.Lessons.FindAsync(id);
            if (les == null)
            {
                return NotFound();
            }

            try
            {
                db.Lessons.Remove(les);
                await db.SaveChangesAsync();
                return Ok(les);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            
        }

    }
}
