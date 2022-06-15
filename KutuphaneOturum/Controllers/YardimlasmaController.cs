//using KutuphaneOturum.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KutuphaneOturum.Controllers
{
    public class YardimlasmaAltModel
    {
       
        public yardim y { get; set; }
        public gonderim gndrm { get; set; }
    }
    public class YardimlasmaModel
    {
        public kullanicilar kl { get; set; }
        public List<YardimlasmaAltModel> yam { get; set; }
        public List<yardim> yrdm { get; set; }
        public List<gonderim> gonderims { get; set; }
        public List<masalar> msl { get; set; }
    }
    public class YardimlasmaController : Controller
    {
        // [Audit]

        KullaniciEntities1 db = new KullaniciEntities1();

        // GET: Yardimlasma
        public ActionResult Yardimlasma()
        {

            Session["SayfaAdi"] = "Yardimlasma";

            string username = Session["USERNAME"].ToString();
            if (username == null)
            {
                return RedirectToAction("Giris", "Giris");
            }
            
            kullanicilar u1 = db.kullanicilar.Where(w => w.username == username).FirstOrDefault();
            YardimlasmaModel y1 = new YardimlasmaModel();
            yardim yr = db.yardim.Where(w => w.soran_id == u1.id ).FirstOrDefault();
            List<masalar> yrdmlsm = new List<masalar>();
            yrdmlsm = db.masalar.Where(w => w.user_id == u1.id).ToList();

            
            var yardimlas = db.gonderim.Where(w => w.gon_id == yr.id).ToList();

            List<YardimlasmaAltModel> listt = new List<YardimlasmaAltModel>();
            foreach (var item in yardimlas)
            {
                YardimlasmaAltModel asam = new YardimlasmaAltModel();

                asam.gndrm = item;
                asam.y = item.yardim;



                listt.Add(asam);
            }
           
            List<gonderim> gonder = new List<gonderim>();

            gonder = db.gonderim.Where(w => w.gon_id == yr.id).ToList();
            y1.kl = u1;
            y1.msl = yrdmlsm;
            y1.yam = listt;
            y1.gonderims = gonder;

            return View(y1);
        }

    }
}