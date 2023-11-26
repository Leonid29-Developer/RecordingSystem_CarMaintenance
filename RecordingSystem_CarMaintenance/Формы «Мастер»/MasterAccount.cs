using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RecordingSystem_CarMaintenance
{
    public partial class MasterAccount : Form
    {
        public MasterAccount() => InitializeComponent();

        public static string CustomerСontact { get; set; }

        private List<MaintenanceRequests> Requests = new List<MaintenanceRequests>();

        private void MasterAccount_Load(object sender, EventArgs e)
        {
            Requests.Clear();

            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
                SQL_Connection.Open(); string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[MaintenanceRequests_ALL]"; // SQL-запрос
                SqlCommand SQL_Command = new SqlCommand(Request, SQL_Connection); SqlDataReader Reader = SQL_Command.ExecuteReader();
                while (Reader.Read())
                {
                    int COST = 0; if (Reader.GetValue(4) != DBNull.Value) COST = (int)Reader.GetValue(4); CustomerСontacts Contact_Client = new CustomerСontacts("NULL", "", "", "", ""), Contact_Master = new CustomerСontacts("NULL", "", "", "", "");

                    using (SqlConnection SQL_Connection2 = new SqlConnection(FormRequest.ConnectString))
                    {
                        SQL_Connection2.Open();
                        Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[CustomerСontact_ID] @ID"; // SQL-запрос
                        SqlCommand SQL_Command2 = new SqlCommand(Request, SQL_Connection2);
                        SQL_Command2.Parameters.Add("@ID", SqlDbType.VarChar, 7); SQL_Command2.Parameters["@ID"].Value = (string)Reader.GetValue(1); SqlDataReader Reader2 = SQL_Command2.ExecuteReader();
                        while (Reader2.Read()) Contact_Client = new CustomerСontacts((string)Reader2.GetValue(0), (string)Reader2.GetValue(1), (string)Reader2.GetValue(2), (string)Reader2.GetValue(3), (string)Reader2.GetValue(4));
                    }

                    if (Reader.GetValue(7) != DBNull.Value) using (SqlConnection SQL_Connection2 = new SqlConnection(FormRequest.ConnectString))
                        {
                            SQL_Connection2.Open();
                            Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[CustomerСontact_ID] @ID"; // SQL-запрос
                            SqlCommand SQL_Command2 = new SqlCommand(Request, SQL_Connection2);
                            SQL_Command2.Parameters.Add("@ID", SqlDbType.VarChar, 7); SQL_Command2.Parameters["@ID"].Value = (string)Reader.GetValue(7); SqlDataReader Reader2 = SQL_Command2.ExecuteReader();
                            while (Reader2.Read())
                            {
                                Contact_Master = new CustomerСontacts((string)Reader2.GetValue(0), (string)Reader2.GetValue(1), (string)Reader2.GetValue(2), (string)Reader2.GetValue(3), (string)Reader2.GetValue(4));
                                Contact_Master.IDA((string)Reader.GetValue(7));
                            }
                            SQL_Connection2.Close();
                        }

                    Requests.Add(new MaintenanceRequests((string)Reader.GetValue(0), (string)Reader.GetValue(2), (string)Reader.GetValue(3), COST, (string)Reader.GetValue(5), (string)Reader.GetValue(6), Contact_Client, Contact_Master, (DateTime)Reader.GetValue(8)));
                }
                SQL_Connection.Close();
            }

            UpdateOut();
        }

        private void UpdateOut()
        {
            Table.Controls.Clear();

            if (Requests.Count > 0) foreach (MaintenanceRequests Request in Requests) if (Request.Contact_Master.ID == CustomerСontact & Request.StatusWork == "В обработке")
                    {
                        Panel NewRequest = new Panel { Name = $"R_{Request.ID}", Size = new Size(Table.Width, 352), BorderStyle = BorderStyle.FixedSingle };
                        {
                            Panel Frame1 = new Panel { Name = "Frame1", Size = new Size(NewRequest.Width, 110), BorderStyle = BorderStyle.FixedSingle };
                            {
                                Label Number_Date = new Label { Text = $"Заявка №{Request.ID.Remove(0, 2)} от {Request.Date.Day}.{Request.Date.Month}.{Request.Date.Year}", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width, 40), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle };

                                Label Cost = new Label { Text = $"Предварительная cтоимость работы: ", Font = new Font("Times New Roman", 18), Size = new Size(390, 50), TextAlign = ContentAlignment.MiddleLeft, Left = 80, Top = 48 };

                                MaskedTextBox TB_Cost = new MaskedTextBox { Name = "Cost", Font = new Font("Times New Roman", 19), TextAlign = HorizontalAlignment.Center, Mask = "000 000 000 руб", Size = new Size(190, 50), Left = Cost.Left + Cost.Width, Top = 56 };
                                if (Request.Cost != 0) TB_Cost.Text = Request.Cost.ToString(); else TB_Cost.Text = "000000000";

                                PictureBox Pic_UpdateCost = new PictureBox { BackgroundImage = Properties.Resources.Picture_Notepad, BackgroundImageLayout = ImageLayout.Stretch, Size = new Size(50, 50), Left = 750, Top = 48, BorderStyle = BorderStyle.FixedSingle };
                                Label Lab_UpdateCost = new Label { Name = Request.ID, Text = "Зафиксировать", Font = new Font("Times New Roman", 18), Size = new Size(180, 50), TextAlign = ContentAlignment.MiddleCenter, Left = Pic_UpdateCost.Left + 50, Top = 48, BorderStyle = BorderStyle.FixedSingle };
                                Lab_UpdateCost.Click += RequestUpdateCost_Click;

                                Frame1.Controls.Add(Number_Date); Frame1.Controls.Add(Cost); Frame1.Controls.Add(TB_Cost); Frame1.Controls.Add(Pic_UpdateCost); Frame1.Controls.Add(Lab_UpdateCost); NewRequest.Controls.Add(Frame1);
                            }

                            Panel Frame2 = new Panel { Size = new Size(NewRequest.Width, 120), BorderStyle = BorderStyle.FixedSingle, Top = Frame1.Height };
                            {
                                Label Model = new Label { Text = $"Модель транспорта: {Request.TransportModel}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width - 55, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 23, BorderStyle = BorderStyle.FixedSingle };

                                Label ProblemDescription = new Label { Text = $"Описание проблемы: {Request.DescriptionProblem}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width - 55, 80), TextAlign = ContentAlignment.MiddleLeft, Left = 23, Top = 40, BorderStyle = BorderStyle.FixedSingle };

                                Frame2.Controls.Add(Model); Frame2.Controls.Add(ProblemDescription); NewRequest.Controls.Add(Frame2);
                            }

                            Panel Frame3 = new Panel { Size = new Size(NewRequest.Width / 2, 120), BorderStyle = BorderStyle.FixedSingle, Top = Frame1.Height + Frame2.Height, Left = NewRequest.Width / 4 };
                            {
                                Label Client = new Label { Text = $"Данные клиента", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width / 2, 40), TextAlign = ContentAlignment.MiddleCenter, Left = 10 };

                                Label FIO = new Label { Text = $"Обращение: {Request.Contact_Client.FirstName} {Request.Contact_Client.MiddleName}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 2 - 60, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 40 };

                                Label Telephone1 = new Label { Text = $"Номер телефона: ", Font = new Font("Times New Roman", 16), Size = new Size(180, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 70 };
                                Label Telephone2 = new Label { Text = $"{Request.Contact_Client.Telephone}", Font = new Font("Times New Roman", 16), Size = new Size(282, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30 + Telephone1.Width, Top = 70, ForeColor = Color.Blue };

                                Frame3.Controls.Add(Client); Frame3.Controls.Add(FIO); Frame3.Controls.Add(Telephone1); Frame3.Controls.Add(Telephone2); NewRequest.Controls.Add(Frame3);
                            }

                            Table.Controls.Add(NewRequest);
                        }
                    }

            Table.AutoScroll = true;
        }

        private void RequestUpdateCost_Click(object sender, EventArgs e)
        {
            string[] Named = new string[2]; Label Click = (Label)sender;

            foreach (Panel Control in Table.Controls) if (Control.Name[0] == 'R' & Control.Name.Remove(0, 2) == Click.Name)
                {
                    Named[0] = Control.Name.Remove(0, 2); foreach (Panel Frame in Control.Controls) if (Frame.Name == "Frame1") foreach (var Element in Frame.Controls) if (Element.GetType().ToString() == "System.Windows.Forms.MaskedTextBox")
                                {
                                    MaskedTextBox Element_MaskedTextBox = (MaskedTextBox)Element;
                                    if (Element_MaskedTextBox.Name == "Cost") { Named[1] = ""; string[] G = Element_MaskedTextBox.Text.Split(' '); for (int i = 0; i < G.Length - 1; i++) Named[1] += G[i]; }
                                }
                }

            int Cost = 0; if (Named[1] != "") Cost = Convert.ToInt32(Named[1]);

            foreach (MaintenanceRequests Requested in Requests) if (Requested.ID == Named[0]) using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
                    {
                        SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                        string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[MaintenanceRequests_Update] @RequestID, @Cost, @StatusWork, @StatusPayment"; // SQL-запрос
                        SQL_Command.Parameters.Add("@RequestID", SqlDbType.VarChar, 8); SQL_Command.Parameters["@RequestID"].Value = Named[0];
                        SQL_Command.Parameters.Add("@Cost", SqlDbType.Int); SQL_Command.Parameters["@Cost"].Value = Cost;
                        SQL_Command.Parameters.Add("@StatusWork", SqlDbType.NVarChar, 16); SQL_Command.Parameters["@StatusWork"].Value = Requested.StatusWork;
                        SQL_Command.Parameters.Add("@StatusPayment", SqlDbType.NVarChar, 12); SQL_Command.Parameters["@StatusPayment"].Value = Requested.StatusPayment;
                        SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                    }

            MessageBox.Show("Данные успешно обновлены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); MasterAccount_Load(sender, e);
        }

        private void MasterAccount_Resize(object sender, EventArgs e) => UpdateOut();
    }
}