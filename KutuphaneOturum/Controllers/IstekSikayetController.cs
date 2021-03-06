using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneOturum.Controllers
{
    public class IstekSikayetModel
    {
        public kullanicilar kl { get; set; }
        public masalar m { get; set; }
        public oturumSuresi os { get; set; }
        public List<masalar> msl { get; set; }
        public yorumlar yrm { get; set; }

    }
    public class IstekSikayetController : Controller
    {
        KullaniciEntities1 db = new KullaniciEntities1();
        // GET: İstekSikayet
        public ActionResult IstekSikayet()
        {
            Session["SayfaAdi"] = "İstek Ve Şikayet";

            string username = Session["USERNAME"].ToString();
            if (username == null)
            {
                return RedirectToAction("Giris", "Giris");
            }
            kullanicilar user = db.kullanicilar.Where(w => w.username == username).FirstOrDefault();
            IstekSikayetModel ym = new IstekSikayetModel();
            List<masalar> yrdmlsm = new List<masalar>();
            yrdmlsm = db.masalar.Where(w => w.user_id == user.id).ToList();
            ym.kl = user;
            ym.msl = yrdmlsm;
            
            return View(ym);
        }
        [HttpPost]
        public ActionResult IstekSikayetAction(string usrN, string yorum )
        {
            string username = Session["USERNAME"].ToString();
            if (username == null)
            {
                return RedirectToAction("Giris", "Giris");
            }

            kullanicilar user1 = new kullanicilar();
            try
            {
                user1 = db.kullanicilar.Where(w => w.username == usrN).FirstOrDefault();
            }
            catch
            {
                user1 = null;
            }

            if (user1 == null)
            {
                return RedirectToAction("Giris", "Giris");
            }

            ViewBag.usr1 = user1;
            string yrm = yorum;
            yorumlar y = new yorumlar();
            kullanicilar k1 = new kullanicilar();
            y.yorum_id = db.kullanicilar.Where(w => w.username == usrN).FirstOrDefault().id;
            
            y.yorum = yrm;
            y.zaman = DateTime.Now;
            db.yorumlar.Add(y);
            
            db.SaveChanges();
            return RedirectToAction("Anasayfa", "Anasayfa");
        }
    }
}