//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Admin.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhanQuyen
    {
        public string UserName { get; set; }
        public string MaChucNang { get; set; }
        public string GhiChu { get; set; }
    
        public virtual ChucNang ChucNang { get; set; }
        public virtual User User { get; set; }
    }
}
