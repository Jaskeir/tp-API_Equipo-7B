using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dominio;

namespace tp_API_Equipo_7B.Models
{
    public class ImagenDTO
    {
        public int idArticulo { get; set; }
        public List<string> imagenesURL { get; set; }
    }
}