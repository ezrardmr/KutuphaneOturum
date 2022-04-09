using KutuphaneOturum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneOturum.Controllers
{
    public class GirisController : Controller
    {
        // GET: Giris
        KullaniciEntities K = new KullaniciEntities();
        public ActionResult Giris()
        {
            Session["SayfaAdi"] = "Giriş";
            return View();
        }

        public ActionResult Cıkıs()
        {
            Session.RemoveAll();
            return RedirectToAction("Giris");
        }

        [HttpPost]
        public ActionResult GirisAction(string usernamea, string password)
        {
            if (usernamea.Length > 12 || password.Length > 20)
            {
                Session["Uyari"] = "Kullanıcı adı veya şifreniz hatalı. Lütfen tekrar deneyin.";
            }

            var pss = Hash.md5(password);

            kullanicilar giris = new kullanicilar();

            try
            {
                giris = K.kullanicilar.Where(w => w.username == usernamea && w.pass == password).FirstOrDefault();
            }
            catch (Exception ex)
            {
                giris = null;
            }




            if (giris != null)
            {

                {

                }
                
                if (giris == null)
                {
                    Session["Uyari"] = "Kullanıcı bilgilerine erişilemedi. Lütfen tekrar deneyin. Sorun devam ederse yönetici ile iletişime geçin.";

                    return RedirectToAction("Giris");
                }  
            }
            else
            {
                Session["Uyari"] = "Kullanıcı adı veya şifreniz hatalı. Lütfen tekrar deneyin.";
                return RedirectToAction("Giris");
            }

            return RedirectToAction("Giris", "Giris");
        }
    
    }
}