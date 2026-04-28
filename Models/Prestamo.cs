using System;
using System.Collections.Generic;

namespace CalculadoraPrestamosApp.Models
{
    public class PrestamoRequest
    {
        public DateTime FechaNacimiento { get; set; }
        public decimal Monto { get; set; }
        public int Meses { get; set; }
    }

    public class PrestamoResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public decimal? Cuota { get; set; }
        public List<CuotaAmortizacion> TablaAmortizacion { get; set; } = new List<CuotaAmortizacion>();
    }

    public class CuotaAmortizacion
    {
        public int NumeroCuota { get; set; }
        public decimal Cuota { get; set; }
        public decimal Interes { get; set; }
        public decimal Capital { get; set; }
        public decimal BalanceRestante { get; set; }
    }
}
