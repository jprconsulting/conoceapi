namespace conocelos_v3.Controllers
{
    public class Utilis
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Utilis(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> SubirImagen(string base64Imagen)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(base64Imagen))
                {
                    // Convertir la cadena base64 en bytes
                    byte[] bytesImagen = Convert.FromBase64String(base64Imagen);

                    // Generar un nombre de archivo único (puedes personalizar esto)
                    string nombreArchivo = "nombre" + ".JPG";



                    // Obtener la ruta de la carpeta donde se guardarán las imágenes (puedes personalizar la ruta)
                    // var rutaCarpetaImagenes = Path.Combine(_hostingEnvironment.ContentRootPath, "images");
                    // var ruta = "wwwroot\\images";
                    var ruta = "C:\\Users\\Administrador\\Documents\\a_Trabajo JPR Consulting\\Utilidades_Web_Api\\conocelos_v4\\conocelos-v3-master\\wwwroot\\images\\";
                    // var rutaCarpetaImagenes = Path.Combine(ruta, "images");


                    // Verificar si la carpeta de imágenes existe; si no, créala
                    if (!Directory.Exists(ruta))
                    {
                        Directory.CreateDirectory(ruta);
                    }

                    // Combinar la ruta de la carpeta con el nombre del archivo
                    string rutaCompletaImagen = Path.Combine(ruta, nombreArchivo);

                    // Guardar los bytes de la imagen en el archivo
                    await System.IO.File.WriteAllBytesAsync(rutaCompletaImagen, bytesImagen);

                    return nombreArchivo;
                }
                return "";

            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante el proceso
                return "";
            }
        }

        
    }
}
