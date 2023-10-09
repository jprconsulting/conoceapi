namespace conocelos_v3.DTOS
{
    public class AutorizacionResponseDTO
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public List<AppRolClaimDTO> Claims { get; set; }
    }
}
