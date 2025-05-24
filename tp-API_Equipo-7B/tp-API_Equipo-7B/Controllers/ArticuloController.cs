using dominio;
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
<<<<<<< Updated upstream
            Articulo filtrado = articulo.getArticle(id);

            if(filtrado.Id==0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No hay articulo con el id: " + id);

            }

            return Request.CreateResponse(HttpStatusCode.OK, filtrado);
=======

            return articulo.getArticle(id);
>>>>>>> Stashed changes
        }

        // POST: api/Articulo
        public void Post([FromBody] ArticuloDTO value)
        {
            articuloDatos dbArticulos = new articuloDatos();
            marcaDatos dbMarca = new marcaDatos();
            categoriaDatos dbCategoria = new categoriaDatos();

            // Mapeo de datos:
            Articulo nuevoArticulo = new Articulo();
            nuevoArticulo.Nombre = value.Nombre;
            nuevoArticulo.Descripcion = value.Descripcion;
            nuevoArticulo.Marca = dbMarca.getMarca(value.idMarca);
            nuevoArticulo.Categoria = dbCategoria.getCategoria(value.idCategoria);
            //nuevoArticulo.Imagenes = value.Imagenes;
            nuevoArticulo.Precio = value.Precio;

            dbArticulos.addArticleToDatabase(nuevoArticulo);
        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
        }
    }
}
