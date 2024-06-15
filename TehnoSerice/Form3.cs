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
    public partial class Form3 : Form
    {
        MyDataBaseHelper helper = new MyDataBaseHelper();
        Form4 form4;
        List<ProblemType> problemTypes;
        User user;
        public Form3(User user)
        {
            form4 = new Form4(user, this);
            problemTypes = helper.GetAllProblem();
            this.user = user;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (var item in problemTypes)
            {
                comboBox1.Items.Add(item.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = new Order
            {
                AddDate = DateTime.Now,
                Description = textBox1.Text,
                ProblemId = problemTypes.Where(x => x.Name == comboBox1.SelectedItem).Select(x => x.ProblemTypeId).FirstOrDefault(),
                UserId = user.UserId,
                Status = "В ожидании",
            };
            helper.SaveOrder(order);
            MessageBox.Show("Заявка сформирована");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            form4.Show();
        }
    }
}
