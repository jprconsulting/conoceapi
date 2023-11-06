namespace conocelos_v3.Models
{
    public partial class Ayuntamiento
    {
        public int AyuntamientoId { get; set; }
        public string NombreAyuntamiento { get; set; }
        public string Acronimo { get; set; }
        public ulong Estatus { get; set; }
        public string ExtPet { get; set; }
        public int DistritoLocalId { get; set; }

        public DistritoLocal DistritoLocal { get; set; }
    }
}
