//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaCobros.Models.ViewModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Detalle_Gestion
    {
        public int id_detalle_gestion { get; set; }
        public string descripcion { get; set; }
        public System.DateTime fecha { get; set; }
        public System.TimeSpan hora { get; set; }
        public int id_gestion { get; set; }
        public int id_tipo_gestion { get; set; }
        public string comentario { get; set; }
    
        public virtual Gestion Gestion { get; set; }
        public virtual Tipo_Gestion Tipo_Gestion { get; set; }
    }
}
