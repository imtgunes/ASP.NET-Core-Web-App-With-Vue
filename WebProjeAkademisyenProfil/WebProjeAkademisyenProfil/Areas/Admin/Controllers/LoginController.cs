using Microsoft.AspNetCore.Mvc;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;
using WebProjeAkademisyenProfil.Security;

namespace WebProjeAkademisyenProfil.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        public string key = "྆嫓래묫㈿胅峧彠㺥焼鑪軓䎌井ኣ";
        private byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private APSContext db = new APSContext();
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin([FromBody] UserAdmin user)
        {
            if(String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
            {
                return Json(new { Result = false, Message = "Kullanıcı Adınızı veya Şifrenizi Girmediniz!" });
            }
            else
            {
                try
                {
                    Decrypt decryption = new Decrypt();
                    int userID = db.Users.Where(u => u.Email == user.Email).Select(u => u.UserAdminId).SingleOrDefault();
                    if (userID == 0)
                    {
                        return Json(new { Result = false, Message = "Kullanıcı Adınızı ya da Şifreyi hatalı girdiniz!" });
                    }
                    
                    string encryptedPassword = db.Users.Where(u => u.Email == user.Email).Select(u => u.Password).SingleOrDefault().ToString();
                    var decryptedPassword = decryption.DecryptAes(encryptedPassword, GetBytes(key), IV);

                    if (decryptedPassword == user.Password)
                    {

                        var kullanici = db.Users.Find(userID);

                        if (kullanici == null) return Json(new { Result = false, Message = "Kullanıcı Adınızı ya da Şifreyi hatalı girdiniz!" });

                        HttpContext.Session.SetInt32("Kullanici_ID", kullanici.UserAdminId);
                        HttpContext.Session.SetString("Ad", kullanici.Name);
                        HttpContext.Session.SetString("Soyad", kullanici.SurName);
                        HttpContext.Session.SetString("Resim", kullanici.Image);
                        return Json(new { Result = true, Message = "Başarıyla Giriş Yaptınız. Yönlendiriliyorsunuz...", url = "https://localhost:7210/Admin/HomeAdmin/HomeAdmin" });
                    }
                    else
                    {
                        return Json(new { Result = false, Message = "Kullanıcı Adınızı ya da Şifreyi hatalı girdiniz!" });
                    }

                    
                  

                    
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Bir hata oluştu lütfen tekrar deneyiniz" });
                }
            }
            


        }

        
        public IActionResult UserLogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
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
