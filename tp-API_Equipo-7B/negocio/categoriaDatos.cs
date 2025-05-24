using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class categoriaDatos
    {
        public Categoria getCategoria(int id)
        {
            database db = new database();
            Categoria categoria = new Categoria();

            try
            {
                db.setQuery("SELECT Id, Descripcion AS Nombre FROM Categorias WHERE Id = @id");
                db.setParameter("id", id);
                db.execQuery();

                if (db.Lector.Read())
                {
                    categoria.Id = (int)db.Lector["Id"];
                    categoria.Nombre = (string)db.Lector["Nombre"];
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

            return categoria;
        }
    }
}
