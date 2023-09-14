using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using mroSoft.Properties;

namespace mroSoft
{
    class connectBD
    {


        //conextin string del app config
        public static string cadena()
        {
            return Settings.Default.ConnectionString;
        }

        //metodo para guardar el conexionString
        public static SqlConnection getconnection()
        {

            SqlConnection con = new SqlConnection(cadena());
            return con;
        }


    }
}
