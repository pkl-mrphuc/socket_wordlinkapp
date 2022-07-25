
namespace WordLinkApp
{
    partial class frmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inpUsername = new System.Windows.Forms.TextBox();
            this.inpPassword = new System.Windows.Forms.TextBox();
            this.labUsername = new System.Windows.Forms.Label();
            this.labPassword = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inpUsername
            // 
            this.inpUsername.Location = new System.Drawing.Point(145, 9);
            this.inpUsername.Name = "inpUsername";
            this.inpUsername.Size = new System.Drawing.Size(215, 27);
            this.inpUsername.TabIndex = 0;
            // 
            // inpPassword
            // 
            this.inpPassword.Location = new System.Drawing.Point(145, 66);
            this.inpPassword.Name = "inpPassword";
            this.inpPassword.Size = new System.Drawing.Size(215, 27);
            this.inpPassword.TabIndex = 1;
            this.inpPassword.UseSystemPasswordChar = true;
            // 
            // labUsername
            // 
            this.labUsername.AutoSize = true;
            this.labUsername.Location = new System.Drawing.Point(12, 9);
            this.labUsername.Name = "labUsername";
            this.labUsername.Size = new System.Drawing.Size(107, 20);
            this.labUsername.TabIndex = 2;
            this.labUsername.Text = "Tên đăng nhập";
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Location = new System.Drawing.Point(12, 73);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(70, 20);
            this.labPassword.TabIndex = 3;
            this.labPassword.Text = "Mật khẩu";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(145, 119);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(94, 29);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 167);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.labUsername);
            this.Controls.Add(this.inpPassword);
            this.Controls.Add(this.inpUsername);
            this.Name = "frmLogin";
            this.Text = "WordAppLink";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inpUsername;
        private System.Windows.Forms.Label labUsername;
        private System.Windows.Forms.Label labPassword;
        private System.Windows.Forms.TextBox inpPassword;
        private System.Windows.Forms.Button btnLogin;
    }
}

