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

namespace bsbd_pozhalusta_createbd
{
    public partial class Team : Form
    {
        private readonly user _user;
        private SqlConnection sql_connection = null;
        public Team(user user)
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
                    dataGridView1.DataSource = FillDataGridView($"select * from Team ");
                    break;

                case "coach":
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    dataGridView1.DataSource = FillDataGridView($"select * from Team ");
                    break;

                case "player":
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    dataGridView1.DataSource = FillDataGridView($"select * from Team ");
                    break;
            }
        }
        private void Team_Load(object sender, EventArgs e)
        {
            chech_user_status();
        }

        private void coachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Coach coach = new Coach(_user);
            this.Hide();
            coach.Show();
        }

        private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            application application = new application(_user);
            this.Hide();
            application.Show();
        }

        private void sportsmanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sportsman sportsman = new Sportsman(_user);
            this.Hide();
            sportsman.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            auth auth = new auth();
            auth.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(C) ТУСУР, БИС, Букина Полина Германовна, 740-1, 2022", "О программе",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(idcoach_team.Text != "" && idcoach_team.Text != null &&
                    name_team.Text != "" && name_team.Text != null)
                {
                    string sqlcommand = "INSERT INTO Team (Name, Id_Coach) VALUES"
                        + "(@Name, @Id_Coach)";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@Id_Coach", Convert.ToInt32(idcoach_team.Text));
                    command.Parameters.AddWithValue("@Name", name_team.Text);
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Team ");
                    MessageBox.Show("Команда успешно добавлена", "Success", MessageBoxButtons.OK);
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


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (idteam_txt_team.Text != "" && idteam_txt_team.Text != null &&
                    name_team.Text != "" && name_team.Text != null)
                {
                    string sqlcommand = "UPDATE Team SET Name = @Name WHERE Id = @Id";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@Name", name_team.Text);
                    command.Parameters.AddWithValue("@Id", Convert.ToInt32(idteam_txt_team.Text));
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Team ");
                    MessageBox.Show("Данные команды успешно изменены", "Success", MessageBoxButtons.OK);
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
                dataGridView1.DataSource = FillDataGridView($"select * from Team where Name = '{name_team.Text}' ");
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(idteam_txt_team.Text != "" && idteam_txt_team.Text != null)
                {
                    string sqlcommand = "DELETE FROM Team WHERE Id = @Id";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@Id", Convert.ToInt32(idteam_txt_team.Text));
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Team ");
                    MessageBox.Show("Команда успешно удалена", "Success", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Заполните поле номера команды для удаления");
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

        private void show_coach_info_Click(object sender, EventArgs e)
        {
            //SqlCommand thisCommand = sql_connection.CreateCommand();
            //SqlDataReader thisReader = null;
            //var answer = "";
            //thisCommand.CommandText = $"SELECT Surname, Name, Middlename, Rank FROM Coach WHERE Id = {Convert.ToInt32(idcoach_team.Text)}";
            //thisReader = thisCommand.ExecuteReader();
            //while (thisReader.Read())
            //{
            //    if (thisReader["Surname"] == null || thisReader["Name"] == null || thisReader["Middlename"] == null)
            //    {
            //        answer += "Такого тренера нету";
            //    }
            //    else if (thisReader["Surname"] != null && thisReader["Name"] != null && thisReader["Middlename"] != null)
            //    {
            //        answer += $"Surname: {thisReader["Surname"].ToString()} Name: {thisReader["Name"].ToString()} Middlename: {thisReader["Middlename"].ToString()} Rank: {thisReader["Rank"].ToString()}";
            //    }
            //}
            //var info_str = $"Информация о тренере с id: {idcoach_team.Text}\n{answer}";
            //MessageBox.Show(info_str);
            //thisReader.Close();
            Coach Coach = new Coach(_user);
            Coach.Show();
        }
    }
}
