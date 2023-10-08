namespace conocelos_v3.DTOS
{
    public class CandidaturaDTO
    {
        public int? CandidaturaId { get; set; }

        public int TipoCandidaturaId { get; set; }
        public string NombreTipoCandidatura { get; set; }

        public string NombreCandidatura { get; set; }

        public string? Logo { get; set; }
        public string? Base64Logo { get; set; }

        public bool Estatus { get; set; }

        public IFormFile imagen { get; set; }

        public string acronimo { get; set; }
    }
}
