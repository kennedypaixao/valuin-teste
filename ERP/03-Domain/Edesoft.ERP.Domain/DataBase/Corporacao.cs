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
    
    public partial class Corporacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Corporacao()
        {
            this.Contratante = new HashSet<Contratante>();
        }
    
        public System.Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contratante> Contratante { get; set; }
    }
}