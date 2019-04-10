using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RecipeDatabase
{
    class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public List<Category> GetAllCategory()
        {
            DBManager db = new DBManager("SELECT CategoryID, CategoryName FROM Category");
            DataTable tbl = db.ExecuteSQL();

            var list = new List<Category>();
            foreach (DataRow row in tbl.Rows)
            {
                Category category = new Category
                {
                    CategoryName = row["CategoryName"].ToString(),
                    CategoryID = Convert.ToInt32(row["CategoryID"]),
                };
                list.Add(category);
            }
            return list;
        }


    }
}
