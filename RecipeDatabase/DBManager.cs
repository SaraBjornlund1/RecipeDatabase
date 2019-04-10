using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RecipeDatabase
{
    public class DBManager
    {
        private string ConString { get; set; }
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private string SQL;

        public DBManager(string sql)
        {
            ConString = "Data Source=localhost;Initial Catalog = RecipeDatabase; Integrated Security = SSPI;";
            SQL = sql;
        }

        public DataTable ExecuteSQL()
        {
            DataTable tbl = new DataTable();

            using (con = new SqlConnection(ConString))
            {
                con.Open();

                cmd = new SqlCommand(SQL, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tbl);
            }
            return tbl;
        }

        //public void ExecuteSQLNoReturn()
        //{
        //    using (con = new SqlConnection(ConString))
        //    {
        //        con.Open();

        //        cmd = new SqlCommand(SQL, con);
        //        cmd.ExecuteNonQuery();
        //    }
        //}
    }
}
