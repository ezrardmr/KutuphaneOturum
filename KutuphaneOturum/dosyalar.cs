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
    
    public partial class dosyalar
    {
        public int dosyaid { get; set; }
        public string usern { get; set; }
        public string yol { get; set; }
    
        public virtual kullanicilar kullanicilar { get; set; }
    }
}
