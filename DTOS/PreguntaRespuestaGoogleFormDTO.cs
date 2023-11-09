namespace conocelos_v3.DTOS
{
    public class PreguntaRespuestaGoogleFormDTO
    {
        public int PreguntaCuestionarioId { get; set; } 
        public string Pregunta { get; set; } = null!;
        public int RespuestaPreguntaCuestionarioid { get; set; }
        public string Respuesta { get; set; } = null!;
    }
}
