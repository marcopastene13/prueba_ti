namespace PruebaTi.Api.Dominio.Entidades
{
    public class Favorito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string IdExterno { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string? Autores { get; set; }
        public int? AnioPrimeraPublicacion { get; set; }
        public string? UrlPortada { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
