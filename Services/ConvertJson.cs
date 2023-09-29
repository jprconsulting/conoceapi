using System.Text.Json;
using conocelos_v3.Models;
using conocelos_v3.Services;
using conocelos_v3.Servicios;

namespace conocelos_v3.Services
{
    public class ConvertJson : IConvertJson
    {
        /// <summary>
        /// Funicion para deserializar el Json toma parametro el nombre del archivo JSON
        /// </summary>
        /// <param name="nameFile"></param>
        /// <returns></returns>
        public async Task<TablaFormularioBack> ConvertirDataJson(string nameFile)
        {
            string contenidoJson = File.ReadAllText(nameFile);

            TablaFormularioBack objetoDeserializado = JsonSerializer.Deserialize<TablaFormularioBack>(contenidoJson);

            return objetoDeserializado;
        }
    }
}
