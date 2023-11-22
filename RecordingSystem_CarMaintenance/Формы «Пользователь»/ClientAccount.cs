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

namespace RecordingSystem_CarMaintenance
{
    public partial class ClientAccount : Form
    {
        public ClientAccount() => InitializeComponent();

        public static string CustomerСontact { get; set; }

        private CustomerСontacts Сontact {get; set; }

        private void ClientAccount_Load(object sender, EventArgs e)
        {
            using (SqlConnection SQL_Connection = new SqlConnection(Request.ConnectString))
            {
                SQL_Connection.Open(); string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[CustomerСontact_ID] @ID"; // SQL-запрос
                SqlCommand SQL_Command = new SqlCommand(Request, SQL_Connection); 
                SQL_Command.Parameters.Add("@ID", SqlDbType.VarChar, 6); SQL_Command.Parameters["@ID"].Value = CustomerСontact; SqlDataReader Reader = SQL_Command.ExecuteReader();
                while (Reader.Read()) Сontact = new CustomerСontacts((string)Reader.GetValue(0), (string)Reader.GetValue(1), (string)Reader.GetValue(2), (string)Reader.GetValue(3), (string)Reader.GetValue(4));
                SQL_Connection.Close();
            }
        }
    }
}
