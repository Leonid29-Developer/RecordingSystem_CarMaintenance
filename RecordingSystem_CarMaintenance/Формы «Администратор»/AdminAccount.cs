using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RecordingSystem_CarMaintenance
{
    public partial class AdminAccount : Form
    {
        public AdminAccount() => InitializeComponent();

        private List<MaintenanceRequests> Requests = new List<MaintenanceRequests>();

        private List<CustomerСontacts> Masters = new List<CustomerСontacts>();

        private void AdminAccount_Load(object sender, EventArgs e)
        {
            CB_Client.Items.Clear(); CB_StatusWork.Items.Clear(); Requests.Clear(); Masters.Clear();
            CB_Client.Items.Add("Все"); CB_StatusWork.Items.Add("В обработке"); CB_StatusWork.Items.Add("Ожидание оплаты"); CB_StatusWork.Items.Add("В работе"); CB_StatusWork.Items.Add("Готово"); CB_StatusWork.Items.Add("Завершено"); CB_StatusWork.Items.Add("Отклонено");

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
                        bool T = true; foreach (string Client in CB_Client.Items) if (Client == $"{Contact_Client.LastName} {Contact_Client.FirstName[0]}. {Contact_Client.MiddleName[0]}.") T = false;
                        if (T) CB_Client.Items.Add($"{Contact_Client.LastName} {Contact_Client.FirstName[0]}. {Contact_Client.MiddleName[0]}."); SQL_Connection2.Close();
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

            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
                SQL_Connection.Open();
                string Request_SQL = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[Masters]"; // SQL-запрос
                SqlCommand SQL_Command = new SqlCommand(Request_SQL, SQL_Connection); SqlDataReader Reader = SQL_Command.ExecuteReader();
                while (Reader.Read()) Masters.Add(new CustomerСontacts((string)Reader.GetValue(1), (string)Reader.GetValue(2), (string)Reader.GetValue(3), (string)Reader.GetValue(4), (string)Reader.GetValue(5)));
                SQL_Connection.Close();
            }

            UpdateOut();
        }

        private void UpdateOut()
        {
            Table.Controls.Clear(); UpdateT = false; if (CB_StatusWork.SelectedIndex == -1) CB_StatusWork.SelectedIndex = 0; if (CB_Client.SelectedIndex == -1) CB_Client.SelectedIndex = 0; UpdateT = true;

            if (Requests.Count > 0) foreach (MaintenanceRequests Request in Requests) if (Request.StatusWork == CB_StatusWork.Items[CB_StatusWork.SelectedIndex].ToString())
                        if ((CB_Client.SelectedIndex == 0) | (CB_Client.Items[CB_Client.SelectedIndex].ToString() == $"{Request.Contact_Client.LastName} {Request.Contact_Client.FirstName[0]}. {Request.Contact_Client.MiddleName[0]}."))
                        {
                            Panel NewRequest = new Panel { Name = $"R_{Request.ID}", Size = new Size(Table.Width, 365), BorderStyle = BorderStyle.FixedSingle };
                            {
                                Panel Frame1 = new Panel { Name = "Frame1", Size = new Size(NewRequest.Width, 95), BorderStyle = BorderStyle.FixedSingle };
                                {
                                    Label Number_Date = new Label { Text = $"Заявка №{Request.ID.Remove(0, 2)} от {Request.Date.Day}.{Request.Date.Month}.{Request.Date.Year}", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width, 40), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle };

                                    Label Cost = new Label { Text = $"Стоимость работы: ", Font = new Font("Times New Roman", 16), Size = new Size(200, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 5, Top = 45 };
                                    if (Request.StatusWork != "В обработке") { Cost.Name = "Cost"; Cost.Size = new Size(360, 40); Cost.Text += $"{Request.Cost} руб"; }
                                    else
                                    {
                                        MaskedTextBox TB_Cost = new MaskedTextBox { Name = "Cost", Font = new Font("Times New Roman", 16), TextAlign = HorizontalAlignment.Center, Mask = "000 000 000 руб", Size = new Size(160, 40), Left = 205, Top = 50 };
                                        if (Request.Cost != 0) TB_Cost.Text = Request.Cost.ToString(); Frame1.Controls.Add(TB_Cost);
                                    }

                                    Label StatusWork1 = new Label { Text = $"Статус работы: ", Font = new Font("Times New Roman", 16), Size = new Size(160, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 375, Top = 45 };
                                    if (Request.StatusWork == "Завершено" | Request.StatusWork == "Отклонено")
                                    {
                                        Label StatusWork2 = new Label { Name = "StatusWork", Text = Request.StatusWork, Font = new Font("Times New Roman", 16), Size = new Size(190, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 535, Top = 45 };
                                        if (Request.StatusWork == "Завершено") StatusWork2.ForeColor = Color.Green; else StatusWork2.ForeColor = Color.Red; Frame1.Controls.Add(StatusWork2);
                                    }
                                    else
                                    {
                                        ComboBox CB_StatusWork1 = new ComboBox { Name = "StatusWork", Font = new Font("Times New Roman", 16), Size = new Size(185, 40), Left = 535, Top = 50 };
                                        foreach (string Status in CB_StatusWork.Items) CB_StatusWork1.Items.Add(Status); CB_StatusWork1.SelectedItem = Request.StatusWork; Frame1.Controls.Add(CB_StatusWork1);
                                    }

                                    Label StatusPayment1 = new Label { Text = $"Статус оплаты: ", Font = new Font("Times New Roman", 16), Size = new Size(160, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 730, Top = 45 };
                                    if (Request.StatusPayment == "Оплачено")
                                    {
                                        Label StatusPayment2 = new Label { Name = "StatusPayment", Text = Request.StatusPayment, Font = new Font("Times New Roman", 16), Size = new Size(140, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 890, Top = 45, ForeColor = Color.Green };
                                        Frame1.Controls.Add(StatusPayment2);
                                    }
                                    else
                                    {
                                        ComboBox CB_StatusPayment = new ComboBox { Name = "StatusPayment", Font = new Font("Times New Roman", 16), Size = new Size(140, 40), Left = 890, Top = 50 };
                                        CB_StatusPayment.Items.Add("Не Оплачено"); CB_StatusPayment.Items.Add("Оплачено"); CB_StatusPayment.SelectedItem = Request.StatusPayment; Frame1.Controls.Add(CB_StatusPayment);
                                    }

                                    Frame1.Controls.Add(Number_Date); Frame1.Controls.Add(Cost); Frame1.Controls.Add(StatusWork1); Frame1.Controls.Add(StatusPayment1); NewRequest.Controls.Add(Frame1);
                                }

                                Panel Frame2 = new Panel { Size = new Size(NewRequest.Width - 18, 120), BorderStyle = BorderStyle.FixedSingle, Top = Frame1.Height };
                                {
                                    Label Model = new Label { Text = $"Модель транспорта: {Request.TransportModel}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width - 23, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10, BorderStyle = BorderStyle.FixedSingle };

                                    Label ProblemDescription = new Label { Text = $"Описание проблемы: {Request.DescriptionProblem}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width - 23, 80), TextAlign = ContentAlignment.MiddleLeft, Left = 10, Top = 40, BorderStyle = BorderStyle.FixedSingle };

                                    PictureBox Pic_Delete = new PictureBox { BackgroundImage = Properties.Resources.Picture_Delete, BackgroundImageLayout = ImageLayout.Stretch, Size = new Size(40, 40), BorderStyle = BorderStyle.FixedSingle };
                                    Label Lab_Delete = new Label { Name = Request.ID, Text = "Удалить", Font = new Font("Times New Roman", 16), Size = new Size(100, 40), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle };
                                    Lab_Delete.Left = Frame2.Size.Width - Lab_Delete.Size.Width; Pic_Delete.Left = Lab_Delete.Left - Pic_Delete.Size.Width; Lab_Delete.Click += RequestDelete_Click;

                                    PictureBox Pic_Save = new PictureBox { BackgroundImage = Properties.Resources.Picture_Save, BackgroundImageLayout = ImageLayout.Stretch, Size = new Size(40, 40), BorderStyle = BorderStyle.FixedSingle };
                                    Label Lab_Save = new Label { Name = Request.ID, Text = "Сохранить", Font = new Font("Times New Roman", 16), Size = new Size(115, 40), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle };
                                    Lab_Save.Left = Pic_Delete.Left - Lab_Save.Size.Width; Pic_Save.Left = Lab_Save.Left - Pic_Save.Size.Width; Lab_Save.Click += RequestSave_Click;

                                    Frame2.Controls.Add(Model); Frame2.Controls.Add(ProblemDescription); Frame2.Controls.Add(Pic_Delete); Frame2.Controls.Add(Lab_Delete); Frame2.Controls.Add(Pic_Delete); Frame2.Controls.Add(Lab_Delete);
                                    Model.Size = new Size(Model.Size.Width - (Lab_Delete.Width + Pic_Delete.Width) * 2 - 20, Model.Size.Height); Frame2.Controls.Add(Pic_Save); Frame2.Controls.Add(Lab_Save); NewRequest.Controls.Add(Frame2);
                                }

                                Panel Frame3 = new Panel { Size = new Size(NewRequest.Width / 2, 150), BorderStyle = BorderStyle.FixedSingle, Top = Frame1.Height + Frame2.Height };
                                {
                                    Label Client = new Label { Text = $"Данные клиента", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width / 2, 40), TextAlign = ContentAlignment.MiddleCenter, Left = 10 };

                                    Label FIO = new Label { Text = $"ФИО: {Request.Contact_Client.LastName} {Request.Contact_Client.FirstName} {Request.Contact_Client.MiddleName}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 2 - 60, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 40 };

                                    Label Telephone1 = new Label { Text = $"Номер телефона: ", Font = new Font("Times New Roman", 16), Size = new Size(180, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 70 };
                                    Label Telephone2 = new Label { Text = $"{Request.Contact_Client.Telephone}", Font = new Font("Times New Roman", 16), Size = new Size(282, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30 + Telephone1.Width, Top = 70, ForeColor = Color.Blue };

                                    Label Email1 = new Label { Text = $"E-mail: ", Font = new Font("Times New Roman", 16), Size = new Size(80, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 100 };
                                    Label Email2 = new Label { Text = $"{Request.Contact_Client.Email}", Font = new Font("Times New Roman", 16), Size = new Size(382, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30 + Email1.Width, Top = 100, ForeColor = Color.Blue };

                                    Frame3.Controls.Add(Client); Frame3.Controls.Add(FIO); Frame3.Controls.Add(Telephone1); Frame3.Controls.Add(Telephone2); Frame3.Controls.Add(Email1); Frame3.Controls.Add(Email2); NewRequest.Controls.Add(Frame3);
                                }

                                Panel Frame4 = new Panel { Size = new Size(NewRequest.Width / 2, 150), BorderStyle = BorderStyle.FixedSingle, Top = Frame1.Height + Frame2.Height, Left = NewRequest.Width / 2 };
                                {
                                    Label Client = new Label { Text = $"Данные мастера", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width / 2, 40), TextAlign = ContentAlignment.MiddleCenter, Left = 10 };

                                    if (Request.Contact_Master.LastName != "NULL")
                                    {
                                        Label FIO = new Label { Text = $"ФИО: {Request.Contact_Master.LastName} {Request.Contact_Master.FirstName} {Request.Contact_Master.MiddleName}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 2 - 60, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 40 };

                                        Label Telephone1 = new Label { Text = $"Номер телефона: ", Font = new Font("Times New Roman", 16), Size = new Size(180, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 70 };
                                        Label Telephone2 = new Label { Text = $"{Request.Contact_Master.Telephone}", Font = new Font("Times New Roman", 16), Size = new Size(282, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30 + Telephone1.Width, Top = 70, ForeColor = Color.Blue, };

                                        Label Email1 = new Label { Text = $"E-mail: ", Font = new Font("Times New Roman", 16), Size = new Size(80, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 100 };
                                        Label Email2 = new Label { Text = $"{Request.Contact_Master.Email}", Font = new Font("Times New Roman", 16), Size = new Size(382, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30 + Email1.Width, Top = 100, ForeColor = Color.Blue };

                                        Frame4.Controls.Add(FIO); Frame4.Controls.Add(Telephone1); Frame4.Controls.Add(Telephone2); Frame4.Controls.Add(Email1); Frame4.Controls.Add(Email2);
                                    }
                                    else
                                    {
                                        ComboBox CB_Masters = new ComboBox { Name = $"{Request.ID}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 2 - 120, 50), Left = 60, Top = 45 };
                                        CB_Masters.Text = "Не Назначен"; foreach (CustomerСontacts Master in Masters) CB_Masters.Items.Add($"{Master.LastName} {Master.FirstName} {Master.MiddleName}");

                                        Label MasterSet = new Label { Text = "Назначить мастера", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 2 - 160, 46), TextAlign = ContentAlignment.MiddleCenter, Left = 80, Top = 88, BorderStyle = BorderStyle.FixedSingle };

                                        Frame4.Controls.Add(CB_Masters); MasterSet.Click += MasterSet_Click; Frame4.Controls.Add(MasterSet); CB_Mast = CB_Masters;
                                    }

                                    Frame4.Controls.Add(Client); NewRequest.Controls.Add(Frame4);
                                }

                                Table.Controls.Add(NewRequest);
                            }
                        }

            Table.AutoScroll = true;
        }

        private void RequestSave_Click(object sender, EventArgs e)
        {
            string[] Named = new string[4]; Label Click = (Label)sender;

            foreach (Panel Control in Table.Controls) if (Control.Name[0] == 'R' & Control.Name.Remove(0, 2) == Click.Name)
                {
                    Named[0] = Control.Name.Remove(0, 2); foreach (Panel Frame in Control.Controls) if (Frame.Name == "Frame1") foreach (var Element in Frame.Controls)
                            {
                                switch (Element.GetType().ToString())
                                {
                                    case "System.Windows.Forms.Label":
                                        {
                                            Label Element_Label = (Label)Element;

                                            if (Element_Label.Name == "Cost") { Named[1] = ""; string[] G = Element_Label.Text.Split(' '); for (int i = 2; i < G.Length - 1; i++) Named[1] += G[i]; }
                                            if (Element_Label.Name == "StatusWork") Named[2] = Element_Label.Text; if (Element_Label.Name == "StatusPayment") Named[3] = Element_Label.Text;
                                        }
                                        break;

                                    case "System.Windows.Forms.MaskedTextBox":
                                        {
                                            MaskedTextBox Element_MaskedTextBox = (MaskedTextBox)Element;

                                            if (Element_MaskedTextBox.Name == "Cost") { Named[1] = ""; string[] G = Element_MaskedTextBox.Text.Split(' '); for (int i = 0; i < G.Length - 1; i++) Named[1] += G[i]; }
                                            if (Element_MaskedTextBox.Name == "StatusWork") Named[2] = Element_MaskedTextBox.Text; if (Element_MaskedTextBox.Name == "StatusPayment") Named[3] = Element_MaskedTextBox.Text;
                                        }
                                        break;

                                    case "System.Windows.Forms.ComboBox":
                                        {
                                            ComboBox Element_ComboBox = (ComboBox)Element;

                                            if (Element_ComboBox.Name == "Cost") { Named[1] = ""; string[] G = Element_ComboBox.Text.Split(' '); for (int i = 0; i < G.Length - 1; i++) Named[1] += G[i]; }
                                            if (Element_ComboBox.Name == "StatusWork") Named[2] = Element_ComboBox.Text; if (Element_ComboBox.Name == "StatusPayment") Named[3] = Element_ComboBox.Text;
                                        }
                                        break;
                                }
                            }
                }

            int Cost = 0; if (Named[1] != "") Cost = Convert.ToInt32(Named[1]);

            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
                SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[MaintenanceRequests_Update] @RequestID, @Cost, @StatusWork, @StatusPayment"; // SQL-запрос
                SQL_Command.Parameters.Add("@RequestID", SqlDbType.VarChar, 8); SQL_Command.Parameters["@RequestID"].Value = Named[0];
                SQL_Command.Parameters.Add("@Cost", SqlDbType.Int); SQL_Command.Parameters["@Cost"].Value = Cost;
                SQL_Command.Parameters.Add("@StatusWork", SqlDbType.NVarChar, 16); SQL_Command.Parameters["@StatusWork"].Value = Named[2];
                SQL_Command.Parameters.Add("@StatusPayment", SqlDbType.NVarChar, 12); SQL_Command.Parameters["@StatusPayment"].Value = Named[3];
                SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
            }

            MessageBox.Show("Данные успешно обновлены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); AdminAccount_Load(sender, e);
        }

        private void RequestDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Уверены, что хотите удалить заявку?", "Подтверждение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string Named = ""; Label Click = (Label)sender;

                foreach (Panel Control in Table.Controls) if (Control.Name[0] == 'R' & Control.Name.Remove(0, 2) == Click.Name) Named = Control.Name.Remove(0, 2);

                using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
                {
                    SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                    string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[MaintenanceRequests_Delete] @RequestID"; // SQL-запрос
                    SQL_Command.Parameters.Add("@RequestID", SqlDbType.VarChar, 8); SQL_Command.Parameters["@RequestID"].Value = Named;
                    SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                }

                MessageBox.Show("Данные успешно удалены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); AdminAccount_Load(sender, e);
            }
        }

        private ComboBox CB_Mast;

        private void MasterSet_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Masters[CB_Mast.SelectedIndex].ID);

            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
                SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                string Request = $"EXEC [RecordingSystem_CarMaintenance].[dbo].[Master Set] @RequestID, @DesignatedMaster"; // SQL-запрос
                SQL_Command.Parameters.Add("@RequestID", SqlDbType.VarChar, 8); SQL_Command.Parameters["@RequestID"].Value = CB_Mast.Name;
                SQL_Command.Parameters.Add("@DesignatedMaster", SqlDbType.VarChar, 7); SQL_Command.Parameters["@DesignatedMaster"].Value = Masters[CB_Mast.SelectedIndex].ID;
                SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
            }

            MessageBox.Show("Мастер успешно назначен", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); AdminAccount_Load(sender, e);
        }

        private bool UpdateT = false;

        private void CB_SelectedIndexChanged(object sender, EventArgs e) { if (UpdateT) UpdateOut(); }

        private void AdminAccount_Resize(object sender, EventArgs e) => UpdateOut();
    }
}