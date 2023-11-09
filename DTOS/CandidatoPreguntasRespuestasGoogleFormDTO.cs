namespace conocelos_v3.DTOS
{
    public class CandidatoPreguntasRespuestasGoogleFormDTO
    {
        public int CandidatoId { get; set; }
        public string Nombre { get; set; } = null!;
        public List<FormularioPreguntasRespuestasGoogleFormDTO> Formularios { get; set; }
    }
}
