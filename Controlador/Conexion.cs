using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorImages.Controlador
{
    class Conexion
    {
        private SqlConnection conexion = new SqlConnection();
        public bool AbrirConexion()
        {
            try
            {
                conexion.ConnectionString = VectorImages.Properties.Resources.cadena;
                conexion.Open();
                return true;
            }
            catch (Exception)
            {

                return false;
            }



        }

        public bool CerrarConexion()
        {
            try
            {
                conexion.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
