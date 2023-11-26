namespace RecordingSystem_CarMaintenance
{
    partial class AdminAccount
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Table = new System.Windows.Forms.TableLayoutPanel();
            this.CB_Client = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_StatusWork = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Table
            // 
            this.Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Table.ColumnCount = 1;
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Table.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Table.Location = new System.Drawing.Point(0, 62);
            this.Table.Margin = new System.Windows.Forms.Padding(4, 10, 4, 4);
            this.Table.Name = "Table";
            this.Table.RowCount = 1;
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table.Size = new System.Drawing.Size(1044, 760);
            this.Table.TabIndex = 1;
            // 
            // CB_Client
            // 
            this.CB_Client.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CB_Client.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_Client.FormattingEnabled = true;
            this.CB_Client.Location = new System.Drawing.Point(231, 16);
            this.CB_Client.Name = "CB_Client";
            this.CB_Client.Size = new System.Drawing.Size(235, 31);
            this.CB_Client.TabIndex = 2;
            this.CB_Client.SelectedIndexChanged += new System.EventHandler(this.CB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(144, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Клиент";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(479, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Статус работы";
            // 
            // CB_StatusWork
            // 
            this.CB_StatusWork.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CB_StatusWork.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_StatusWork.FormattingEnabled = true;
            this.CB_StatusWork.Location = new System.Drawing.Point(629, 16);
            this.CB_StatusWork.Name = "CB_StatusWork";
            this.CB_StatusWork.Size = new System.Drawing.Size(235, 31);
            this.CB_StatusWork.TabIndex = 5;
            this.CB_StatusWork.SelectedIndexChanged += new System.EventHandler(this.CB_SelectedIndexChanged);
            // 
            // AdminAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 822);
            this.Controls.Add(this.CB_StatusWork);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CB_Client);
            this.Controls.Add(this.Table);
            this.Name = "AdminAccount";
            this.Text = "Учетная запись администратора";
            this.Load += new System.EventHandler(this.AdminAccount_Load);
            this.Resize += new System.EventHandler(this.AdminAccount_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Table;
        private System.Windows.Forms.ComboBox CB_Client;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CB_StatusWork;
    }
}