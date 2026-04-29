using CalculadoraPrestamosApp.Models;
using CalculadoraPrestamosApp.Data;

namespace CalculadoraPrestamosApp.Services
{
    public interface IPrestamoService
    {
        PrestamoResponse CalcularPrestamo(PrestamoRequest request, string ipCliente);
    }

    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _repo;

        public PrestamoService(IPrestamoRepository repo)
        {
            _repo = repo;
        }

        public int CalcularEdad(DateTime fechaNacimiento)
        {
            var today = DateTime.Today;
            int edad = today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > today.AddYears(-edad)) edad--;
            return edad;
        }

        public PrestamoResponse CalcularPrestamo(PrestamoRequest request, string ipCliente)
        {
            int edad = CalcularEdad(request.FechaNacimiento);

            if (edad < 18)
                return new PrestamoResponse { Exito = false, Mensaje = "Lo sentimos, aún no cuenta con la edad para solicitar este producto." };
            
            if (edad > 25)
                return new PrestamoResponse { Exito = false, Mensaje = "Favor pasar por una de nuestras sucursales para evaluar su caso." };

            if (!_repo.ValidarPlazo(request.Meses))
                return new PrestamoResponse { Exito = false, Mensaje = "Plazo de meses no válido. Solo se permiten 3, 6, 9 o 12 meses." };

            decimal? tasa = _repo.ObtenerTasaPorEdad(edad);
            if (!tasa.HasValue)
                return new PrestamoResponse { Exito = false, Mensaje = "No hay tasa definida para esta edad." };

            // Cálculo de cuota simple (Interés global / meses)
            // Opcional: Podríamos usar la fórmula de amortización francesa si se prefiere.
            // Según la lógica previa: cuota = (monto * tasa) / meses
            decimal cuota = (request.Monto * tasa.Value) / request.Meses;
            cuota = Math.Round(cuota, 2);

            var response = new PrestamoResponse
            {
                Exito = true,
                Mensaje = "Cálculo exitoso",
                Cuota = cuota
            };

            // Generar tabla de amortización (Reglas estrictas)
            decimal montoTotal = request.Monto * tasa.Value;
            decimal balance = montoTotal;
            for (int i = 1; i <= request.Meses; i++)
            {
                decimal cuotaActual = cuota;
                
                // Ajustar la última cuota para evitar errores de redondeo
                if (i == request.Meses)
                {
                    cuotaActual = Math.Round(balance, 2);
                }

                balance -= cuotaActual;

                response.TablaAmortizacion.Add(new CuotaAmortizacion
                {
                    NumeroCuota = i,
                    Cuota = cuotaActual,
                    BalanceRestante = Math.Max(0, Math.Round(balance, 2))
                });
            }

            _repo.InsertarLog(edad, request.Monto, request.Meses, cuota, ipCliente);
            
            return response;
        }
    }
}
