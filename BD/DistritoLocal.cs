namespace conocelos_v3.BD
{
    public partial class DistritoLocal
    {
        public int? DistritoLocalId { get; set; }
        public string NombreDistritoLocal { get; set; }
        public string Acronimo { get; set; }
        public bool Estatus { get; set; }
        public string ExtPet { get; set; }
        public int EstadoId { get; set; }

    }
}
