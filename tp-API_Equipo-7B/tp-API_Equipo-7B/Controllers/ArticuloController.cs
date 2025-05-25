using dominio;
using negocio;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using tp_API_Equipo_7B.Models;

namespace tp_API_Equipo_7B.Controllers
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        public HttpResponseMessage Get()
        {
            articuloDatos articulo = new articuloDatos();

            List<Articulo> listaArticulos = articulo.getArticles();

            if (listaArticulos.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No hay articulos en la lista");
            }
            return Request.CreateResponse(HttpStatusCode.OK, listaArticulos);
        }

        // GET: api/Articulo/5
        public HttpResponseMessage Get(int id)
        {
            articuloDatos articulo = new articuloDatos();
            Articulo filtrado = articulo.getArticle(id);
            if (filtrado.Id == -1)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No hay articulo con el id: " + id);
            }

            imagenesDatos imagenes = new imagenesDatos();
            List<Imagen> imagenesArticulo = imagenes.Listar(id);

            return Request.CreateResponse(HttpStatusCode.OK, filtrado);
        }

        // POST: api/Articulo
        public HttpResponseMessage Post([FromBody] ArticuloDTO value)
        {
            if (value == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            articuloDatos dbArticulos = new articuloDatos();
            marcaDatos dbMarca = new marcaDatos();
            categoriaDatos dbCategoria = new categoriaDatos();

            try
            {
                // Mapeo de datos:
                Articulo nuevoArticulo = new Articulo();
                nuevoArticulo.Nombre = value.Nombre;
                nuevoArticulo.Descripcion = value.Descripcion;
                if (dbMarca.getMarca(value.idMarca).Id == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No hay Marca con id = " + value.idMarca);
                }
                nuevoArticulo.Marca = dbMarca.getMarca(value.idMarca);
                if (dbCategoria.getCategoria(value.idCategoria).Id == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No hay Categoria con id = " + value.idCategoria);
                }
                nuevoArticulo.Categoria = dbCategoria.getCategoria(value.idCategoria);
                nuevoArticulo.Precio = value.Precio;

                dbArticulos.addArticleToDatabase(nuevoArticulo);

                return Request.CreateResponse(HttpStatusCode.OK, "Se insertó el artículo correctamente");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Articulo/5
        public HttpResponseMessage Put(int id, [FromBody] ArticuloDTO art) // No incluye la modificaion de imagenes porque esta en otra funcion
        {
            if (art == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            articuloDatos dbArticulos = new articuloDatos();
            marcaDatos dbMarca = new marcaDatos();
            categoriaDatos dbCategoria = new categoriaDatos();

            Articulo modificacion = dbArticulos.getArticle(id);

            if (modificacion.Id == -1)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No hay articulo con id = " + id);
            }
            else
            {
                modificacion.Nombre = art.Nombre;
                modificacion.Descripcion = art.Descripcion;
                if (dbMarca.getMarca(art.idMarca).Id == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No hay Marca con id = " + art.idMarca);
                }
                modificacion.Marca = dbMarca.getMarca(art.idMarca);
                if (dbCategoria.getCategoria(art.idCategoria).Id == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No hay Categoria con id = " + art.idCategoria);
                }
                modificacion.Categoria = dbCategoria.getCategoria(art.idCategoria);
                modificacion.Precio = art.Precio;
                modificacion.Id = id;
                dbArticulos.updateArticle(modificacion);
                return Request.CreateResponse(HttpStatusCode.OK, "Articulo modificado correctamente.");
            }
        }

        // DELETE: api/Articulo/5
        public HttpResponseMessage Delete(int id)
        {
            articuloDatos manager = new articuloDatos();
            imagenesDatos imagenes = new imagenesDatos();
            Articulo articuloEliminar = manager.getArticle(id);

            if (articuloEliminar.Id == -1)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No hay articulos con ese id: " + id);
            }
            imagenes.removeAllImages(id);
            manager.EliminarArticulo(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Se elimino el articulo con id: " + id);
        }
    }
}
