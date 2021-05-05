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
    public partial class LoginForm : Form
    {
        const string CONNECTIONTODB = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\User\source\repos\Login App\Login App\Database1.mdf; Integrated Security = True";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(isValid())
            {
                using (SqlConnection db = new SqlConnection(CONNECTIONTODB))
                {
                    db.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) from [dbo].[LoginTable] where UserName=@UserName and Password=@Password", db);
                    cmd.Parameters.AddWithValue("@UserName", UserNameTxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", PasswordTxt.Text.Trim());

                    var isCorrectPassword = cmd.ExecuteScalar();

                    /*string query = "SELECT * FROM LoginTable WHERE UserName = '" + UserNameTxt.Text.Trim() + "' AND Password = '" + PasswordTxt.Text.Trim() + "'";*/

                    if ((int)isCorrectPassword >= 1)
                    {
                        Form1 fr = new Form1();
                        this.Hide();
                        fr.Show();
                        db.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password not correct", "Error");
                        db.Close();
                    }                   
                }
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private bool isValid()
        {
            if(UserNameTxt.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Please enter your valid name", "Error");
                return false;
            }else if (PasswordTxt.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Please enter your valid password", "Error");
                return false;
            }
            return true;
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            Registration rg = new Registration();
            this.Hide();
            rg.Show();
        }
    }
}
