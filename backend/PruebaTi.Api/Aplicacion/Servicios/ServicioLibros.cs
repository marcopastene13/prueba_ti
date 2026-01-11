using PruebaTi.Api.Aplicacion.Dto;
using PruebaTi.Api.Infraestructura.ClientesExternos;

namespace PruebaTi.Api.Aplicacion.Servicios
{
    public class ServicioLibros : IServicioLibros
    {
        private readonly ILibrosApiCliente _librosApiCliente;

        public ServicioLibros(ILibrosApiCliente librosApiCliente)
        {
            _librosApiCliente = librosApiCliente;
        }

        public Task<List<LibroDto>> BuscarAsync(string textoBusqueda, CancellationToken cancellationToken = default)
        {
            return _librosApiCliente.BuscarLibrosPorTextoAsync(textoBusqueda, cancellationToken);
        }
    }
}
