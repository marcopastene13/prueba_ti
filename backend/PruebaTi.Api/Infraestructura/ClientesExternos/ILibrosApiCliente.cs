using PruebaTi.Api.Aplicacion.Dto;

namespace PruebaTi.Api.Infraestructura.ClientesExternos
{
    public interface ILibrosApiCliente
    {
        Task<List<LibroDto>> BuscarLibrosPorTextoAsync(string textoBusqueda, CancellationToken cancellationToken = default);
    }
}
