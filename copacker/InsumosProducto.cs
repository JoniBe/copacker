//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace copacker
{
    using System;
    using System.Collections.Generic;
    
    public partial class InsumosProducto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InsumosProducto()
        {
            this.StockAjustes = new HashSet<StockAjustes>();
        }
    
        public string SKU { get; set; }
        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }
        public bool EstaActivo { get; set; }
        public bool EsInsumo { get; set; }
        public decimal CantStock { get; set; }
        public string idUniMed { get; set; }
        public string idUsuarioUltimaModificacion { get; set; }
    
        public virtual UnidadMedida UnidadMedida { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockAjustes> StockAjustes { get; set; }
    }
}
