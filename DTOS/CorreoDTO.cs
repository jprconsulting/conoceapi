namespace conocelos_v3.DTOS
{
    public class CorreoDTO
    {
        public int Id { get; set; }
        public string EmailOrigen { get; set; }
        public string Contraseña { get; set; }
        public Boolean Credenciales { get; set; }
        public string NombreUsuario { get; set; }
        public int ServidorOrigen { get; set; }
        public string PuertoOrigen { get; set; }
        public Boolean ConfiarCertificado { get; set; }
    }
}
