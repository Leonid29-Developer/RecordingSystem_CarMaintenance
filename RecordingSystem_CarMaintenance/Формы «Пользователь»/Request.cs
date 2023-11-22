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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace RecordingSystem_CarMaintenance
{
    public partial class Request : Form
    {
        public Request() => InitializeComponent();

        // Строка подключения
        public static string ConnectString = "Data Source=PC-LEONID29\\SQLEXPRESS;Integrated Security=True"; //'ВПИСАТЬ ИМЯ СЕРВЕРА'

        public static bool Request_Authorized { get; set; }

        private void Request_Load(object sender, EventArgs e)
        {
            Request_Authorized = false;
            if (Request_Authorized) Size = new Size(splitContainer1.SplitterDistance + 10, Height);
        }

        private void SendRequest_Click(object sender, EventArgs e)
        {
            if (TB_TransportModel.Text != "" & TB_DescriptionProblem.Text != "" & (TB_Email.Text != "" | TB_Telephone.Text != "") & TB_LastName.Text != "" & TB_FirstName.Text != "")
            {
                using (SqlConnection SQL_Connection = new SqlConnection(ConnectString))
                {
                    SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                    string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[Request_ADD] @LastName, @FirstName, @MiddleName, @Email, @Telephone, @TransportModel, @DescriptionProblem"; // SQL-запрос

                    SQL_Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@LastName"].Value = TB_LastName.Text;
                    SQL_Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@FirstName"].Value = TB_FirstName.Text;
                    SQL_Command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@MiddleName"].Value = TB_MiddleName.Text;
                    SQL_Command.Parameters.Add("@Email", SqlDbType.VarChar, 256); SQL_Command.Parameters["@Email"].Value = TB_Email.Text;
                    SQL_Command.Parameters.Add("@Telephone", SqlDbType.VarChar, 20); SQL_Command.Parameters["@Telephone"].Value = TB_Telephone.Text;
                    SQL_Command.Parameters.Add("@TransportModel", SqlDbType.NVarChar, 50); SQL_Command.Parameters["@TransportModel"].Value = TB_TransportModel.Text;
                    SQL_Command.Parameters.Add("@DescriptionProblem", SqlDbType.NVarChar, 1000000); SQL_Command.Parameters["@DescriptionProblem"].Value = TB_DescriptionProblem.Text;
                    SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                }
            }
        }
    }
}