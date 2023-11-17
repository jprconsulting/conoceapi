namespace conocelos_v3.DTOS
{
    public class AceptacionDTO
    {
        public int Id { get; set; }
        public string? NombreC { get; set; }
        public int idCandidato { get; set; }
        public string? Nombre { get; set; }
        public string? Apat { get; set; }
        public string? Amat { get; set; }
        public string? Email { get; set; }
        public DateTime Fechadenvio { get; set; }
        public DateTime? Fechaaceptacion { get; set; }

    }
}
