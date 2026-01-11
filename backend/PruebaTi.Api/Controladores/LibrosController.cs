using Microsoft.AspNetCore.Mvc;
using PruebaTi.Api.Aplicacion.Servicios;

namespace PruebaTi.Api.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly IServicioLibros _servicioLibros;

        public LibrosController(IServicioLibros servicioLibros)
        {
            _servicioLibros = servicioLibros;
        }

        // GET: /api/libros/buscar?texto=...
        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string texto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return BadRequest("El parámetro 'texto' es obligatorio.");
            }

            var libros = await _servicioLibros.BuscarAsync(texto, cancellationToken);
            return Ok(libros);
        }
    }
}
