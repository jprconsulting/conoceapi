namespace conocelos_v3.BD
{
    public partial class Correo
    {
        public int Id { get; set; }
        public string EmailOrigen { get; set; }
        public string Contraseña { get; set; }
        public ulong Credenciales { get; set; }
        public string NombreUsuario { get; set; }
        public string ServidorOrigen { get; set; }
        public int PuertoOrigen { get; set; }
        public ulong ConfiarCertificado { get; set; }
        public string PerfilCorreo { get; set; }
    }
}
