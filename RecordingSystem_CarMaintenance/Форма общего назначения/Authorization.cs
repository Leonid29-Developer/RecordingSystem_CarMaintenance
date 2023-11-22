﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordingSystem_CarMaintenance
{
    public partial class Authorization : Form
    {
        public Authorization() => InitializeComponent();

        private void Button_Input_Click(object sender, EventArgs e)
        {
            bool T = false;
            if (TB_Login.Text != "" & TB_Password.Text != "")
            {
                DataTable DATA = new DataTable();

                using (SqlConnection SQL_Connection = new SqlConnection(Request.ConnectString))
                {
                    SQL_Connection.Open(); using (SqlCommand CMD = new SqlCommand($"EXEC [Interactive_FairyTales].[dbo].[Authorization] '{TB_Login.Text}','{TB_Password.Text}'", SQL_Connection)) 
                    using (SqlDataReader Reader = CMD.ExecuteReader()) DATA.Load(Reader);  SQL_Connection.Close();
                }

                if (T == true) switch ((string)DATA.Rows[0][2])
                    {
                        case "User": { Hide(); ClientAccount.CustomerСontact = (string)DATA.Rows[0][3]; new ClientAccount().ShowDialog(); Close(); } break;
                        case "Master": { /*Hide(); ClientAccount.CustomerСontact = (string)DATA.Rows[0][3]; new ClientAccount().ShowDialog();*/ Close(); } break;
                        case "Administrator": { /*Hide(); ClientAccount.CustomerСontact = (string)DATA.Rows[0][3]; new ClientAccount().ShowDialog();*/ Close(); } break;
                    }
            }
            else T = false; if (T == false) MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Authorization_FormClosed(object sender, FormClosedEventArgs e) => Application.OpenForms["StartingForm"].Show();

        // Кнопка быстрой авторизации
        private void pictureBox1_Click(object sender, EventArgs e) { TB_Login.Text = "Cerieya"; TB_Password.Text = "ZOiWV334"; }

        private void pictureBox2_Click(object sender, EventArgs e) { TB_Login.Text = "Bdillw"; TB_Password.Text = "4DU08Lls"; }

        private void pictureBox3_Click(object sender, EventArgs e) { TB_Login.Text = "Dirope"; TB_Password.Text = "uTk9x65o"; }
    }
}
