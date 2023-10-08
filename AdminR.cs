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
    public partial class AdminR : Form
    {
        public AdminR()
        {
            InitializeComponent();
            FillTextBoxWithData();
        }

        private void buttonCave_Click(object sender, EventArgs e)
        {

            DB db = new DB();
            {
                db.openConnection(); // Открыть соединение с базой данных

                // Получение значений из TextBox
                string Name = TextName.Text;
                string Surname = TextSurname.Text;
                string login = Textlogin.Text;
                string pass = Textpassword.Text;
                string id = idR.Text;


                // Создание SQL-запроса для обновления данных
                string query = "UPDATE users SET Name = @name, Surname = @Surname, login = @login, pass = @pass WHERE id = @id";

                MySqlCommand command = new MySqlCommand(query, db.getConnection());
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Surname", Surname);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@pass", pass);
                command.Parameters.AddWithValue("@id", id);



                // Выполнение SQL-запроса
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Данные успешно сохранены в базе данных.");
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при сохранении данных.");
                }

                db.closeConnection(); // Закрыть соединение с базой данных
            }
        }
        private void FillTextBoxWithData()
        {
            DB db = new DB();
            string query = "SELECT * FROM users"; // Запрос для выборки данных из базы данных
            MySqlCommand command = new MySqlCommand(query, db.getConnection());

            try
            {
                db.openConnection(); // Открыть соединение с базой данных

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Получить значения полей из результата запроса
                        string Name = reader.GetString("Name");
                        string Surname = reader.GetString("Surname");
                        string login = reader.GetString("login");
                        string pass = reader.GetString("pass");
                        string id = reader.GetString("id");


                        // Заполнить TextBox данными из базы данных

                        TextName.Text = Name;
                        TextSurname.Text = Surname;
                        Textlogin.Text = login;
                        Textpassword.Text = pass;
                        idR.Text = id;

                    }
                }

                db.closeConnection(); // Закрыть соединение с базой данных
            }
            catch (Exception ex)
            {
                // Обработка ошибки при выполнении запроса
                MessageBox.Show("Произошла ошибка при выполнении запроса: " + ex.Message);
            }
        }

        private void AdminR_Load(object sender, EventArgs e)
        {

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form form = new Admin();
            form.Show();
            Hide();
        }

        private void idR_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
