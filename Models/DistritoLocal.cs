namespace conocelos_v3.Models
{
    public partial class DistritoLocal
    {
        public int DistritoLocalId { get; set; }
        public string NombreDistritoLocal { get; set; }
        public string Acronimo { get; set; }
        public ulong Estatus { get; set; }
        public string ExtPet { get; set; }
        public int EstadoId { get; set; } 

    }
}
