using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class marcaDatos
    {
        public Marca getMarca(int id)
        {
            database db = new database();
            Marca marca = new Marca();
            bool validar = false;
            try
            {
                db.setQuery("SELECT Id, Descripcion AS Nombre FROM Marcas WHERE Id = @id");
                db.setParameter("id", id);
                db.execQuery();

                if (db.Lector.Read())
                {
                    marca.Id = (int)db.Lector["Id"];
                    marca.Nombre = (string)db.Lector["Nombre"];
                    validar = true;
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
            if (!validar)
            {
                marca.Id = -1;
            }
            return marca;
        }
    }
}
