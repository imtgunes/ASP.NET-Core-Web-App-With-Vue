using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class MembershipController : Controller
    {
        private APSContext db = new APSContext();

        public IActionResult Membership()
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

        [HttpGet("Membership/Membership/{AcademicianId}")]
        public IEnumerable<Membership> GetMemberships(int AcademicianId)
        {
            return db.Memberships.Where(e => e.AcademicianId == AcademicianId).ToList();

        }

        [HttpGet("Membership/Membership/Membership/{membershipId}")]
        public IEnumerable<Membership> GetMembership(int membershipId)
        {
            return db.Memberships.Where(m => m.MembershipId == membershipId).ToList();

        }

        [HttpPut("Membership/Membership/Membership/")]
        public async Task<IActionResult> UpdateMembership([FromBody] Membership membership)
        {

            var member = await db.Memberships.FindAsync(membership.MembershipId);

            if (member == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(member.MembershipName) || member.MembershipName == " " || String.IsNullOrEmpty(member.MembershipInfo) || member.MembershipInfo == " " )
            {

                return NotFound();
            }
            else
            {
                member.MembershipName = membership.MembershipName;

                member.MembershipDateStart = membership.MembershipDateStart;

                member.MembershipDateEnd = membership.MembershipDateEnd;

                member.MembershipInfo = membership.MembershipInfo;

              


            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(member);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Membership/Membership/Membership/")]
        public async Task<IActionResult> AddMembership([FromBody] Membership membership)
        {

            try
            {
                db.Memberships.Add(membership);
                await db.SaveChangesAsync();
                return Ok(membership);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Membership/Membership/Membership/{id}")]
        public async Task<IActionResult> DeleteMembership(int id)
        {
            var member = await db.Memberships.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            try
            {
                db.Memberships.Remove(member);
                await db.SaveChangesAsync();
                return Ok(member);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }
    }
}
