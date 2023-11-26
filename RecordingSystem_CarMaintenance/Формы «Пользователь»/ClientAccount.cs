using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RecordingSystem_CarMaintenance
{
    public partial class ClientAccount : Form
    {
        public ClientAccount() => InitializeComponent();

        public static string CustomerСontact { get; set; }

        private List<MaintenanceRequests> Requests = new List<MaintenanceRequests>();

        private void ClientAccount_Load(object sender, EventArgs e)
        {
            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
                SQL_Connection.Open(); string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[MaintenanceRequests_Contact] @ID"; // SQL-запрос
                SqlCommand SQL_Command = new SqlCommand(Request, SQL_Connection);
                SQL_Command.Parameters.Add("@ID", SqlDbType.VarChar, 7); SQL_Command.Parameters["@ID"].Value = CustomerСontact; SqlDataReader Reader = SQL_Command.ExecuteReader();
                while (Reader.Read())
                {
                    int COST = 0; if (Reader.GetValue(3) != DBNull.Value) COST = (int)Reader.GetValue(3); CustomerСontacts Сontact = null;

                    using (SqlConnection SQL_Connection2 = new SqlConnection(FormRequest.ConnectString))
                    {
                        SQL_Connection2.Open();
                        Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[CustomerСontact_ID] @ID"; // SQL-запрос
                        SqlCommand SQL_Command2 = new SqlCommand(Request, SQL_Connection2);
                        SQL_Command2.Parameters.Add("@ID", SqlDbType.VarChar, 7); SQL_Command2.Parameters["@ID"].Value = (string)Reader.GetValue(6); SqlDataReader Reader2 = SQL_Command2.ExecuteReader();
                        while (Reader2.Read()) Сontact = new CustomerСontacts((string)Reader2.GetValue(0), (string)Reader2.GetValue(1), (string)Reader2.GetValue(2), (string)Reader2.GetValue(3), (string)Reader2.GetValue(4));
                        SQL_Connection2.Close();
                    }

                    Requests.Add(new MaintenanceRequests((string)Reader.GetValue(0), (string)Reader.GetValue(1), (string)Reader.GetValue(2), COST, (string)Reader.GetValue(4), (string)Reader.GetValue(5), null, Сontact, (DateTime)Reader.GetValue(7)));
                }
                SQL_Connection.Close();
            }

            Panel Head_CR = new Panel { Size = new Size(Table.Width - 40, 80) };
            {
                Label CurrentRequests = new Label { Font = new Font("Times New Roman", 22), Size = new Size(Head_CR.Width - 150, Head_CR.Height), Text = "       Текущие заявки", TextAlign = ContentAlignment.MiddleLeft, Left = 50, BorderStyle = BorderStyle.FixedSingle };
                Label Arrow = new Label { Font = new Font("Times New Roman", 22), Size = new Size(80, Head_CR.Height), Text = "▼", TextAlign = ContentAlignment.MiddleCenter, Left = Head_CR.Width - 99, BorderStyle = BorderStyle.FixedSingle };

                Head_CR.Controls.Add(CurrentRequests); Head_CR.Controls.Add(Arrow); Table.Controls.Add(Head_CR);
            }

            if (Requests.Count > 0) foreach (MaintenanceRequests Request in Requests) if (Request.StatusWork != "Завершено" & Request.StatusWork != "Отклонено")
                    {
                        Panel NewRequest = new Panel { Size = new Size(Table.Width - 40, 160), BorderStyle = BorderStyle.FixedSingle };
                        {
                            Panel Frame1 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60, BorderStyle = BorderStyle.FixedSingle };
                            {
                                Label Number_Date = new Label { Text = $"Заявка №{Request.ID.Remove(0, 2)} от {Request.Date.Day}.{Request.Date.Month}.{Request.Date.Year}", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width / 3, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 5 };

                                Label Cost = new Label { Text = $"Стоимость работы: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width * 2 / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 40 };
                                if (Request.Cost == 0) Cost.Text += $"Не назначено"; else Cost.Text += $"{Request.Cost} руб.";

                                Label StatusWork1 = new Label { Text = $"Статус работы: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 80 };
                                Label StatusWork2 = new Label { Text = $"{Request.StatusWork}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20 + NewRequest.Width / 7, Top = 80 };
                                if (Request.StatusWork == "Готово") StatusWork2.ForeColor = Color.Green; else StatusWork2.ForeColor = Color.Orange;

                                Label StatusPayment1 = new Label { Text = $"Статус оплаты: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 120 };
                                Label StatusPayment2 = new Label { Text = $"{Request.StatusPayment}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20 + NewRequest.Width / 7, Top = 120, ForeColor = Color.Green };

                                Frame1.Controls.Add(Number_Date); Frame1.Controls.Add(StatusWork1); Frame1.Controls.Add(StatusWork2); Frame1.Controls.Add(Cost); Frame1.Controls.Add(StatusPayment1); Frame1.Controls.Add(StatusPayment2); NewRequest.Controls.Add(Frame1);
                            }

                            Panel Frame2 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60 + NewRequest.Width / 3, BorderStyle = BorderStyle.FixedSingle };
                            {
                                Label Model = new Label { Text = $"Модель транспорта: {Request.TransportModel}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 3, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10 };

                                Label Problem = new Label { Text = $"Описание проблемы:", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 3, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10, Top = 40 };
                                Label Description = new Label { Text = $"{Request.DescriptionProblem}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 3 - 10, 80), TextAlign = ContentAlignment.TopLeft, Left = 10, Top = 80 };

                                Frame2.Controls.Add(Model); Frame2.Controls.Add(Problem); Frame2.Controls.Add(Description); NewRequest.Controls.Add(Frame2);
                            }

                            Panel Frame3 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60 + NewRequest.Width * 2 / 3, BorderStyle = BorderStyle.FixedSingle };
                            {
                                Label Master1 = new Label { Text = $"Мастер: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 12, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10, Top = 50 };
                                Label Master2 = new Label { Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width * 3 / 16, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10 + Master1.Width, Top = 50 };
                                if (Request.Contact_Master == null) Master2.Text = $"Не назначен"; else Master2.Text = $"{Request.Contact_Master.LastName} {Request.Contact_Master.FirstName[0]}. {Request.Contact_Master.MiddleName[0]}.";

                                Frame3.Controls.Add(Master1); Frame3.Controls.Add(Master2); NewRequest.Controls.Add(Frame3);
                            }

                            Table.Controls.Add(NewRequest);
                        }
                    }

            Panel Head_HR = new Panel { Size = new Size(Table.Width - 40, 80) };
            {
                Label HistoryOfRequests = new Label { Font = new Font("Times New Roman", 22), Size = new Size(Head_CR.Width - 150, Head_CR.Height), Text = "       История заявок", TextAlign = ContentAlignment.MiddleLeft, Left = 50, BorderStyle = BorderStyle.FixedSingle };
                Label Arrow = new Label { Font = new Font("Times New Roman", 22), Size = new Size(80, Head_CR.Height), Text = "▼", TextAlign = ContentAlignment.MiddleCenter, Left = Head_CR.Width - 99, BorderStyle = BorderStyle.FixedSingle };

                Head_HR.Controls.Add(HistoryOfRequests); Head_HR.Controls.Add(Arrow); Table.Controls.Add(Head_HR);
            }

            if (Requests.Count > 0) foreach (MaintenanceRequests Request in Requests) if (Request.StatusWork == "Завершено" | Request.StatusWork == "Отклонено")
                    {
                        Panel NewRequest = new Panel { Size = new Size(Table.Width - 40, 160), BorderStyle = BorderStyle.FixedSingle };
                        {
                            Panel Frame1 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60, BorderStyle = BorderStyle.FixedSingle };
                            {
                                Label Number_Date = new Label { Text = $"Заявка №{Request.ID.Remove(0, 2)} от {Request.Date.Day}.{Request.Date.Month}.{Request.Date.Year}", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width / 3, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 5 };

                                Label Cost = new Label { Text = $"Стоимость работы: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width * 2 / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 40 };
                                if (Request.Cost == 0) Cost.Text += $"Не назначено"; else Cost.Text += $"{Request.Cost} руб.";

                                Label StatusWork1 = new Label { Text = $"Статус работы: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 80 };
                                Label StatusWork2 = new Label { Text = $"{Request.StatusWork}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20 + NewRequest.Width / 7, Top = 80 };
                                if (Request.StatusWork == "Отклонено") StatusWork2.ForeColor = Color.Red; else StatusWork2.ForeColor = Color.Green;

                                Label StatusPayment1 = new Label { Text = $"Статус оплаты: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 120 };
                                Label StatusPayment2 = new Label { Text = $"{Request.StatusPayment}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20 + NewRequest.Width / 7, Top = 120, ForeColor = Color.Green };

                                Frame1.Controls.Add(Number_Date); Frame1.Controls.Add(StatusWork1); Frame1.Controls.Add(StatusWork2); Frame1.Controls.Add(Cost); Frame1.Controls.Add(StatusPayment1); Frame1.Controls.Add(StatusPayment2); NewRequest.Controls.Add(Frame1);
                            }

                            Panel Frame2 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60 + NewRequest.Width / 3, BorderStyle = BorderStyle.FixedSingle };
                            {
                                Label Model = new Label { Text = $"Модель транспорта: {Request.TransportModel}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 3, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10 };

                                Label Problem = new Label { Text = $"Описание проблемы:", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 3, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10, Top = 40 };
                                Label Description = new Label { Text = $"{Request.DescriptionProblem}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 3 - 10, 80), TextAlign = ContentAlignment.TopLeft, Left = 10, Top = 80 };

                                Frame2.Controls.Add(Model); Frame2.Controls.Add(Problem); Frame2.Controls.Add(Description); NewRequest.Controls.Add(Frame2);
                            }

                            Panel Frame3 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60 + NewRequest.Width * 2 / 3, BorderStyle = BorderStyle.FixedSingle };
                            {
                                Label Master1 = new Label { Text = $"Мастер: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 12, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10, Top = 50 };
                                Label Master2 = new Label { Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width * 3 / 16, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10 + Master1.Width, Top = 50 };
                                if (Request.Contact_Master == null) Master2.Text = $"Не назначен"; else Master2.Text = $"{Request.Contact_Master.LastName} {Request.Contact_Master.FirstName[0]}. {Request.Contact_Master.MiddleName[0]}.";

                                Frame3.Controls.Add(Master1); Frame3.Controls.Add(Master2); NewRequest.Controls.Add(Frame3);
                            }

                            Table.Controls.Add(NewRequest);
                        }
                    }

            Table.AutoScroll = true;
        }

        private void Label_Request_Click(object sender, EventArgs e)
        { FormRequest.CustomerСontact = CustomerСontact; FormRequest.Request_Authorized = true; Hide(); new FormRequest().ShowDialog(); Show(); ClientAccount_Load(sender, e); }
    }
}