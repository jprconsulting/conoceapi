namespace conocelos_v3.DTOS
{
    public class CandidaturaDTO
    {
        public int? CandidaturaId { get; set; }
        public int TipoCandidaturaId { get; set; }
        public string NombreCandidatura { get; set; }
        public string? Logo { get; set; }
        public string? Foto { get; set; }
        public IFormFile FotoArchivo { get; set; }
        public bool Estatus { get; set; }
        public string Acronimo { get; set; }
    }
}
