namespace TehnoSerice
{
    public partial class Form1 : Form
    {
        MyDataBaseHelper dataBaseHelper = new MyDataBaseHelper();

        Form2 form2;
        public Form1()
        {
            form2 = new Form2(this);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = dataBaseHelper.FindUser(textBox1.Text);

            if (user.UserId != 0)
            {
                MessageBox.Show("Логин занят!");
            }
            else
            {
                dataBaseHelper.SaveUser(new User { Login = textBox1.Text, Password = textBox2.Text, Role = User.UserRole.User });
                MessageBox.Show("Вы успешно зарегистрированы!");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            form2.Show();
        }
    }
}