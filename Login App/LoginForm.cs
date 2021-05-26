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
    public partial class LoginForm : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["UsersConnection"].ConnectionString;

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
                using (SqlConnection db = new SqlConnection(connectionString))
                {

                    db.Open();
                    

                    SqlCommand cmd = new SqlCommand("select count(*) from LoginTable where UserName=@UserName and Password=@Password", db);

                    cmd.Parameters.AddWithValue("@UserName", UserNameTxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", PasswordTxt.Text.Trim());

                    var isCorrectPassword = cmd.ExecuteScalar();


                    if ((int)isCorrectPassword >= 1)
                    {
                        GetRoot();
                        User.UserName = UserNameTxt.Text.Trim();

                        MainWindow main = new MainWindow();
                        this.Hide();
                        main.Show();
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

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
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

        private void GetRoot()
        {
            using(SqlConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                SqlCommand cmd = new SqlCommand($"SELECT Root FROM LoginTable WHERE UserName = '{UserNameTxt.Text.Trim()}' AND Password = '{PasswordTxt.Text.Trim()}'", db);
                var Root = cmd.ExecuteScalar();

                if(string.IsNullOrWhiteSpace(Root.ToString()) || Root.ToString() == null)
                {
                    User.Root = "user";
                }
                else
                {
                    User.Root = Root.ToString();
                }
                db.Close();
            }
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            Registration rg = new Registration();
            this.Hide();
            rg.Show();
        }
    }
}