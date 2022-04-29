using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace KutuphaneOturum.Classes
{
    public class AuditAttribute : ActionFilterAttribute
    {
        public class Audit
        {
            public string Guid { get; set; }
            public string UserName { get; set; }
            public string IPAddress { get; set; }
            public string AreaAccessed { get; set; }
            public string Timestamp { get; set; }
            public string Browser { get; set; }

            public Audit() { }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            Audit audit = new Audit()
            {
                Guid = Guid.NewGuid().ToString(),
                UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                Browser = request.Browser.Browser,
                AreaAccessed = request.RawUrl,
                Timestamp = DateTime.Now.ToString()
            };

            string dosyaadi = DateTime.Now.ToString("yyyyMMdd") + ".log";
            string kokyolu = Convert.ToString(ConfigurationManager.AppSettings["FilePath"]);

            string jsonlog = @"{""Guid"": """ + audit.Guid + @""",""UserName"": """ + audit.UserName + @""",""IPAddress"": """ + audit.IPAddress + @""",""Timestamp"": """ + audit.Timestamp + @""",""AreaAccessed"": """ + audit.AreaAccessed + @""",""Browser"": """ + audit.Browser + @"""}";

            FileStream fs = new FileStream(kokyolu + "Logs/" + dosyaadi, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();
            File.AppendAllText(kokyolu + "Logs/" + dosyaadi, Environment.NewLine + jsonlog);
            base.OnActionExecuting(filterContext);

        }
    }
}