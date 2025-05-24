using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class database
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public database()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true");
            comando = new SqlCommand();
            
        }

        public void setQuery(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        
        /// <summary>
        /// Permite ejecutar consultas (select)
        /// </summary>
        public void execQuery()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite ejecutar acciones (insert, update)
        /// </summary>
        public void execNonQuery()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void setParameter(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void closeConnection()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

    }
}
