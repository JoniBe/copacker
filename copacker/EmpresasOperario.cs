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
    
    public partial class EmpresasOperario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmpresasOperario()
        {
            this.AvanceDiarioOperarios = new HashSet<AvanceDiarioOperarios>();
        }
    
        public string Legajo { get; set; }
        public string idEmpresa { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Documento { get; set; }
        public bool Activo { get; set; }
        public Nullable<System.DateTime> FechaVtoCarnetSalud { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public bool CompletoCuestionarioIngreso { get; set; }
        public bool CompletoCuestionarioEnfermedades { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AvanceDiarioOperarios> AvanceDiarioOperarios { get; set; }
    }
}
