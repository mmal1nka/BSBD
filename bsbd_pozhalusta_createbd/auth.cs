using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace bsbd_pozhalusta_createbd
{
    public partial class auth : Form
    {
        private SqlConnection sqlconnection = null;
        public auth()
        {
            InitializeComponent();
        }

        private void auth_Load(object sender, EventArgs e)
        {
            sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
            sqlconnection.Open();
            
        }
        private void sign_in_button_Click(object sender, EventArgs e)
        {
            try
            {
                var login = login_txt_box_auth.Text;
                var password = pass_txt_box_auth.Text;
                var phone_number = phone_txt_auth.Text;

                SqlDataAdapter adapter_admin = new SqlDataAdapter();
                DataTable dt_admin = new DataTable();
                SqlDataAdapter adapter_coach = new SqlDataAdapter();
                DataTable dt_coach = new DataTable();
                SqlDataAdapter adapter_player = new SqlDataAdapter();
                DataTable dt_player = new DataTable();

                string select_admins = $"select id, login, password, phone_number from admins where login = '{login}' and " +
                    $"password = '{password}' and phone_number = {phone_number}";
                string select_coaches = $"select id, login, password, PhoneNumber from Coach where login = '{login}' and " +
                    $"password = '{password}' and PhoneNumber = {phone_number}";
                string select_players = $"select id, login, password, PhoneNumber from Sportsman where login = '{login}' and " +
                    $"password = '{password}' and PhoneNumber = {phone_number}";

                SqlCommand select_admins_cmd = new SqlCommand(select_admins, sqlconnection);
                SqlCommand select_coaches_cmd = new SqlCommand(select_coaches, sqlconnection);
                SqlCommand select_players_cmd = new SqlCommand(select_players, sqlconnection);

                adapter_admin.SelectCommand = select_admins_cmd;
                adapter_admin.Fill(dt_admin);

                adapter_coach.SelectCommand = select_coaches_cmd;
                adapter_coach.Fill(dt_coach);

                adapter_player.SelectCommand = select_players_cmd;
                adapter_player.Fill(dt_player);

                if (dt_admin.Rows.Count == 1)
                {
                    var user = new user(dt_admin.Rows[0].ItemArray[1].ToString(), "admin", Convert.ToInt32(dt_admin.Rows[0].ItemArray[0]));
                    MessageBox.Show("Вход произведен успешно от имени администратора");
                    application application = new application(user);
                    this.Hide();
                    sqlconnection.Close();
                    application.Show();
                }
                else if (dt_coach.Rows.Count == 1)
                {
                    var user = new user(dt_coach.Rows[0].ItemArray[1].ToString(), "coach", Convert.ToInt32(dt_coach.Rows[0].ItemArray[0]));
                    MessageBox.Show("Вход произведен успешно от имени тренера");
                    application application = new application(user);
                    this.Hide();
                    sqlconnection.Close();
                    application.Show();
                }
                else if (dt_player.Rows.Count == 1)
                {
                    var user = new user(dt_player.Rows[0].ItemArray[1].ToString(), "player", Convert.ToInt32(dt_player.Rows[0].ItemArray[0]));
                    MessageBox.Show("Вход произведен успешно от имени игрока");
                    application application = new application(user);
                    this.Hide();
                    sqlconnection.Close();
                    application.Show();
                }
                else
                {
                    MessageBox.Show("Пользователя с такими данными не существует");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Неправильный ввод данных");
            }
        }

    }
}
