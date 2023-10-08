using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegAv
{
    public partial class AdminD : Form
    {
        public AdminD()
        {
            InitializeComponent();
        }

        private void labadd_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`Name`, `Surname`, `login`, `pass`) VALUES (@Name, @Surname, @login, @pass)", db.getConnection());

            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = Name.Text;
            command.Parameters.Add("@Surname", MySqlDbType.VarChar).Value = Surname.Text;
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password.Text;
          
            
            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Пользователь был добавлен");
            else
                MessageBox.Show("Пользователь не был добавлен");

            db.closeConnection();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form form = new Admin();
            form.Show();
            Hide();
        }
    }
}
