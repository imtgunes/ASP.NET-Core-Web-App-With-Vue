using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class BookController : Controller
    {
        private APSContext db = new APSContext();
        public IActionResult Book()
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

        [HttpGet("Book/Book/{AcademicianId}")]
        public IEnumerable<Book> GetBooks(int AcademicianId)
        {
            return db.Books.Where(e => e.AcademicianId == AcademicianId).ToList();

        }

        [HttpGet("Book/Book/Book/{bookId}")]
        public IEnumerable<Book> GetBook(int bookId)
        {
            return db.Books.Where(b => b.BookId == bookId).ToList();

        }

        [HttpPut("Book/Book/Book/")]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {

            var bk = await db.Books.FindAsync(book.BookId);

            if (bk == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(bk.BookName) || bk.BookName == " " || String.IsNullOrEmpty(bk.BookType) || bk.BookType == " " || String.IsNullOrEmpty(bk.BookScope) || bk.BookScope == " " || String.IsNullOrEmpty(bk.BookAuthor) || bk.BookAuthor == " " || String.IsNullOrEmpty(bk.BookLanguage) || bk.BookLanguage == " " || String.IsNullOrEmpty(bk.BookISBN) || bk.BookISBN == " " )
            {

                return NotFound();
            }
            else
            {
                bk.BookName = book.BookName;

                bk.BookType = book.BookType;

                bk.BookScope = book.BookScope;

                bk.BookAuthor = book.BookAuthor;

                bk.BookDate = book.BookDate;

                bk.BookPage = book.BookPage;

                bk.BookLanguage = book.BookLanguage;

                bk.BookISBN = book.BookISBN;


            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(bk);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("Book/Book/Book/")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {

            try
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return Ok(book);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("Book/Book/Book/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            try
            {
                db.Books.Remove(book);
                await db.SaveChangesAsync();
                return Ok(book);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }
    }
}
