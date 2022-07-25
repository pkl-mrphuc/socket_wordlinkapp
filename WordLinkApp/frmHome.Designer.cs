
namespace WordLinkApp
{
    partial class frmHome
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
            this.lbDisplayName = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.LinkLabel();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.lstRoom = new System.Windows.Forms.ListView();
            this.room = new System.Windows.Forms.ColumnHeader();
            this.btnJoinRoom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbDisplayName
            // 
            this.lbDisplayName.AutoSize = true;
            this.lbDisplayName.Location = new System.Drawing.Point(12, 9);
            this.lbDisplayName.Name = "lbDisplayName";
            this.lbDisplayName.Size = new System.Drawing.Size(95, 20);
            this.lbDisplayName.TabIndex = 0;
            this.lbDisplayName.Text = "Lê Khắc Phúc";
            this.lbDisplayName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLogout
            // 
            this.btnLogout.AutoSize = true;
            this.btnLogout.Location = new System.Drawing.Point(397, 9);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(77, 20);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.TabStop = true;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnLogout_LinkClicked);
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.Location = new System.Drawing.Point(280, 159);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(94, 29);
            this.btnCreateRoom.TabIndex = 2;
            this.btnCreateRoom.Text = "Tạo phòng";
            this.btnCreateRoom.UseVisualStyleBackColor = true;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            // 
            // lstRoom
            // 
            this.lstRoom.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.room});
            this.lstRoom.FullRowSelect = true;
            this.lstRoom.GridLines = true;
            this.lstRoom.HideSelection = false;
            this.lstRoom.Location = new System.Drawing.Point(12, 32);
            this.lstRoom.Name = "lstRoom";
            this.lstRoom.Size = new System.Drawing.Size(462, 121);
            this.lstRoom.TabIndex = 3;
            this.lstRoom.UseCompatibleStateImageBehavior = false;
            this.lstRoom.View = System.Windows.Forms.View.List;
            // 
            // room
            // 
            this.room.Text = "Phòng game";
            this.room.Width = 200;
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.Location = new System.Drawing.Point(380, 159);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new System.Drawing.Size(94, 29);
            this.btnJoinRoom.TabIndex = 4;
            this.btnJoinRoom.Text = "Tham gia";
            this.btnJoinRoom.UseVisualStyleBackColor = true;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 201);
            this.Controls.Add(this.btnJoinRoom);
            this.Controls.Add(this.lstRoom);
            this.Controls.Add(this.btnCreateRoom);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lbDisplayName);
            this.Name = "frmHome";
            this.Text = "Word link app";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHome_FormClosing);
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDisplayName;
        private System.Windows.Forms.LinkLabel btnLogout;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.ListView lstRoom;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.ColumnHeader room;
    }
}