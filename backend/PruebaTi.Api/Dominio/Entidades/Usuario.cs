namespace PruebaTi.Api.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();
    }
}
