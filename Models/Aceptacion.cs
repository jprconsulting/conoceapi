namespace conocelos_v3.Models
{
    public partial class Aceptacion
    {
        public int Id { get; set; }
        public string NombreC { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public DateTime Fechadenvio { get; set; }
        public DateTime Fechaaceptacion { get; set; }
    }
}
