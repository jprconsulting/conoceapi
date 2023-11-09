namespace conocelos_v3.DTOS
{
    public class CandidatoPreguntasRespuestasGoogleFormDTO
    {
        public int CandidatoId { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public List<FormularioPreguntasRespuestasGoogleFormDTO> Formularios { get; set; }
    }
}
