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
    public partial class Coach : Form
    {
        private readonly user _user;
        private SqlConnection sql_connection = null;
        public Coach(user user)
        {
            InitializeComponent();
            _user = user;
        }
        private void chech_user_status()
        {
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
        private void Coach_Load(object sender, EventArgs e)
        {
            sql_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
            sql_connection.Open();
            switch (_user.user_status)
            {
                case "admin":
                    dataGridView1.DataSource = FillDataGridView($"select * from Coach");
                    this.coachTableAdapter.Fill(this.database1DataSet.Coach);
                    chech_user_status();
                    break;

                case "coach":
                   // dataGridView1.DataSource = FillDataGridView($"select Id, Surname, Name, Middlename, Rank from Coach");
                    this.coachTableAdapter.FillBynotAdminViewCoaches(this.database1DataSet.Coach);
                    chech_user_status();
                    break;

                case "player":
                    //dataGridView1.DataSource = FillDataGridView($"select Id, Surname, Name, Middlename, Rank from Coach");
                    this.coachTableAdapter.FillBynotAdminViewCoaches(this.database1DataSet.Coach);
                    chech_user_status();
                    break;
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(C) ТУСУР, БИС, Букина Полина Германовна, 740-1, 2022", "О программе",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void teamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Team team = new Team(_user);
            this.Hide();
            team.Show();
        }

        private void sportsmanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sportsman sportsman = new Sportsman(_user);
            this.Hide();
            sportsman.Show();
        }

        private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            application application = new application(_user);
            this.Hide();
            application.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            auth auth = new auth();
            auth.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Surname_coach.Text!= "" && Surname_coach.Text!=null &&
                    Name_coach.Text != "" && Name_coach.Text != null &&
                    Middlename_coach.Text != "" && Middlename_coach.Text != null &&
                    PhoneNumber_coach.Text != "" && PhoneNumber_coach.Text != null &&
                    rank_coach.Text != "" && rank_coach.Text != null &&
                    login_coach.Text != "" && login_coach.Text != null &&
                     pass_coach.Text != "" && pass_coach.Text != null)
                {
                    string sqlcommand = "INSERT INTO Coach (Surname, Name, Middlename, PhoneNumber, Rank, login, password) VALUES" +
                        "(@Surname, @Name, @Middlename, @PhoneNumber, @Rank, @login, @password)";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@Surname", Surname_coach.Text);
                    command.Parameters.AddWithValue("@Name", Name_coach.Text);
                    command.Parameters.AddWithValue("@Middlename", Middlename_coach.Text);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber_coach.Text);
                    command.Parameters.AddWithValue("@Rank", rank_coach.Text);
                    command.Parameters.AddWithValue("@login", login_coach.Text);
                    command.Parameters.AddWithValue("@password", pass_coach.Text);
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Coach");
                    MessageBox.Show("Тренер успешно добавлен", "Success", MessageBoxButtons.OK);
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (PhoneNumber_coach.Text != "" && PhoneNumber_coach.Text != null) 
                {
                    string sqlcommand = "DELETE FROM Coach WHERE PhoneNumber = @PhoneNumber";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber_coach.Text);
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Coach");
                    MessageBox.Show("Тренер успешно удален", "Success", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Заполните поле номера телефона для удаления");
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
                if(rank_coach.Text != "" && rank_coach.Text != null)
                {
                string sqlcommand = "UPDATE Coach SET Rank = @Rank WHERE PhoneNumber = @PhoneNumber";
                SqlCommand command = sql_connection.CreateCommand();
                command.CommandText = sqlcommand;
                command.Parameters.AddWithValue("@Rank", rank_coach.Text);
                command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber_coach.Text);
                command.ExecuteNonQuery();
                dataGridView1.DataSource = FillDataGridView($"select * from Coach");
                MessageBox.Show("Данные тренера успешно изменены", "Success", MessageBoxButtons.OK);
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
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = FillDataGridView($"select * from Coach where PhoneNumber = {PhoneNumber_coach.Text}");
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка выполнения запроса.\n" + err.Message,
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            
        }
    }
}
