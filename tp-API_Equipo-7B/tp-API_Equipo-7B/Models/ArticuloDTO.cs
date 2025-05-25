using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tp_API_Equipo_7B.Models
{
    public class ArticuloDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int idMarca { get; set; }
        public string Marca { get; set; }
        public int idCategoria { get; set; }
        public string Categoria { get; set; }
        public List<string> Imagenes { get; set; }
        public decimal Precio { get; set; }
    }
}