using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class ProjectController : Controller
    {
        private APSContext db = new APSContext();

        public IActionResult Project()
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

        [HttpGet("Project/Project/{AcademicianId}")]
        public IEnumerable<Project> GetProjects(int AcademicianId)
        {
            return db.Projects.Where(e => e.AcademicianId == AcademicianId).ToList();

        }

        [HttpGet("Project/Project/Project/{projectId}")]
        public IEnumerable<Project> GetProject(int projectId)
        {
            return db.Projects.Where(p => p.ProjectId == projectId).ToList();

        }

        [HttpPut("Project/Project/Project/")]
        public async Task<IActionResult> UpdateProject([FromBody] Project project)
        {

            var prj = await db.Projects.FindAsync(project.ProjectId);

            if (prj == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(prj.ProjectName) || prj.ProjectName == " " || String.IsNullOrEmpty(prj.ProjectInfo) || prj.ProjectInfo == " " || String.IsNullOrEmpty(prj.ProjectState) || prj.ProjectState == " " || String.IsNullOrEmpty(prj.ProjectScope) || prj.ProjectScope == " ")
            {

                return NotFound();
            }
            else
            {
                prj.ProjectName = project.ProjectName;

                prj.ProjectState = project.ProjectState;

                prj.ProjectInfo = project.ProjectInfo;

                prj.ProjectScope = project.ProjectScope;



            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(prj);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Project/Project/Project/")]
        public async Task<IActionResult> AddProject([FromBody] Project project)
        {

            try
            {
                db.Projects.Add(project);
                await db.SaveChangesAsync();
                return Ok(project);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Project/Project/Project/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var prj = await db.Projects.FindAsync(id);
            if (prj == null)
            {
                return NotFound();
            }

            try
            {
                db.Projects.Remove(prj);
                await db.SaveChangesAsync();
                return Ok(prj);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

    }


}
