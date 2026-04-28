using CalculadoraPrestamosApp.Models;
using CalculadoraPrestamosApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraPrestamosApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public CalculoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpPost("cuota")]
        public ActionResult<PrestamoResponse> CalcularCuota([FromBody] PrestamoRequest request)
        {
            if (request == null || request.Monto <= 0 || request.Meses <= 0 || request.FechaNacimiento == default)
            {
                return BadRequest(new PrestamoResponse { Exito = false, Mensaje = "Datos de entrada inválidos." });
            }

            string ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";
            var resultado = _prestamoService.CalcularPrestamo(request, ip);
            
            return Ok(resultado);
        }
    }
}