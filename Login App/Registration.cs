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

namespace Login_App
{
    public partial class Registration : Form
    {
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
            if (isValid())
            {
                using(SqlConnection db = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\User\source\repos\Login App\Login App\Database1.mdf; Integrated Security = True"))
                {
                    string query = $"INSERT INTO LoginTable (UserName, Password) values('{txtName.Text.TrimStart()}', '{txtPassword.Text.TrimStart()}')";

                    SqlDataAdapter sda = new SqlDataAdapter(query, db);
                    DataTable dta = new DataTable();
                    sda.Fill(dta);

                    MessageBox.Show("Registration Done!", "Success!");
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
    }
}
