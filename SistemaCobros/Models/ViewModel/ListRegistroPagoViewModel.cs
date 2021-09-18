using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCobros.Models.ViewModel
{
    public class ListRegistroPagoViewModel
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public decimal Mora { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public int Id_cartera { get; set; }
        public int Id_tipo_pago { get; set; }

    }
}