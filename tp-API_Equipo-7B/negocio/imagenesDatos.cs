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
    }
}
