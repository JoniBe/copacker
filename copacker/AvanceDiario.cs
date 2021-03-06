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
    
    public partial class AvanceDiario
    {
        public int idPromocion { get; set; }
        public System.DateTime Fecha { get; set; }
        public string idTurno { get; set; }
        public string idMesa { get; set; }
        public Nullable<decimal> UnidadesRealizadas { get; set; }
        public Nullable<decimal> MermaRealDelTurno { get; set; }
        public Nullable<decimal> HorasPerdidas { get; set; }
        public string idMotivoHorasPerdidas { get; set; }
        public string Observaciones { get; set; }
        public string Palletizado { get; set; }
        public string idUsuarioUltimaModificacion { get; set; }
    
        public virtual Mesa Mesa { get; set; }
        public virtual MotivosHorasPerdida MotivosHorasPerdida { get; set; }
        public virtual Promocione Promocione { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual Usuarios Usuario { get; set; }
    }
}
