using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorImages.Controlador
{
    class ImgsCRUD
    {
        public static DataTable VerImgs()
        {
            try
            {
                string sql = "select id,nombre, data_path from Imagenes";

                SqlConnection conn = new SqlConnection(VectorImages.Properties.Resources.cadena);
                DataSet ds = new DataSet();
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
                conn.Close();

                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool InsertIMG(string name, string path)
        {
            try
            {
                string query = "insert into Imagenes values ('"+name+"','"+path+"')";

                SqlConnection conn = new SqlConnection(VectorImages.Properties.Resources.cadena);
                conn.Open();
                SqlCommand nc = new SqlCommand(query, conn);
                nc.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable FilterImg(string nombre)
        {
            try
            {
                string sql = "select id,nombre, data_path from Imagenes where nombre like '"+"%"+nombre+"%'";

                SqlConnection conn = new SqlConnection(VectorImages.Properties.Resources.cadena);
                DataSet ds = new DataSet();
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
                conn.Close();

                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
