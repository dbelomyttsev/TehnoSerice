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
    public partial class Form4 : Form
    {
        MyDataBaseHelper helper = new MyDataBaseHelper();
        List<Order> orders;
        User user;
        Form3 form3;
        public Form4(User user, Form3 form3)
        {
            this.form3 = form3;
            this.user = user;
            orders = helper.GetAllOrders();
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            foreach (var item in orders)
            {
                if (item.UserId == user.UserId)
                {
                    flowLayoutPanel1.Controls.Add(new UserOrderItem(item, user));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            form3.Show();
        }
    }
}
