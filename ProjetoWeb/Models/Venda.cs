//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjetoWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venda()
        {
            this.VendaItem = new HashSet<VendaItem>();
        }
    
        public int IdVenda { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> DataVenda { get; set; }
        public Nullable<decimal> TotalVenda { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendaItem> VendaItem { get; set; }
    }
}
