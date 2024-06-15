using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TehnoSerice
{
    public partial class Form2 : Form
    {
        Form1 form1;
        MyDataBaseHelper helper = new MyDataBaseHelper();
        public Form2(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = helper.FindUser(textBox1.Text);

            if (user.UserId == 0 || user.Password != textBox2.Text)
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
            else
            {
                this.Hide();
                if (user.Role == User.UserRole.Manager)
                {
                    new Form5(user).Show();
                    return;
                }
                new Form3(user).Show();
            }
        }
    }
}
