using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class ArticleController : Controller
    {
        private APSContext db = new APSContext();
        public IActionResult Article()
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

        [HttpGet("Article/Article/{AcademicianId}")]
        public IEnumerable<Article> GetArticles(int AcademicianId)
        {
            return db.Articles.Where(e => e.AcademicianId == AcademicianId).ToList();

        }


        [HttpGet("Article/Article/Article/{articleId}")]
        public IEnumerable<Article> GetArticle(int articleId)
        {
            return db.Articles.Where(e => e.ArticleId == articleId).ToList();

        }

        [HttpPut("Article/Article/Article/")]
        public async Task<IActionResult> UpdateArticle([FromBody] Article article)
        {

            var art = await db.Articles.FindAsync(article.ArticleId);

            if (art == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(art.ArticleName) || art.ArticleName == " " || String.IsNullOrEmpty(art.ArticleWriters) || art.ArticleWriters == " " || String.IsNullOrEmpty(art.ArticleIndex) || art.ArticleIndex == " " || String.IsNullOrEmpty(art.ArticleSource) || art.ArticleSource == " ")
            {

                return NotFound();
            }
            else
            {
                art.ArticleName = article.ArticleName;

                art.ArticleWriters = article.ArticleWriters;

                art.ArticleSource = article.ArticleSource;

                art.ArticleDate = article.ArticleDate;

                art.ArticleIndex = article.ArticleIndex;


            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(art);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Article/Article/Article/")]
        public async Task<IActionResult> AddArticle([FromBody] Article article)
        {

            try
            {
                db.Articles.Add(article);
                await db.SaveChangesAsync();
                return Ok(article);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Article/Article/Article/{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var art = await db.Articles.FindAsync(id);
            if (art == null)
            {
                return NotFound();
            }

            try
            {
                db.Articles.Remove(art);
                await db.SaveChangesAsync();
                return Ok(art);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

    }
}
