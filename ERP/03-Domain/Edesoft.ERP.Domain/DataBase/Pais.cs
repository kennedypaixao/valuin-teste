//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Edesoft.ERP.Domain.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pais
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pais()
        {
            this.UF = new HashSet<UF>();
        }
    
        public System.Guid Id { get; set; }
        public string Nome { get; set; }
        public int CodigoIBGE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UF> UF { get; set; }
    }
}
