
namespace WordLinkApp
{
    partial class frmRoomGame
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
            this.lstUser = new System.Windows.Forms.ListView();
            this.user = new System.Windows.Forms.ColumnHeader();
            this.lstAnswer = new System.Windows.Forms.ListView();
            this.sender = new System.Windows.Forms.ColumnHeader();
            this.answer = new System.Windows.Forms.ColumnHeader();
            this.inpAnswer = new System.Windows.Forms.TextBox();
            this.btnSendAnswer = new System.Windows.Forms.Button();
            this.lbTimeCounter = new System.Windows.Forms.Label();
            this.btnOutGame = new System.Windows.Forms.Button();
            this.btnStarting = new System.Windows.Forms.Button();
            this.lbNameRoom = new System.Windows.Forms.Label();
            this.lbBeforeAnswer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstUser
            // 
            this.lstUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.user});
            this.lstUser.Enabled = false;
            this.lstUser.HideSelection = false;
            this.lstUser.Location = new System.Drawing.Point(384, 46);
            this.lstUser.Name = "lstUser";
            this.lstUser.Size = new System.Drawing.Size(170, 213);
            this.lstUser.TabIndex = 0;
            this.lstUser.UseCompatibleStateImageBehavior = false;
            this.lstUser.View = System.Windows.Forms.View.List;
            // 
            // user
            // 
            this.user.Text = "Thành viên";
            this.user.Width = 150;
            // 
            // lstAnswer
            // 
            this.lstAnswer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sender,
            this.answer});
            this.lstAnswer.HideSelection = false;
            this.lstAnswer.Location = new System.Drawing.Point(13, 46);
            this.lstAnswer.Name = "lstAnswer";
            this.lstAnswer.Size = new System.Drawing.Size(365, 213);
            this.lstAnswer.TabIndex = 1;
            this.lstAnswer.UseCompatibleStateImageBehavior = false;
            this.lstAnswer.View = System.Windows.Forms.View.List;
            // 
            // sender
            // 
            this.sender.Text = "Người gửi";
            this.sender.Width = 100;
            // 
            // answer
            // 
            this.answer.Text = "Kết quả";
            this.answer.Width = 200;
            // 
            // inpAnswer
            // 
            this.inpAnswer.Location = new System.Drawing.Point(131, 13);
            this.inpAnswer.Name = "inpAnswer";
            this.inpAnswer.Size = new System.Drawing.Size(247, 27);
            this.inpAnswer.TabIndex = 2;
            // 
            // btnSendAnswer
            // 
            this.btnSendAnswer.Enabled = false;
            this.btnSendAnswer.Location = new System.Drawing.Point(384, 11);
            this.btnSendAnswer.Name = "btnSendAnswer";
            this.btnSendAnswer.Size = new System.Drawing.Size(94, 29);
            this.btnSendAnswer.TabIndex = 3;
            this.btnSendAnswer.Text = "Gửi";
            this.btnSendAnswer.UseVisualStyleBackColor = true;
            this.btnSendAnswer.Click += new System.EventHandler(this.btnSendAnswer_Click);
            // 
            // lbTimeCounter
            // 
            this.lbTimeCounter.Location = new System.Drawing.Point(483, 16);
            this.lbTimeCounter.Name = "lbTimeCounter";
            this.lbTimeCounter.Size = new System.Drawing.Size(71, 20);
            this.lbTimeCounter.TabIndex = 4;
            this.lbTimeCounter.Text = "Thời gian";
            this.lbTimeCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnOutGame
            // 
            this.btnOutGame.Location = new System.Drawing.Point(113, 266);
            this.btnOutGame.Name = "btnOutGame";
            this.btnOutGame.Size = new System.Drawing.Size(88, 29);
            this.btnOutGame.TabIndex = 5;
            this.btnOutGame.Text = "Thoát";
            this.btnOutGame.UseVisualStyleBackColor = true;
            this.btnOutGame.Click += new System.EventHandler(this.btnOutGame_Click);
            // 
            // btnStarting
            // 
            this.btnStarting.Location = new System.Drawing.Point(12, 266);
            this.btnStarting.Name = "btnStarting";
            this.btnStarting.Size = new System.Drawing.Size(94, 29);
            this.btnStarting.TabIndex = 6;
            this.btnStarting.Text = "Bắt đầu";
            this.btnStarting.UseVisualStyleBackColor = true;
            this.btnStarting.Click += new System.EventHandler(this.btnStarting_Click);
            // 
            // lbNameRoom
            // 
            this.lbNameRoom.AutoSize = true;
            this.lbNameRoom.Location = new System.Drawing.Point(13, 15);
            this.lbNameRoom.Name = "lbNameRoom";
            this.lbNameRoom.Size = new System.Drawing.Size(63, 20);
            this.lbNameRoom.TabIndex = 7;
            this.lbNameRoom.Text = "Phòng 1";
            // 
            // lbBeforeAnswer
            // 
            this.lbBeforeAnswer.AutoSize = true;
            this.lbBeforeAnswer.Location = new System.Drawing.Point(426, 269);
            this.lbBeforeAnswer.Name = "lbBeforeAnswer";
            this.lbBeforeAnswer.Size = new System.Drawing.Size(0, 20);
            this.lbBeforeAnswer.TabIndex = 8;
            this.lbBeforeAnswer.Visible = false;
            // 
            // frmRoomGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 310);
            this.Controls.Add(this.lbBeforeAnswer);
            this.Controls.Add(this.lbNameRoom);
            this.Controls.Add(this.btnStarting);
            this.Controls.Add(this.btnOutGame);
            this.Controls.Add(this.lbTimeCounter);
            this.Controls.Add(this.btnSendAnswer);
            this.Controls.Add(this.inpAnswer);
            this.Controls.Add(this.lstAnswer);
            this.Controls.Add(this.lstUser);
            this.Name = "frmRoomGame";
            this.Text = "Word link app";
            this.Load += new System.EventHandler(this.frmRoomGame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstUser;
        private System.Windows.Forms.ListView lstAnswer;
        private System.Windows.Forms.TextBox inpAnswer;
        private System.Windows.Forms.Button btnSendAnswer;
        private System.Windows.Forms.Label lbTimeCounter;
        private System.Windows.Forms.Button btnOutGame;
        private System.Windows.Forms.Button btnStarting;
        private System.Windows.Forms.Label lbNameRoom;
        private System.Windows.Forms.ColumnHeader user;
        private System.Windows.Forms.ColumnHeader sender;
        private System.Windows.Forms.ColumnHeader answer;
        private System.Windows.Forms.Label lbBeforeAnswer;
    }
}