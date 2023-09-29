using conocelos_v3.Models;

namespace conocelos_v3.Servicios
{
    public interface IConvertJson
    {
        /// <summary>
        /// Interface para hacer uso de la funcion
        /// retorna tabla modelo - parametre nombre del archivo
        /// </summary>
        /// <param name="nameFile"></param>
        /// <returns></returns>
        Task<TablaFormularioBack> ConvertirDataJson(string nameFile);
    }
}
