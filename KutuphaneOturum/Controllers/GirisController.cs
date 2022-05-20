//using KutuphaneOturum.Classes;
using KutuphaneOturum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneOturum.Controllers
{
   // [Audit]
    public class GirisController : Controller
    {
        // GET: Giris
        KullaniciEntities1 K = new KullaniciEntities1();
        public ActionResult Giris()
        {
            Session["SayfaAdi"] = "Giriş";
            return View();
        }

        public ActionResult Cikis()
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
                
                    Session["UserName"] = giris.username;
                    Session["Password"] = giris.pass;

                    return RedirectToAction("AnaSayfa", "AnaSayfa");
 
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