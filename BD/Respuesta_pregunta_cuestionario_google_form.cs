namespace conocelos_v3.BD
{
    public partial class RespuestaPreguntaCuestionarioGoogleForm
    {
        public int RespuestaPreguntaCuestionarioId { get; set; }

        public int PreguntaCuestionarioId { get; set; }

        public string Respuesta { get; set; } = null!;

        public int CandidatoId { get; set; }
    }
}
