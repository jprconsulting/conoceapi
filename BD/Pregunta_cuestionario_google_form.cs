namespace conocelos_v3.BD
{
    public partial class PreguntaCuestionarioGoogleForm
    {
        public int PreguntaCuestionarioId { get; set; }

        public int FormularioId { get; set; }

        public string Pregunta { get; set; } = null!;
    }
}
