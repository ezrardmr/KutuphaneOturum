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
    public class YardimlasmaModel
    {
        public kullanicilar k1 { get; set; }
        public List<yardim> yrdm { get; set; }
        public List<masalar> msl { get; set; }
    }
    public class YardimlasmaController : Controller
    {
        // [Audit]
        KullaniciEntities db = new KullaniciEntities();
        
        // GET: Yardimlasma
        public ActionResult Yardimlasma()
        {
           
            Session["SayfaAdi"] = "Yardimlasma";

            string username = Session["USERNAME"].ToString();
            if (username == null)
            {
                return RedirectToAction("Giris", "Giris");
            }
            kullanicilar user = db.kullanicilar.Where(w => w.username == username).FirstOrDefault();
            YardimlasmaModel ym = new YardimlasmaModel();
            List<masalar> yrdmlsm = new List<masalar>();
            yrdmlsm = db.masalar.Where(w => w.user_id == user.id).ToList();
            ym.k1 = user;
            ym.msl = yrdmlsm;

            return View(ym);
        }
        [HttpPost]
        public ActionResult YardimlasmaAction(string userName, HttpPostedFileBase file, string ders)
        {
            string usernamea = Session["USERNAME"].ToString();
            if (usernamea == null)
            {
                return RedirectToAction("Giris", "Giris");
            }
            kullanicilar user1 = new kullanicilar();
            try
            {
                user1 = db.kullanicilar.Where(w => w.username == usernamea).FirstOrDefault();
            }
            catch
            {
                user1 = null;
            }

            if (user1 == null)
            {
                return RedirectToAction("Giris", "Giris");
            }

            string un= Session["USERNAME"].ToString();

            string ders_konusu = ders;

           
            yardim y = new yardim();
            gonderim g = new gonderim();
            y.soran_id = db.kullanicilar.Where(w => w.username == un).FirstOrDefault().id;
            //y.tarih = DateTime.Now;
            g.gon_id = y.id;
            y.ders_turu = ders_konusu;
            db.yardim.Add(y);
            db.gonderim.Add(g);

            
            kullanicilar k2 = new kullanicilar();
            yardim ym = new yardim();
            gonderim gn = new gonderim();
            string koku = "../Files/" + un;

            string guid = Guid.NewGuid().ToString();
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        if (file.ContentType != "application/pdf")
                        {
                            return RedirectToAction("Yardimlasma", "Yardimlasma");
                            throw new Exception("İstenmeyen dosya formatı");
                        }
                    }
                    catch
                    {

                    }


                    string path = Server.MapPath(koku);

                    if (!Directory.Exists(path))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                    }

                    string dosyaad = guid + Path.GetExtension(file.FileName);

                    string kayityol = path + "/" + dosyaad;

                    file.SaveAs(kayityol);
                    //gn.gon_id = 0;
                    gn.csv_yolu = kayityol;

                    db.gonderim.Add(gn);
                }
                db.SaveChanges();
            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return RedirectToAction("Anasayfa", "Anasayfa");
        }
    }
}