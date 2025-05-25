using dominio;
using negocio;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using tp_API_Equipo_7B.Models;

namespace tp_API_Equipo_7B.Controllers
{
    [RoutePrefix("api/Imagen")]
    public class ImagenController : ApiController
    {
        // GET: api/Imagen/4
        [HttpGet]
        [Route("{idArticulo:int}")]
        public HttpResponseMessage Get(int idArticulo) // Para obtener el listado de imagenes de un articulo
        {
            imagenesDatos imagenes = new imagenesDatos();

            try
            {
                List<Imagen> imagenesArticulo = imagenes.Listar(idArticulo);
                if (imagenesArticulo.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No hay imagenes para este id de artículo");
                }
                return Request.CreateResponse(HttpStatusCode.OK, imagenesArticulo.Select(imagen => imagen.Url).ToList());
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "No hay imagenes para este id de artículo");
            }
        } //    OK

        // POST: api/Imagen
        // Para insertar imágenes a un artículo
        [HttpPost]
        [Route("{id}")]
        public HttpResponseMessage Post(int id, [FromBody] ImagenDTO imgs)
        {
            if (imgs == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            imagenesDatos imagenes = new imagenesDatos();
            articuloDatos articulos = new articuloDatos();
            if (articulos.getArticle(id).Id == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No existe artículo con esta ID.");
            }
            try
            {
                if (imagenes.addImages(id, imgs.imagenesURL))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Imagenes añadidas correctamente.");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
        }

        [HttpPut]
        [Route("{id}")]
        // PUT: api/Imagen/
        // Para actualizar el listado de imagenes (reemplaza la totalidad de las fotos por la nueva lista)
        public HttpResponseMessage Put(int id, [FromBody] ImagenDTO imgs)
        {
            if (imgs == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            imagenesDatos imagenes = new imagenesDatos();
            articuloDatos articulos = new articuloDatos();

            if (articulos.getArticle(id).Id == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No existe artículo con esta ID.");
            }

            imagenes.removeAllImages(id);
            try
            {
                if (imagenes.addImages(id, imgs.imagenesURL))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Imagenes actualizadas correctamente.");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        // Borra el listado que le pasa
        public HttpResponseMessage Delete(int id, [FromBody] ImagenDTO imgs)
        {
            if (imgs == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            imagenesDatos imagenes = new imagenesDatos();
            articuloDatos articulos = new articuloDatos();

            if (articulos.getArticle(id).Id == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No existe artículo con esta ID.");
            }

            foreach (string imgURL in imgs.imagenesURL)
            {
                imagenes.removeImage(id, imgURL);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Imagenes borradas.");
        }

        [HttpDelete]
        [Route("{id}/deleteAll")]
        // Borra todas las imagenes
        public HttpResponseMessage Delete(int id)
        {
            imagenesDatos imagenes = new imagenesDatos();
            articuloDatos articulos = new articuloDatos();

            if (articulos.getArticle(id).Id == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No existe artículo con esta ID.");
            }

            imagenes.removeAllImages(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Imagenes borradas.");
        }
    }
}