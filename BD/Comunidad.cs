namespace conocelos_v3.BD
{
    public partial class Comunidad
    {
        public int? ComunidadId { get; set; }
        public string NombreComunidad { get; set; }
        public string Acronimo { get; set; }
        public ulong Estatus { get; set; }
        public string ExtPet { get; set; }
        public int AyuntamientoId { get; set; }
    }
}
