namespace conocelos_v3.DTOS
{
    public class FormularioPreguntasRespuestasGoogleFormDTO
    {
        public int FormularioId { get; set; }
        public string FormName { get; set; } = null!;
        public string GoogleFormId { get; set; } = null!;
        public List<PreguntaRespuestaGoogleFormDTO> PreguntasRespuestas { get; set; }
    }
}
