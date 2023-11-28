namespace conocelos_v3.BD
{
    public partial class Usuario
    {
        public int UsuarioId { get; set; }

        public int RolId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ulong Estatus { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public virtual Rol Rol { get; set; }
    }

}
