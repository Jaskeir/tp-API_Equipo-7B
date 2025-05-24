using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class imagenesDatos
    {
        public List<Imagen> Listar(int id)
        {
            List<Imagen> lista = new List<Imagen>();

            database db = new database();

            try
            {
                db.setQuery("SELECT ImagenUrl FROM Imagenes WHERE IdArticulo = @id");
                db.setParameter("@id", id);
                db.execQuery();

                while (db.Lector.Read())
                {
                    lista.Add(new Imagen((string)db.Lector["ImagenUrl"]));
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
            return lista;
        }

        public bool addImage(int idArticulo, string url)
        {
            database db = new database();
            try
            {
                db.setQuery("INSERT INTO Imagenes (IdArticulo, ImagenUrl) VALUES (@id, @url)");
                db.setParameter("@id", idArticulo);
                db.setParameter("@url", url);
                db.execNonQuery();
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

        public bool addImages(Articulo articulo)
        {
            database db = new database();
            articuloDatos articuloManager = new articuloDatos();
            int idArticulo = articuloManager.getId(articulo);
            foreach (Imagen img in articulo.Imagenes)
            {
                addImage(idArticulo, img.Url);
            }
            return true;
        }

        public bool removeImage(Articulo articulo, string url)
        {
            database db = new database();
            try
            {
                db.setQuery("DELETE FROM Imagenes WHERE IdArticulo = @id AND ImagenUrl = @url");
                db.setParameter("@id", articulo.Id);
                db.setParameter("@url", url);
                db.execNonQuery();
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
    }
}
