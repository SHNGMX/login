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

namespace login
{
    public partial class login : Form
    {
        Class1 dataBase = new Class1();

        public int role = 0;

        int i = 0;

        

        public login()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Войти_Click(object sender, EventArgs e)
        {
            label4.Text = string.Format("", i++); 

            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id, login_user, password_user from Users where login_user ='{loginUser}' and password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {


                Form1 frm1 = new Form1();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
                Close();

            }
            else
                MessageBox.Show("Такого аккаунта не существует, либо были введены неверные данные", "Аккаунта не существует", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if(i==2)
                {
                MessageBox.Show("Такого аккаунта не существует еще 2 попытки");

            }
            if (i == 3)
            {
                MessageBox.Show("Такого аккаунта не существует еще 1 попытка");

            }
            if (i == 3)
            {
                timer1.Interval = 20000;
                timer1.Tick += timer1_Tick;
                timer1.Start();
                MessageBox.Show("Авторизация заблокирована на 20 сек.");
                Войти.Enabled = false;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Войти.Enabled = true;
            timer1.Stop();
        }
    }
}
