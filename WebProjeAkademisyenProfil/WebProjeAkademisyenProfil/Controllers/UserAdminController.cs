using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.X509Certificates;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;
using WebProjeAkademisyenProfil.Security;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class UserAdminController : Controller
    {
        public string key = "྆嫓래묫㈿胅峧彠㺥焼鑪軓䎌井ኣ";
        private byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private APSContext db = new APSContext();
        public IActionResult UserAdmin()
        {
            return View();
        }

        [HttpGet("UserAdmin/UserAdmin/UserAdmin")]
        public IEnumerable<UserAdmin> GetUserAdmins()
        {
            return db.Users.ToList();

        }

        [HttpGet("UserAdmin/UserAdmin/UserAdmin/{useradminId}")]
        public IEnumerable<UserAdmin> GetUserAdmin(int useradminId)
        {
            return db.Users.Where(u => u.UserAdminId == useradminId).ToList();

        }

        [HttpPut("UserAdmin/UserAdmin/UserAdmin/")]
        public async Task<IActionResult> UpdateUserAdmin(UserAdmin userAdmin, IFormFile userAdminImagePath)
        {

            var  usad = await db.Users.FindAsync(userAdmin.UserAdminId);

            if (usad == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(usad.Name) || usad.Name == " " || String.IsNullOrEmpty(usad.Email) || usad.Email == " " || String.IsNullOrEmpty(usad.Image) || usad.Image == " " || String.IsNullOrEmpty(usad.SurName) || usad.SurName == " ")
            {

                return NotFound();
            }
            else
            {
                if (userAdminImagePath == null)
                {
                    usad.Image = userAdmin.Image;
                }
                else
                {
                    string imageExtension = Path.GetExtension(userAdminImagePath.FileName);
                    string imageName = Guid.NewGuid() + imageExtension;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{imageName}");
                    using var stream = new FileStream(path, FileMode.Create);
                    await userAdminImagePath.CopyToAsync(stream);
                    usad.Image = "/images/" + imageName;
                }


                usad.Name = userAdmin.Name;

                usad.SurName = userAdmin.SurName;

                usad.Email = userAdmin.Email;

                Encrypt encryption = new Encrypt();

                usad.Password = encryption.EncryptAes(userAdmin.Password, GetBytes(key), IV);


            }

            try
            {
                await db.SaveChangesAsync();
                return Ok(usad);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }


        }

        [HttpPost("UserAdmin/UserAdmin/UserAdmin/")]
        public async Task<IActionResult> AddUserAdmin(UserAdmin userAdmin, IFormFile userAdminImagePath)
        {

            try
            {
                if (userAdminImagePath == null)
                {
                    userAdmin.Image = "/images/defaultProfileImage.png";
                }
                else
                {
                    string imageExtension = Path.GetExtension(userAdminImagePath.FileName);
                    string imageName = Guid.NewGuid() + imageExtension;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{imageName}");
                    using var stream = new FileStream(path, FileMode.Create);
                    await userAdminImagePath.CopyToAsync(stream);
                    userAdmin.Image = "/images/" + imageName;
                }

               
                Encrypt encryption = new Encrypt();

                userAdmin.Password = encryption.EncryptAes(userAdmin.Password, GetBytes(key), IV);

                db.Users.Add(userAdmin);
                await db.SaveChangesAsync();
                return Ok(userAdmin);
            }
            catch (Exception e)
            {
                return NotFound();
            }


        }

        [HttpDelete("UserAdmin/UserAdmin/UserAdmin/{id}")]
        public async Task<IActionResult> DeleteUserAdmin(int id)
        {
            var usad = await db.Users.FindAsync(id);
            if (usad == null)
            {
                return NotFound();
            }

            try
            {
                if (!usad.Image.Equals("/images/defaultProfileImage.png"))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{usad.Image}");

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }


                db.Users.Remove(usad);
                await db.SaveChangesAsync();
                return Ok(usad);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
