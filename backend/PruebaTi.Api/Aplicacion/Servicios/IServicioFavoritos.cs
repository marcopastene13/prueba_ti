using PruebaTi.Api.Aplicacion.Dto;

namespace PruebaTi.Api.Aplicacion.Servicios
{
    public interface IServicioFavoritos
    {
        Task<List<FavoritoDto>> ListarAsync(int usuarioId, CancellationToken cancellationToken = default);
        Task<(bool Creado, string? MensajeError)> AgregarAsync(int usuarioId, CrearFavoritoRequest request, CancellationToken cancellationToken = default);
        Task<(bool Eliminado, string? MensajeError)> EliminarAsync(int usuarioId, int favoritoId, CancellationToken cancellationToken = default);
    }
}
