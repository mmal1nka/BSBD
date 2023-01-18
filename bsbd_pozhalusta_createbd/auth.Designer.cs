namespace bsbd_pozhalusta_createbd
{
    partial class auth
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
            this.sign_in_button = new System.Windows.Forms.Button();
            this.login_txt_box_auth = new System.Windows.Forms.TextBox();
            this.pass_txt_box_auth = new System.Windows.Forms.TextBox();
            this.login_auth = new System.Windows.Forms.Label();
            this.pass_auth = new System.Windows.Forms.Label();
            this.phone_number_auth = new System.Windows.Forms.Label();
            this.phone_txt_auth = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sign_in_button
            // 
            this.sign_in_button.Location = new System.Drawing.Point(226, 176);
            this.sign_in_button.Name = "sign_in_button";
            this.sign_in_button.Size = new System.Drawing.Size(113, 32);
            this.sign_in_button.TabIndex = 0;
            this.sign_in_button.Text = "Войти";
            this.sign_in_button.UseVisualStyleBackColor = true;
            this.sign_in_button.Click += new System.EventHandler(this.sign_in_button_Click);
            // 
            // login_txt_box_auth
            // 
            this.login_txt_box_auth.Location = new System.Drawing.Point(142, 36);
            this.login_txt_box_auth.Name = "login_txt_box_auth";
            this.login_txt_box_auth.Size = new System.Drawing.Size(197, 22);
            this.login_txt_box_auth.TabIndex = 2;
            // 
            // pass_txt_box_auth
            // 
            this.pass_txt_box_auth.Location = new System.Drawing.Point(142, 81);
            this.pass_txt_box_auth.Name = "pass_txt_box_auth";
            this.pass_txt_box_auth.PasswordChar = '*';
            this.pass_txt_box_auth.Size = new System.Drawing.Size(197, 22);
            this.pass_txt_box_auth.TabIndex = 3;
            this.pass_txt_box_auth.UseSystemPasswordChar = true;
            // 
            // login_auth
            // 
            this.login_auth.AutoSize = true;
            this.login_auth.Location = new System.Drawing.Point(12, 39);
            this.login_auth.Name = "login_auth";
            this.login_auth.Size = new System.Drawing.Size(46, 16);
            this.login_auth.TabIndex = 4;
            this.login_auth.Text = "Логин";
            // 
            // pass_auth
            // 
            this.pass_auth.AutoSize = true;
            this.pass_auth.Location = new System.Drawing.Point(12, 84);
            this.pass_auth.Name = "pass_auth";
            this.pass_auth.Size = new System.Drawing.Size(56, 16);
            this.pass_auth.TabIndex = 5;
            this.pass_auth.Text = "Пароль";
            // 
            // phone_number_auth
            // 
            this.phone_number_auth.AutoSize = true;
            this.phone_number_auth.Location = new System.Drawing.Point(12, 132);
            this.phone_number_auth.Name = "phone_number_auth";
            this.phone_number_auth.Size = new System.Drawing.Size(119, 16);
            this.phone_number_auth.TabIndex = 6;
            this.phone_number_auth.Text = "Номер телефона";
            // 
            // phone_txt_auth
            // 
            this.phone_txt_auth.Location = new System.Drawing.Point(142, 129);
            this.phone_txt_auth.Name = "phone_txt_auth";
            this.phone_txt_auth.Size = new System.Drawing.Size(197, 22);
            this.phone_txt_auth.TabIndex = 7;
            // 
            // auth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 220);
            this.Controls.Add(this.phone_txt_auth);
            this.Controls.Add(this.phone_number_auth);
            this.Controls.Add(this.pass_auth);
            this.Controls.Add(this.login_auth);
            this.Controls.Add(this.pass_txt_box_auth);
            this.Controls.Add(this.login_txt_box_auth);
            this.Controls.Add(this.sign_in_button);
            this.Name = "auth";
            this.Text = "auth";
            this.Load += new System.EventHandler(this.auth_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sign_in_button;
        private System.Windows.Forms.TextBox login_txt_box_auth;
        private System.Windows.Forms.TextBox pass_txt_box_auth;
        private System.Windows.Forms.Label login_auth;
        private System.Windows.Forms.Label pass_auth;
        private System.Windows.Forms.Label phone_number_auth;
        private System.Windows.Forms.TextBox phone_txt_auth;
    }
}