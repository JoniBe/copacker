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
    
    public partial class FormulasKit
    {
        public string idKit { get; set; }
        public string SKU { get; set; }
        public Nullable<decimal> OrdenEnPantalla { get; set; }
        public Nullable<decimal> CantInsumoProducto { get; set; }
        public string idUsuarioUltimaModificacion { get; set; }
    
        public virtual Usuarios Usuario { get; set; }
    }
}
