//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class admin_loaihinhanh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public admin_loaihinhanh()
        {
            this.admin_hinhanh = new HashSet<admin_hinhanh>();
        }
    
        public int id { get; set; }
        public string tenloaihinhanh { get; set; }
        public Nullable<System.DateTime> ngaytao { get; set; }
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        public Nullable<int> nguoitao { get; set; }
        public Nullable<bool> hienthi { get; set; }
        public Nullable<bool> daxoa { get; set; }
        public string motahinhanh { get; set; }
        public string ghichu { get; set; }
        public string tenloaihinhanh_en { get; set; }
        public string motahinhanh_en { get; set; }
    
        public virtual admin_account admin_account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<admin_hinhanh> admin_hinhanh { get; set; }
    }
}
