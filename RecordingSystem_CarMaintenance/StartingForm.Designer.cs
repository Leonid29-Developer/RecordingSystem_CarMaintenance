namespace RecordingSystem_CarMaintenance
{
    partial class StartingForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartingForm));
            this.Request = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Authorization = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Request
            // 
            this.Request.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Request.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Request.Location = new System.Drawing.Point(153, 49);
            this.Request.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Request.Name = "Request";
            this.Request.Size = new System.Drawing.Size(200, 50);
            this.Request.TabIndex = 0;
            this.Request.Text = "Составить заявку";
            this.Request.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Request.Click += new System.EventHandler(this.Request_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(76, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Authorization
            // 
            this.Authorization.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Authorization.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Authorization.Location = new System.Drawing.Point(153, 136);
            this.Authorization.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Authorization.Name = "Authorization";
            this.Authorization.Size = new System.Drawing.Size(200, 50);
            this.Authorization.TabIndex = 2;
            this.Authorization.Text = "Авторизоваться";
            this.Authorization.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Authorization.Click += new System.EventHandler(this.Authorization_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(76, 136);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // StartingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 238);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.Authorization);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Request);
            this.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "StartingForm";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Request;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Authorization;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

