using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bsbd_pozhalusta_createbd
{
    public partial class application : Form
    {
        private readonly user _user;
        private SqlConnection sql_connection = null;
        public application(user user)
        {
            InitializeComponent();
            _user = user;
        }

        private void chech_user_status()
        {
            sql_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
            sql_connection.Open();
            switch (_user.user_status)
            {
                case "admin":
                    break;

                case "coach":
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    break;

                case "player":
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    break;
            }
        }
        private void Application_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.Application". При необходимости она может быть перемещена или удалена.
            dataGridView1.DataSource = FillDataGridView($"select * from Application");
            chech_user_status();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(date_app.Text != "" && date_app.Text != null &&
                    comboBox1.Text != "" && comboBox1.Text != null &&
                    app_idteam.Text != "" && app_idteam.Text != null)
                {
                    string sqlcommand = "INSERT INTO Application (DateOfSending, Status, Id_Team) VALUES"
                        + "(@DateOfSending, @Status, @Id_Team)";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@DateOfSending", Convert.ToDateTime(date_app.Text));
                    command.Parameters.AddWithValue("@Status", comboBox1.Text);
                    command.Parameters.AddWithValue("@Id_Team", Convert.ToInt32(app_idteam.Text));
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Application");
                    MessageBox.Show("Заявка успешно добавлена", "Success", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка выполнения запроса.\n" + err.Message,
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void teamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Team team = new Team(_user);
            this.Hide();
            team.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            auth auth = new auth();
            auth.Show();
        }

        private void sportsmanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sportsman sportsman = new Sportsman(_user);
            this.Hide();
            sportsman.Show();
        }


        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(C) ТУСУР, БИС, Букина Полина Германовна, 740-1, 2022", "О программе",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void coachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Coach coach = new Coach(_user);
            this.Hide();
            coach.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(app_idteam.Text != "" && app_idteam.Text != null)
                {
                    string sqlcommand = "DELETE FROM Application WHERE Id_Team = @Id_Team";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@Id_Team", Convert.ToInt32(app_idteam.Text));
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Application");
                    MessageBox.Show("Заявка успешно удалена", "Success", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Заполните поле идентификатора команды для удаления");
                    return;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка выполнения запроса.\n" + err.Message,
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "" && comboBox1.Text != null &&
                    app_idteam.Text != "" && app_idteam.Text != null)
                {
                    string sqlcommand = "UPDATE Application SET Status = @Status WHERE Id_Team = @Id_Team";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@Status", comboBox1.Text);
                    command.Parameters.AddWithValue("@Id_Team", Convert.ToInt32(app_idteam.Text));
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Application");
                    MessageBox.Show("Данные заявки успешно изменены", "Success", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка выполнения запроса.\n" + err.Message,
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var status = comboBox1.Text;
                dataGridView1.DataSource = FillDataGridView($"select * from Application where Status = '{status}' ");
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка выполнения запроса.\n" + err.Message,
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            
        }
        DataTable FillDataGridView(string sqlSelect)
        {
            sql_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
            sql_connection.Open();
            //Создаем объект command для SQL команды
            SqlCommand command = sql_connection.CreateCommand();
            //Заносим текст SQL запроса через параметр sqlSelect
            command.CommandText = sqlSelect;
            //Создаем объект adapter класса SqlDataAdapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Задаем адаптеру нужную команду, в данном случае команду Select
            adapter.SelectCommand = command;
            //Создаем объект table для последующего отображения результата запроса
            DataTable table = new DataTable();
            //заполним набор данных результатом запроса
            adapter.Fill(table);
            return table;
        }

        private void show_teams_button_Click(object sender, EventArgs e)
        {
                //SqlCommand thisCommand = sql_connection.CreateCommand();
            //SqlDataReader thisReader = null;
            //var answer = "";
            //thisCommand.CommandText = $"SELECT Name, Id_Coach FROM Team WHERE Id = {Convert.ToInt32(app_idteam.Text)}";
            //thisReader = thisCommand.ExecuteReader();
            //while (thisReader.Read())
            //{
            //    if (thisReader["Name"] == null || thisReader["Id_Coach"] == null)
            //    {
            //        answer += "Такой команды не найдено";
            //    }
            //    else if (thisReader["Name"] != null && thisReader["Id_Coach"] != null)
            //    {
            //        answer += $"name: {thisReader["Name"].ToString()} id coach: {thisReader["Id_Coach"].ToString()}";
            //    }
            //}
            //var info_str = $"Информация о команде с id: {app_idteam.Text}\n{answer}";
            //MessageBox.Show(info_str);
            //thisReader.Close();
            Team team = new Team(_user);
            team.Show();
        }

    }
}
