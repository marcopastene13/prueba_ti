using Microsoft.EntityFrameworkCore;
using PruebaTi.Api.Aplicacion.Dto;
using PruebaTi.Api.Dominio.Entidades;
using PruebaTi.Api.Infraestructura.Contexto;

namespace PruebaTi.Api.Aplicacion.Servicios
{
    public class ServicioFavoritos : IServicioFavoritos
    {
        private readonly AplicacionDbContext _contexto;

        public ServicioFavoritos(AplicacionDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<FavoritoDto>> ListarAsync(int usuarioId, CancellationToken cancellationToken = default)
        {
            var favoritos = await _contexto.Favoritos
                .Where(f => f.UsuarioId == usuarioId)
                .OrderBy(f => f.Titulo)
                .ToListAsync(cancellationToken);

            return favoritos.Select(f => new FavoritoDto
            {
                Id = f.Id,
                IdExterno = f.IdExterno,
                Titulo = f.Titulo,
                Autores = f.Autores,
                AnioPrimeraPublicacion = f.AnioPrimeraPublicacion,
                UrlPortada = f.UrlPortada
            }).ToList();
        }

        public async Task<(bool Creado, string? MensajeError)> AgregarAsync(int usuarioId, CrearFavoritoRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.IdExterno) || string.IsNullOrWhiteSpace(request.Titulo))
            {
                return (false, "IdExterno y Titulo son obligatorios.");
            }

            var yaExiste = await _contexto.Favoritos
                .AnyAsync(f => f.UsuarioId == usuarioId && f.IdExterno == request.IdExterno, cancellationToken);

            if (yaExiste)
            {
                return (false, "El libro ya está agregado a favoritos.");
            }

            var favorito = new Favorito
            {
                UsuarioId = usuarioId,
                IdExterno = request.IdExterno,
                Titulo = request.Titulo,
                Autores = request.Autores,
                AnioPrimeraPublicacion = request.AnioPrimeraPublicacion,
                UrlPortada = request.UrlPortada
            };

            _contexto.Favoritos.Add(favorito);
            await _contexto.SaveChangesAsync(cancellationToken);

            return (true, null);
        }

        public async Task<(bool Eliminado, string? MensajeError)> EliminarAsync(int usuarioId, int favoritoId, CancellationToken cancellationToken = default)
        {
            var favorito = await _contexto.Favoritos
                .FirstOrDefaultAsync(f => f.Id == favoritoId && f.UsuarioId == usuarioId, cancellationToken);

            if (favorito is null)
            {
                return (false, "No se encontró el favorito indicado.");
            }

            _contexto.Favoritos.Remove(favorito);
            await _contexto.SaveChangesAsync(cancellationToken);

            return (true, null);
        }
    }
}
