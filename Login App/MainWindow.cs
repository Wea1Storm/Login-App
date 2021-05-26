using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Login_App
{
    public partial class MainWindow : Form
    {     
        public MainWindow()
        {

            InitializeComponent();
        }

        private void RootLable_Click(object sender, EventArgs e)
        {
            
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            UserNameLabel.Text = User.UserName;
            RootLable.Text = User.Root;
        }

        private void UserNameLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
