using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Login_App
{
    public partial class Registration : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["UsersConnection"].ConnectionString;

        public Registration()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            this.Hide();
            lf.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (isValid() && isLoginExist())
            {
                using(SqlConnection db = new SqlConnection(connectionString))
                {
                    string query = $"INSERT INTO LoginTable (UserName, Password) values('{txtName.Text.TrimStart()}', '{txtPassword.Text.TrimStart()}')";

                    db.Open();

                    SqlDataAdapter sda = new SqlDataAdapter(query, db);
                    DataTable dta = new DataTable();
                    sda.Fill(dta);

                    db.Close();

                    MessageBox.Show("Registration Done!", "Success!");

                    LoginForm lf = new LoginForm();
                    this.Hide();
                    lf.Show();
                }
            }
        }

        private bool isValid()
        {
            if (txtName.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Please enter your valid name", "Error");
                return false;
            }
            else if (txtPassword.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Please enter your valid password", "Error");
                return false;
            }
            else if (txtConfirmPassword.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Please enter confirm password", "Error");
                return false;
            }

            if(txtPassword.Text.TrimStart() != txtConfirmPassword.Text.TrimStart())
            {
                MessageBox.Show("Password and confirm password aren't equals", "Error");
                return false;
            }

            return true;
        }

        private bool isLoginExist()
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM LoginTable WHERE UserName = '{txtName.Text.TrimStart()}'";

                db.Open();

                SqlDataAdapter sda = new SqlDataAdapter(query, db);
                DataTable dta = new DataTable();
                sda.Fill(dta);

                db.Close();

                if(dta.Rows.Count > 0)
                {
                    MessageBox.Show("Error, this login already exist!", "Error!");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}