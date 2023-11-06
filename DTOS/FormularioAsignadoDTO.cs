namespace conocelos_v3.DTOS
{
    public class FormularioAsignadoDTO
    {
        public int FormularioUsuarioId { get; set; }

        public int FormularioId { get; set; }

        public string FormName { get; set; } = null!;

        public string GoogleFormId { get; set; } = null!;

        public bool Estatus { get; set; }

    }
}
