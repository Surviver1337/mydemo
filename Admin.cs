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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            listView1.Items.AddRange(CreateCollectionProduct().ToArray());
            DB db = new DB();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            // Проверить, выбран ли элемент в ListView
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string Name = selectedItem.SubItems[0].Text; // Получить значение первой подколонки

                // Создать SQL-запрос для удаления строки
                string query = "DELETE FROM users WHERE Name = @Name";
                MySqlCommand command = new MySqlCommand(query, db.getConnection());
                command.Parameters.AddWithValue("@Name", Name);

                try
                {
                    db.openConnection();
                    int rowsAffected = command.ExecuteNonQuery();
                    db.closeConnection();

                    if (rowsAffected > 0)
                    {
                        // Удаление успешно
                        MessageBox.Show("Строка успешно удалена из базы данных.");
                        // Удалить элемент из ListView
                        listView1.Items.Remove(selectedItem);
                    }
                    else
                    {
                        // Ничего не удалено
                        MessageBox.Show("Ошибка при удалении строки из базы данных.");
                    }
                }
                catch (Exception ex)
                {
                    // Ошибка при выполнении запроса
                    MessageBox.Show("Произошла ошибка при выполнении запроса: " + ex.Message);
                }
            }
            else
            {
                // Ни один элемент не выбран
                MessageBox.Show("Выберите строку, которую вы хотите удалить.");
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            Form form= new AdminD();
            form.Show();
            Hide();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public List<ListViewItem> CreateCollectionProduct()
        {
            List<ListViewItem> items = new List<ListViewItem>();
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM users", db.getConnection());
            db.closeConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                ListViewItem item = new ListViewItem();
                item.Text = row["Name"].ToString();
                item.SubItems.Add(row["Surname"].ToString());
                item.SubItems.Add(row["Login"].ToString());
                item.SubItems.Add(row["Pass"].ToString());
                items.Add(item);
            }
            return items;
        }

        private void buttonRegedit_Click(object sender, EventArgs e)
        {
            // Проверить, выбран ли элемент в ListView
            if (listView1.SelectedItems.Count > 0)
            {
                // Получить выбранный элемент
                ListViewItem selectedItem = listView1.SelectedItems[0];


                Form form3 = new AdminR();
                form3.Show();
                Hide();
            }
            else
            {
                // Ни один элемент не выбран
                MessageBox.Show("Выберите строку, которую вы хотите редактировать.");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close   ();
        }
    }
    
}
