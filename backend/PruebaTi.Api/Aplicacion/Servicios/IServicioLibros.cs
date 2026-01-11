using PruebaTi.Api.Aplicacion.Dto;

namespace PruebaTi.Api.Aplicacion.Servicios
{
    public interface IServicioLibros
    {
        Task<List<LibroDto>> BuscarAsync(string textoBusqueda, CancellationToken cancellationToken = default);
    }
}
