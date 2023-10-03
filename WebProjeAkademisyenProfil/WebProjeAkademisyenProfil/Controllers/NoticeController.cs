using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class NoticeController : Controller
    {
        private APSContext db = new APSContext();

        public IActionResult Notice()
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
            ViewBag.Image = db.Academicians.Find(acdId).AcademicianImage.ToString();
            return View(db.Categories.ToList());

        }

        [HttpGet("Notice/Notice/{AcademicianId}")]
        public IEnumerable<Notice> GetNotices(int AcademicianId)
        {
            return db.Notices.Where(e => e.AcademicianId == AcademicianId).ToList();

        }

        [HttpGet("Notice/Notice/Notice/{noticeId}")]
        public IEnumerable<Notice> GetNotice(int noticeId)
        {
            return db.Notices.Where(n => n.NoticeId == noticeId).ToList();

        }

        [HttpPut("Notice/Notice/Notice/")]
        public async Task<IActionResult> UpdateNotice([FromBody] Notice notice)
        {

            var not = await db.Notices.FindAsync(notice.NoticeId);

            if (not == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(not.NoticeType) || not.NoticeType == " " || String.IsNullOrEmpty(not.NoticeInfo) || not.NoticeInfo == " ")
            {

                return NotFound();
            }
            else
            {
                not.NoticeType = notice.NoticeType;

                not.NoticeInfo = notice.NoticeInfo;


            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(not);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Notice/Notice/Notice/")]
        public async Task<IActionResult> AddNotice([FromBody] Notice notice)
        {

            try
            {
                db.Notices.Add(notice);
                await db.SaveChangesAsync();
                return Ok(notice);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Notice/Notice/Notice/{id}")]
        public async Task<IActionResult> DeleteNotice(int id)
        {
            var not = await db.Notices.FindAsync(id);
            if (not == null)
            {
                return NotFound();
            }

            try
            {
                db.Notices.Remove(not);
                await db.SaveChangesAsync();
                return Ok(not);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }
    }
}
