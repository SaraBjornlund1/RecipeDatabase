using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RecipeDatabase
{
    class Recipe
    {
        public int RecipeID { get; set; }
        public string Title { get; set; }
        public string Ingredient { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }

        public List<Recipe> GetAllRecipe()
        {
            DBManager db = new DBManager("SELECT RecipeID, Title, Ingredient, RecipeDescription FROM Recipe");
            DataTable tbl = db.ExecuteSQL();

            var list = new List<Recipe>();
            foreach (DataRow row in tbl.Rows)
            {
                Recipe recipe = new Recipe
                {
                    Title = row["Title"].ToString(),
                    RecipeID = Convert.ToInt32(row["RecipeID"]),
                    Ingredient = row["Ingredient"].ToString(),
                    Description = row["RecipeDescription"].ToString(),
                };
                list.Add(recipe);
            }
            return list;    
        }

        public DataTable SelectedValueInList(int id)
        {
            string sql = "SELECT R.Title, R.Ingredient, R.RecipeDescription, C.CategoryID, C.CategoryName FROM Recipe R " +
                    "inner join Category C on C.CategoryID = R.CategoryID where RecipeID = '" + id + "'";


            DBManager db = new DBManager(sql);
            return db.ExecuteSQL();
        }

        public DataTable SearchForRecipe(string searchText)
        {
            string sql = "SELECT R.Title, R.RecipeID, C.CategoryName FROM Recipe R " +
                         "inner join Category C on C.CategoryID = R.CategoryID " +
                         "WHERE R.Title LIKE '%" + searchText + "%' OR C.CategoryName LIKE '%" + searchText + "%'";

            DBManager db = new DBManager(sql);

            return db.ExecuteSQL();
        }

        public DataTable AddRecipe(int categoryID, string title, string ingredient, string description)
        {
            var sql = "INSERT INTO Recipe (CategoryID, Title, Ingredient, RecipeDescription)" +
                      "VALUES ('" + categoryID + "','" + title + "','" + ingredient + "','" + description + "')";

            DBManager db = new DBManager(sql);
            return db.ExecuteSQL();
        }

        public DataTable UpdateRecipe(string title, string ingredient, string description, int categoryID, int id)
        {
            string sql = "UPDATE Recipe SET Title = '" + title + "', Ingredient = '" + ingredient + "', " +
                         "RecipeDescription = '" + description + "', CategoryID = '" + categoryID + "' where RecipeID = '" + id + "'";

            DBManager db = new DBManager(sql);
            return db.ExecuteSQL();
        }

        public DataTable DeleteRecipe(int id)
        {
            var sql = "DELETE FROM Recipe where RecipeID = '" + id + "'";

            DBManager db = new DBManager(sql);
            return db.ExecuteSQL();
        }
    }
}
