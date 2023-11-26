using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RecordingSystem_CarMaintenance
{
    public partial class FormRequest : Form
    {
        public FormRequest() => InitializeComponent();

        // Строка подключения
        public static string ConnectString = "Data Source=PC-LEONID29\\SQLEXPRESS;Integrated Security=True"; //'ВПИСАТЬ ИМЯ СЕРВЕРА'

        public static bool Request_Authorized { get; set; }

        public static string CustomerСontact { get; set; }

        private void Request_Load(object sender, EventArgs e)
        { if (Request_Authorized) Size = new Size(splitContainer1.SplitterDistance + 10, Height); }

        private void SendRequest_Click(object sender, EventArgs e)
        {
            if (TB_TransportModel.Text != "" & TB_DescriptionProblem.Text != "" & (TB_Email.Text != "" | TB_Telephone.Text != "") & TB_LastName.Text != "" & TB_FirstName.Text != "")
            {
                if (Request_Authorized)
                {
                    CustomerСontacts Сontact = null;

                    using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
                    {
                        SQL_Connection.Open();
                        string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[CustomerСontact_ID] @ID"; // SQL-запрос
                        SqlCommand SQL_Command = new SqlCommand(Request, SQL_Connection);
                        SQL_Command.Parameters.Add("@ID", SqlDbType.VarChar, 7); SQL_Command.Parameters["@ID"].Value = CustomerСontact; SqlDataReader Reader = SQL_Command.ExecuteReader();
                        while (Reader.Read()) Сontact = new CustomerСontacts((string)Reader.GetValue(0), (string)Reader.GetValue(1), (string)Reader.GetValue(2), (string)Reader.GetValue(3), (string)Reader.GetValue(4));
                        SQL_Connection.Close();
                    }

                    using (SqlConnection SQL_Connection = new SqlConnection(ConnectString))
                    {
                        SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                        string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[Request_ADD] @LastName, @FirstName, @MiddleName, @Email, @Telephone, @TransportModel, @DescriptionProblem, @Date"; // SQL-запрос
                        SQL_Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@LastName"].Value = Сontact.LastName;
                        SQL_Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@FirstName"].Value = Сontact.FirstName;
                        SQL_Command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@MiddleName"].Value = Сontact.MiddleName;
                        SQL_Command.Parameters.Add("@Email", SqlDbType.VarChar, 256); SQL_Command.Parameters["@Email"].Value = Сontact.Email;
                        SQL_Command.Parameters.Add("@Telephone", SqlDbType.VarChar, 20); SQL_Command.Parameters["@Telephone"].Value = Сontact.Telephone;
                        SQL_Command.Parameters.Add("@TransportModel", SqlDbType.NVarChar, 50); SQL_Command.Parameters["@TransportModel"].Value = TB_TransportModel.Text;
                        SQL_Command.Parameters.Add("@DescriptionProblem", SqlDbType.NVarChar, 1000000); SQL_Command.Parameters["@DescriptionProblem"].Value = TB_DescriptionProblem.Text;
                        SQL_Command.Parameters.Add("@Date", SqlDbType.Date); SQL_Command.Parameters["@Date"].Value = DateTime.Now.Date;
                        SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                    }
                }
                else
                {
                    using (SqlConnection SQL_Connection = new SqlConnection(ConnectString))
                    {
                        SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                        string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[Request_ADD] @LastName, @FirstName, @MiddleName, @Email, @Telephone, @TransportModel, @DescriptionProblem, @Date"; // SQL-запрос
                        SQL_Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@LastName"].Value = TB_LastName.Text;
                        SQL_Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@FirstName"].Value = TB_FirstName.Text;
                        SQL_Command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@MiddleName"].Value = TB_MiddleName.Text;
                        SQL_Command.Parameters.Add("@Email", SqlDbType.VarChar, 256); SQL_Command.Parameters["@Email"].Value = TB_Email.Text;
                        SQL_Command.Parameters.Add("@Telephone", SqlDbType.VarChar, 20); SQL_Command.Parameters["@Telephone"].Value = TB_Telephone.Text;
                        SQL_Command.Parameters.Add("@TransportModel", SqlDbType.NVarChar, 50); SQL_Command.Parameters["@TransportModel"].Value = TB_TransportModel.Text;
                        SQL_Command.Parameters.Add("@DescriptionProblem", SqlDbType.NVarChar, 1000000); SQL_Command.Parameters["@DescriptionProblem"].Value = TB_DescriptionProblem.Text;
                        SQL_Command.Parameters.Add("@Date", SqlDbType.Date); SQL_Command.Parameters["@Date"].Value = DateTime.Now.Date;
                        SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                    }
                }

                MessageBox.Show("Успешно создана заявка на ТО", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); Close();
            }
        }
    }
}