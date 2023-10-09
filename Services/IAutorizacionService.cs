using conocelos_v3.DTOS;

namespace conocelos_v3.Services
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponseDTO> DevolverToken(AutorizacionRequestDTO autorizacion);
    }
}
