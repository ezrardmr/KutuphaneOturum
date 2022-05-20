//using KutuphaneOturum.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneOturum.Controllers
{
    //[Audit]
    public class AnaSayfaAltModel
    {
        public masalar masa { get; set; } 
        public kullanicilar klc { get; set; }
        public oturumSuresi ots { get; set; }
    }
    public class AnaSayfaModel
    {
        public List<AnaSayfaAltModel> usermasalari { get; set; }
        public kullanicilar kullanici { get; set; } 
        public List<oturumSuresi> otrms { get; set; }
        public List<masalar> msl{ get; set; }
    }
    public class AnasayfaController : Controller
    {
        KullaniciEntities1 db = new KullaniciEntities1();
        // GET: Anasayfa
        public ActionResult Anasayfa()
        {
            Session["SayfaAdi"] = "Anasayfa";

            string username = Session["USERNAME"].ToString();
            if (username == null)
            {
                return RedirectToAction("Giris", "Giris");
            }
            
            kullanicilar user = db.kullanicilar.Where(w => w.username == username).FirstOrDefault();

            AnaSayfaModel asm = new AnaSayfaModel();
            oturumSuresi otrms = new oturumSuresi();
            var otrm = db.masalar.Where(w => w.user_id == user.id).ToList();


            List<AnaSayfaAltModel> asamlist = new List<AnaSayfaAltModel>();
            foreach (var item in otrm)
            {
                AnaSayfaAltModel asam = new AnaSayfaAltModel();

                asam.masa = item;
                asam.klc = item.kullanicilar;
               
                

                asamlist.Add(asam);
            }
            
            List<masalar> oturumlist = new List<masalar>();

            // oturumlist = db.oturumSuresi.Where(w => w.userid == user.id).ToList();
            oturumlist = db.masalar.Where(w => w.user_id == user.id).ToList();
            asm.usermasalari = asamlist;
            asm.msl = oturumlist;
            return View(asm);
        }
    }
}