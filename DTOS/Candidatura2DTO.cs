namespace conocelos_v3.DTOS
{
    public class Candidatura2DTO
    {
        public int TipoCandidaturaId { get; set; }
        public string NombreTipoCandidatura { get; set; }

        public string NombreCandidatura { get; set; }

        public string? NombreFoto { get; set; }

        public string? Acronimo { get; set; }

        public string? Logo { get; set; }
        public string? Base64Logo { get; set; }

        public bool Estatus { get; set; }
    }
}
