using conocelos_v3.Data;
using conocelos_v3.DTOS;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;

namespace conocelos_v3.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly ConocelosV2Context _context;
        private readonly IConfiguration _configuration;

        public AutorizacionService(ConocelosV2Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerarToken(string idUsuario)
        {
            // Toma la contresena unica del archivo JSON de congfiguracion
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key); // Codigica la clave en Bytes

            // Describe las propiedades del usuario
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            // Encripta la credencial de los tokens en a la clave en bytes 
            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                //SecurityAlgorithms.HmacSha256Signature
                SecurityAlgorithms.HmacSha256Signature
                );

            // Describe el token en base a la propiedades, expiracion y la credencial
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(23),
                //Expires = DateTime.UtcNow.AddMinutes(10), // Tiempo de expiaracion del token
                SigningCredentials = credencialesToken
            };

            // Se cra un nuevo token a manipular instanciado de Jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            // Se escribe el nuevo token manipulado en base a las propiedades y su configuracion
            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }

        public async Task<AutorizacionResponseDTO> DevolverToken(AutorizacionRequestDTO autorizacion)
        {
            var usuarioEncontrado = await (from u in _context.Usuarios
                                           join r in _context.Rols
                                           on u.RolId equals r.RolId
                                           where u.Email == autorizacion.Email && u.Password == autorizacion.Password
                                           select new AutorizacionResponseDTO
                                           {
                                               UsuarioId = u.UsuarioId,
                                               Nombre = u.Nombre,
                                               Apellidos = u.Apellidos,
                                               Email = u.Email,
                                               RolId = u.RolId,
                                               Rol = r.Nombre,
                                               FormulariosAsignados = (from fu in _context.GoogleFormUsuarios
                                                                      join gf in _context.GoogleForms
                                                                      on fu.FormularioId equals gf.FormularioId
                                                                      where fu.UsuarioId == u.UsuarioId
                                                                       let estatus = fu.Estatus
                                                                       select new FormularioAsignadoDTO
                                                                      {
                                                                         FormularioId = fu.FormularioId,
                                                                         FormularioUsuarioId = fu.FormularioUsuarioId,
                                                                         FormName = gf.FormName,
                                                                         GoogleFormId = gf.GoogleFormId,
                                                                          Estatus = fu.Estatus
                                                                       }).ToList()
                                           }).FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
            {
                return await Task.FromResult<AutorizacionResponseDTO>(null);
            }


            string tokenCreado = GenerarToken(usuarioEncontrado.UsuarioId.ToString());



            return new AutorizacionResponseDTO()
            {
                UsuarioId = usuarioEncontrado.UsuarioId,
                Nombre = usuarioEncontrado.Nombre,
                Apellidos = usuarioEncontrado.Apellidos,
                Email = usuarioEncontrado.Email,
                Rol = usuarioEncontrado.Rol,
                RolId = usuarioEncontrado.RolId,
                IsAuthenticated = true,
                Token = tokenCreado,
                Claims = GeRolClaims(usuarioEncontrado.RolId),
                FormulariosAsignados = usuarioEncontrado.FormulariosAsignados
            };

        }

        private List<AppRolClaimDTO> GeRolClaims(int rolId)
        {
            List<AppRolClaimDTO> listClaims = _context.RolClaims.Where(r => r.RolId == rolId)
                .Select(r => new AppRolClaimDTO()
                {
                    ClaimRolID = r.RolClaimId,
                    RolId = r.RolId,
                    ClaimType = r.ClaimType,
                    ClaimValue = r.ClaimValue == 1UL
                }).ToList();

            return listClaims;
        }



    }
}
