using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RecipeDatabase
{
    public partial class RecipeForm : Form
    {
        public RecipeForm()
        {
            InitializeComponent();
            LoadRecipe();
            LoadCategory();
        }

        public void LoadRecipe()
        {
            Recipe recipe = new Recipe();
            lstRecipe.DisplayMember = "Title";
            lstRecipe.ValueMember = "RecipeID";
            lstRecipe.DataSource = recipe.GetAllRecipe();
        }

        public void LoadCategory()
        {
            Category category = new Category();
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryID";
            cboCategory.DataSource = category.GetAllCategory();
        }

        private void lstRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = (int)lstRecipe.SelectedValue;

            Recipe recipe = new Recipe();
            var table = recipe.SelectedValueInList(id);

            txtTitle.Text = table.Rows[0].ItemArray[0].ToString();
            txtIngred.Text = table.Rows[0].ItemArray[1].ToString();
            txtDescrip.Text = table.Rows[0].ItemArray[2].ToString();
            cboCategory.SelectedValue = table.Rows[0].ItemArray[3];
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            var searchText = txtSearch.Text;
            Recipe recipe = new Recipe();
            var table = recipe.SearchForRecipe(searchText);

            var list = new List<Recipe>();
            foreach (DataRow row in table.Rows)
            {
                Recipe searchRecipe = new Recipe
                {
                    Title = row["Title"].ToString(),
                    RecipeID = Convert.ToInt32(row["RecipeID"]),
                    Category = new Category { CategoryName = row["CategoryName"].ToString() }
                };
                list.Add(searchRecipe);
            }

            lstRecipe.DisplayMember = "Title";
            lstRecipe.ValueMember = "RecipeID";
            lstRecipe.DataSource = list;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            var categoryID = (int)cboCategory.SelectedValue;
            var title = txtTitle.Text;
            var ingredient = txtIngred.Text;
            var description = txtDescrip.Text;

            Recipe recipe = new Recipe();
            var table = recipe.AddRecipe(categoryID, title, ingredient, description);

            MessageBox.Show("Recipe is saved!");
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            var title = txtTitle.Text;
            var ingredient = txtIngred.Text;
            var description = txtDescrip.Text;
            var categoryID = (int)cboCategory.SelectedValue;
            var id = (int)lstRecipe.SelectedValue;

            Recipe recipe = new Recipe();
            var table = recipe.UpdateRecipe(title, ingredient, description, categoryID, id);

            MessageBox.Show("Recipe is updated!");
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            var id = (int)lstRecipe.SelectedValue;
            Recipe recipe = new Recipe();
            recipe.DeleteRecipe(id);

            MessageBox.Show("Recipe is deleted!");
        }
    }
}
