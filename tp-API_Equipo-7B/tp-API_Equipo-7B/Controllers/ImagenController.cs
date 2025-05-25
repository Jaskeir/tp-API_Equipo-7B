using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tp_API_Equipo_7B.Models;

namespace tp_API_Equipo_7B.Controllers
{
    public class ImagenController : ApiController
    {
        // POST: api/Imagen
        public void Post([FromBody] ImagenDTO imgs)
        {
            imagenesDatos imagenes = new imagenesDatos();

            imagenes.addImages(imgs.idArticulo, imgs.imagenesURL);
        }
    }
}