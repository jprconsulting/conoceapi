namespace conocelos_v3.DTOS
{
    public class UsuarioDTO
    {
        public int? UsuarioId { get; set; }
        public int RolId { get; set; }
        public string? Rol { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Estatus { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
    }
}
