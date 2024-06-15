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
    public partial class UserOrderItem : UserControl
    {
        public Order Order { get; set; }
        MyDataBaseHelper helper = new MyDataBaseHelper();
        User user;
        public UserOrderItem(Order order, User user)
        {
            Order = order;
            this.user = user;
            InitializeComponent();
        }

        private void UserOrderItem_Load(object sender, EventArgs e)
        {
            label1.Text = Order.Description;
            comboBox1.Text = Order.Status;
            comboBox1.Enabled = user.Role == User.UserRole.User ? false : true;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {



            MessageBox.Show(".");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Order.Status == "Готово")
            {
                return;
            }
            if (comboBox1.SelectedItem == "Готово")
            {
                Order.EndDate = DateTime.Now;
                Order.Status = "Готово";
                helper.UpdateOrder(Order);
                string time = (Order.AddDate - Order.EndDate).Value.Days.ToString();
                label1.Text += time.Trim('-');
            }

        }
    }
}
