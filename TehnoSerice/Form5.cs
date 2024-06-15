using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TehnoSerice
{
    public partial class Form5 : Form
    {
        MyDataBaseHelper helper = new MyDataBaseHelper();
        List<Order> orders = new List<Order>();
        User user;
        public Form5(User user)
        {
            this.user = user;
            orders = helper.GetAllOrders();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            foreach (var item in orders)
            {
                flowLayoutPanel1.Controls.Add(new UserOrderItem(item, user));
            }
        }
    }
}
