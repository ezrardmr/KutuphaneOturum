using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneOturum.Controllers
{
    public class ProfilimAltModel
    {
        public kullanicilar kuser { get; set; }
        public profil pr { get; set; }
    }
    public class ProfilimModel
    {
        public List<ProfilimAltModel> pam { get; set; }
        public kullanicilar k1 { get; set; }
        public List<masalar> msl { get; set; }
        public List<profil> prfl { get; set; }
    }
    public class ProfilimController : Controller
    {
        KullaniciEntities db = new KullaniciEntities();
        // GET: Profilim
        public ActionResult Profilim()
        {
            Session["SayfaAdi"] = "Profilim";
            string username = Session["USERNAME"].ToString();
            if (username == null)
            {
                return RedirectToAction("Giris", "Giris");
            }

            kullanicilar user = db.kullanicilar.Where(w => w.username == username).FirstOrDefault();

            ProfilimModel asm = new ProfilimModel();
            profil p = new profil();
            var profil = db.profil.Where(w => w.usrid == user.id).ToList();

           


            List<ProfilimAltModel> asamlist = new List<ProfilimAltModel>();
            foreach (var item in profil)
            {
                ProfilimAltModel asam = new ProfilimAltModel();

                asam.pr = item;
                asam.kuser = item.kullanicilar;



                asamlist.Add(asam);
            }

            List<profil> profils = new List<profil>();

            // oturumlist = db.oturumSuresi.Where(w => w.userid == user.id).ToList();
            //oturumlist = db.masalar.Where(w => w.user_id == user.id).ToList();
            profils = db.profil.Where(w => w.usrid == user.id).ToList();
            asm.pam = asamlist;
            asm.prfl = profils;


            return View(asm);
        }
        
    }
}