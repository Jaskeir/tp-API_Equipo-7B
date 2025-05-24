using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace negocio
{
    public class articuloDatos
    {
        public List<Articulo> getArticles()
        {
            List<Articulo> articulos = new List<Articulo>();
            database db = new database();
            try
            {
                db.setQuery("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, Precio FROM Articulos A INNER JOIN Marcas M ON A.IdMarca = M.Id INNER JOIN Categorias C ON A.IdCategoria = C.Id");
                db.execQuery();

                while (db.Lector.Read())
                {
                    Articulo tempArticle = new Articulo();
                    setArticleData(tempArticle, db.Lector);
                    articulos.Add(tempArticle);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                db.closeConnection();
            }
            return articulos;
        }

        public void setArticleData(Articulo tempArticle, SqlDataReader data)
        {
            imagenesDatos imagenes = new imagenesDatos();

            tempArticle.Id = (int)data["Id"];
            tempArticle.Codigo = (string)data["Codigo"];
            tempArticle.Nombre = (string)data["Nombre"];
            tempArticle.Descripcion = (string)data["Descripcion"];
            tempArticle.Marca.Nombre = (string)data["Marca"];
            tempArticle.Categoria.Nombre = (string)data["Categoria"];
            tempArticle.Precio = Math.Round((decimal)data["Precio"], 2);
            tempArticle.Imagenes = imagenes.Listar((int)data["Id"]);
        }

        public Articulo getArticle(int id)
        {
            Articulo articulo = new Articulo();

            database db = new database();

            try
            {
                db.setQuery("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, Precio FROM Articulos A INNER JOIN Marcas M ON A.IdMarca = M.Id INNER JOIN Categorias C ON A.IdCategoria = C.Id WHERE A.Id = @id");
                db.setParameter("id", id);
                db.execQuery();

                if (db.Lector.Read())
                {
                    setArticleData(articulo, db.Lector);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                db.closeConnection();
            }

            return articulo;
        }

        public bool addArticleToDatabase(Articulo art)
        {
            database db = new database();
            imagenesDatos imagenManager = new imagenesDatos();

            try
            {
                art.Codigo = generateCode(art.Marca.Nombre);

                db.setQuery("INSERT INTO Articulos (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @precio)");
                db.setParameter("@codigo", art.Codigo);
                db.setParameter("@nombre", art.Nombre);
                db.setParameter("@descripcion", art.Descripcion);
                db.setParameter("@idMarca", art.Marca.Id);
                db.setParameter("@idCategoria", art.Categoria.Id);
                db.setParameter("@precio", art.Precio);

                db.execNonQuery();

                //imagenManager.addImages(articulo);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection();
            }
        }

        /// <summary>
        /// Genera un código de producto, a partir de la marca que le pasemos
        /// Inicial de la marca + un número
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public string generateCode(string marca)
        {
            string inicialMarca = marca.Substring(0, 1).ToUpper();
            int codigoMarca = 1;
            List<int> codigosExistentes = new List<int>();

            database db = new database();
            db.setQuery("SELECT Codigo FROM Articulos WHERE Codigo LIKE @codigo");
            db.setParameter("@codigo", inicialMarca + "%");
            db.execQuery();

            while (db.Lector.Read())
            {
                string codigo = (string)db.Lector["Codigo"];
                codigo = codigo.Substring(1);

                codigosExistentes.Add(int.Parse(codigo));
            }

            while (codigosExistentes.Contains(codigoMarca))
            {
                codigoMarca++;
            }

            return inicialMarca + codigoMarca;
        }

        /// <summary>
        /// Obtiene el ID del artículo dentro de la base de datos.
        /// </summary>
        /// <param name="articulo"></param>
        /// <returns></returns>
        public int getId(Articulo articulo)
        {
            database db = new database();
            try
            {
                db.setQuery("SELECT Id FROM Articulos WHERE Codigo = @codigo");
                db.setParameter("@codigo", articulo.Codigo);
                db.execQuery();

                if (db.Lector.Read())
                {
                    return (int)db.Lector["Id"];
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection();
            }
        }
    }
}
