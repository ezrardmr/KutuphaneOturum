using KutuphaneOturum.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneOturum.Controllers
{
    [Audit]
    public class AnaSayfaAltModel
    {
        public masalar masa { get; set; } 
        public kullanicilar klc { get; set; }
    }
    public class AnaSayfaModel
    {
        public List<AnaSayfaAltModel> usermasalari { get; set; }
        public kullanicilar kullanici { get; set; } 
        public List<masalar> msl{ get; set; }
    }
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        public ActionResult Anasayfa()
        {
            return View();
        }
    }
}