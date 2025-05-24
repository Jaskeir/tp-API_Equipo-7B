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
        public int idCategoria { get; set; }
        //public List<string> Imagenes { get; set; }
        public decimal Precio { get; set; }
    }
}