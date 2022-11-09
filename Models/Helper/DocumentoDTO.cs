using Microsoft.AspNetCore.Http;

namespace Quinela_TPD.Models.Helper
{
    public class DocumentoDTO
    {

        public int IdDocumento { get; set; }
        public string Descripcion { get; set; }
        public string Ruta { get; set; }
        public IFormFile Archivo { get; set; }
    }

}
