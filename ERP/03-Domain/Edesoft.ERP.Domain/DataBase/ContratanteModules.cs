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
    
    public partial class ContratanteModules
    {
        public System.Guid IdContratante { get; set; }
        public System.Guid IdModule { get; set; }
        public bool Ativo { get; set; }
    
        public virtual Contratante Contratante { get; set; }
        public virtual Modules Modules { get; set; }
    }
}