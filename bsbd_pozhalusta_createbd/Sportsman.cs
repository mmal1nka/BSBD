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
    public partial class Sportsman : Form
    {
        private readonly user _user;
        private SqlConnection sql_connection = null;
        public Sportsman(user user)
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

        private void Sportsman_Load(object sender, EventArgs e)
        {
            sql_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
            sql_connection.Open();
            switch (_user.user_status)
            {
                case "admin":
                    dataGridView1.DataSource = FillDataGridView($"select * from Sportsman");
                    chech_user_status();
                    break;

                case "coach":
                    //dataGridView1.DataSource = FillDataGridView($"select Id, Surname, Name, Middlename, DateOfBirth, Rate, Id_Team from Sportsman");
                    this.sportsmanTableAdapter.FillBynotAdminViewPlayers(this.database1DataSet.Sportsman);
                    chech_user_status();
                    break;

                case "player":
                    //dataGridView1.DataSource = FillDataGridView($"select Id, Surname, Name, Middlename, DateOfBirth, Rate, Id_Team from Sportsman");
                    this.sportsmanTableAdapter.FillBynotAdminViewPlayers(this.database1DataSet.Sportsman);
                    chech_user_status();
                    break;
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
        private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            application application = new application(_user);
            this.Hide();
            application.Show();
        }

        private void coachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Coach coach = new Coach(_user);
            this.Hide();
            coach.Show();
        }

        private void teamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Team team = new Team(_user);
            this.Hide();
            team.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(C) ТУСУР, БИС, Букина Полина Германовна, 740-1, 2022", "О программе",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (Surname_txt_sportsman.Text != "" && Surname_txt_sportsman.Text != null &&
                    name_txt_sportsman.Text != "" && name_txt_sportsman.Text != null &&
                    middlename_txt_sportsman.Text != "" && middlename_txt_sportsman.Text != null &&
                    rate_txt_sportsman.Text != "" && rate_txt_sportsman.Text != null &&
                    idteam_txt_sportsman.Text != "" && idteam_txt_sportsman.Text != null &&
                    login_txt_sportsman.Text != "" && login_txt_sportsman.Text != null &&
                     pass_txt_sportsman.Text != "" && pass_txt_sportsman.Text != null &&
                     rate_txt_sportsman.Text != "" && rate_txt_sportsman.Text != null &&
                     date_txt_sportsman.Text != "" && date_txt_sportsman.Text != null)
                {
                    string sqlcommand = "INSERT INTO Sportsman (Surname, Name, Middlename, DateOfBirth, Rate, PhoneNumber, Id_Team," +
                    "login, password) VALUES" + "(@Surname, @Name, @Middlename, @DateOfBirth, @Rate, @PhoneNumber, @Id_Team, @login, @password)";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@Surname", Surname_txt_sportsman.Text);
                    command.Parameters.AddWithValue("@Name", name_txt_sportsman.Text);
                    command.Parameters.AddWithValue("@Middlename", middlename_txt_sportsman.Text);
                    command.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(date_txt_sportsman.Text));
                    command.Parameters.AddWithValue("@Rate", Convert.ToInt32(rate_txt_sportsman.Text));
                    command.Parameters.AddWithValue("@PhoneNumber", phone_txt_sportsman.Text);
                    command.Parameters.AddWithValue("@Id_Team", Convert.ToInt32(idteam_txt_sportsman.Text));
                    command.Parameters.AddWithValue("@login", login_txt_sportsman.Text);
                    command.Parameters.AddWithValue("@password", pass_txt_sportsman.Text);
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Sportsman");
                    MessageBox.Show("Спортсмен успешно добавлен", "Success", MessageBoxButtons.OK);
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
                if (phone_txt_sportsman.Text != "" && phone_txt_sportsman.Text != null)
                {
                    string sqlcommand = "DELETE FROM Sportsman WHERE PhoneNumber = @PhoneNumber";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@PhoneNumber", phone_txt_sportsman.Text);
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Sportsman");
                    MessageBox.Show("Спортсмен успешно удален", "Success", MessageBoxButtons.OK);
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
                if(rate_txt_sportsman.Text != "" && rate_txt_sportsman.Text != null)
                {
                    string sqlcommand = "UPDATE Sportsman SET Rate = @Rate WHERE PhoneNumber = @PhoneNumber";
                    SqlCommand command = sql_connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@Rate", Convert.ToInt32(rate_txt_sportsman.Text));
                    command.Parameters.AddWithValue("@PhoneNumber", phone_txt_sportsman.Text);
                    command.ExecuteNonQuery();
                    dataGridView1.DataSource = FillDataGridView($"select * from Sportsman");
                    MessageBox.Show("Данные спортсмена успешно изменены", "Success", MessageBoxButtons.OK);
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
                dataGridView1.DataSource = FillDataGridView($"select * from Sportsman where PhoneNumber = {phone_txt_sportsman.Text}");
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка выполнения запроса.\n" + err.Message,
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
          
        }

        private void show_teams_button_Click(object sender, EventArgs e)
        {
            Team team = new Team(_user);
            team.Show();
        }
    }
}
