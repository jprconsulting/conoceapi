namespace conocelos_v3.Models
{
    public partial class Aceptacion
    {
        public int Id { get; set; }
        public string NombreC { get; set; }
        public string idCandidato { get; set; }
        public string Nombre { get; set; }
        public string Apat { get; set; }
        public string Amat { get; set; }
        public DateTime Fechadenvio { get; set; }
        public DateTime? Fechaaceptacion { get; set; }
    }
}
