using conocelos_v3.Models;
using Microsoft.VisualBasic;

namespace conocelos_v3.DTOS
{
    public class CandidatoDTO
    {
        public int? CandidatoId { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string? SobrenombrePropietario { get; set; }
        //3

        public string NombreSuplente { get; set; }

        public DateOnly FechaNacimiento { get; set; }

        public string? DireccionCasaCampania { get; set; }

        public string? TelefonoPublico { get; set; }
        //7

        public string? Email { get; set; }

        public string? PaginaWeb { get; set; }

        public string? Facebook { get; set; }

        public string? Twitter { get; set; }
        // 11

        public string? Instagram { get; set; }

        public string? Tiktok { get; set; }

        public string? Foto { get; set; }

        public IFormFile FotoArchivo { get; set; }

        public bool Estatus { get; set; }
        //15
        public int CargoId { get; set; }

        public int GeneroId { get; set; }

        public int EstadoId { get; set; }

        public int? DistritoLocalId { get; set; }

        public int? AyuntamientoId { get; set; }

        public int? ComunidadId { get; set; }

        public int CandidaturaId { get; set; }
        // 19      

    }
}