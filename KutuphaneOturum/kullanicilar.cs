//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KutuphaneOturum
{
    using System;
    using System.Collections.Generic;
    
    public partial class kullanicilar
    {
        public int id { get; set; }
        public string username { get; set; }
        public string pass { get; set; }
    
        public virtual oturumSuresi oturumSuresi { get; set; }
    }
}