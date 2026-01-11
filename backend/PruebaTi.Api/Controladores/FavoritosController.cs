using Microsoft.AspNetCore.Mvc;
using PruebaTi.Api.Aplicacion.Dto;
using PruebaTi.Api.Aplicacion.Servicios;

namespace PruebaTi.Api.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritosController : ControllerBase
    {
        private readonly IServicioFavoritos _servicioFavoritos;

        // Usuario fijo para simplificar (UserId = 1)
        private const int UsuarioFijoId = 1;

        public FavoritosController(IServicioFavoritos servicioFavoritos)
        {
            _servicioFavoritos = servicioFavoritos;
        }

        // GET: /api/favoritos
        [HttpGet]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var favoritos = await _servicioFavoritos.ListarAsync(UsuarioFijoId, cancellationToken);
            return Ok(favoritos);
        }

        // POST: /api/favoritos
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] CrearFavoritoRequest request, CancellationToken cancellationToken)
        {
            var (creado, mensajeError) = await _servicioFavoritos.AgregarAsync(UsuarioFijoId, request, cancellationToken);

            if (!creado)
            {
                return BadRequest(new { mensaje = mensajeError });
            }

            return Created(string.Empty, new { mensaje = "Favorito agregado correctamente." });
        }

        // DELETE: /api/favoritos/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id, CancellationToken cancellationToken)
        {
            var (eliminado, mensajeError) = await _servicioFavoritos.EliminarAsync(UsuarioFijoId, id, cancellationToken);

            if (!eliminado)
            {
                return NotFound(new { mensaje = mensajeError });
            }

            return NoContent();
        }
    }
}
