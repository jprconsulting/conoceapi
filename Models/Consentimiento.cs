namespace conocelos_v3.Models
{
    public class Consentimiento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public DateTime Fechadenvio { get; set; }
        public DateTime? Fechaaceptacion { get; set; }
        public string Cuerpocorreo { get; set; }
    }
}
