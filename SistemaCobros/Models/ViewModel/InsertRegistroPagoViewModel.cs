using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SistemaCobros.Models.ViewModel
{
    public class InsertRegistroPagoViewModel
    {
        public int Id { get; set; }
        [DisplayName("Monto")]
        public decimal Monto { get; set; }
        [DisplayName("Mora")]
        public decimal Mora { get; set; }
        [DisplayName("Fecha de Pago")]
        public DateTime Fecha_Pago { get; set; }
        [DisplayName("Tipo de Cartera")]
        public int Id_cartera { get; set; }
        [DisplayName("Tipo de Pago Realizado")]
        public int Id_tipo_pago { get; set; }
        
    }
}