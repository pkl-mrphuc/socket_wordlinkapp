
namespace WordLinkApp.MoniterServer
{
    partial class frmMonitor
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
            this.lstSocket = new System.Windows.Forms.ListView();
            this.lstMessage = new System.Windows.Forms.ListView();
            this.Sender = new System.Windows.Forms.ColumnHeader();
            this.Message = new System.Windows.Forms.ColumnHeader();
            this.inpHost = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.inpPort = new System.Windows.Forms.TextBox();
            this.lbHost = new System.Windows.Forms.Label();
            this.lbPort = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstSocket
            // 
            this.lstSocket.HideSelection = false;
            this.lstSocket.Location = new System.Drawing.Point(742, 12);
            this.lstSocket.Name = "lstSocket";
            this.lstSocket.Size = new System.Drawing.Size(229, 496);
            this.lstSocket.TabIndex = 0;
            this.lstSocket.UseCompatibleStateImageBehavior = false;
            this.lstSocket.View = System.Windows.Forms.View.List;
            // 
            // lstMessage
            // 
            this.lstMessage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Sender,
            this.Message});
            this.lstMessage.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstMessage.HideSelection = false;
            this.lstMessage.Location = new System.Drawing.Point(13, 45);
            this.lstMessage.Name = "lstMessage";
            this.lstMessage.Size = new System.Drawing.Size(723, 463);
            this.lstMessage.TabIndex = 1;
            this.lstMessage.UseCompatibleStateImageBehavior = false;
            this.lstMessage.View = System.Windows.Forms.View.List;
            // 
            // Sender
            // 
            this.Sender.Text = "Sender";
            this.Sender.Width = 200;
            // 
            // Message
            // 
            this.Message.Text = "Message";
            this.Message.Width = 500;
            // 
            // inpHost
            // 
            this.inpHost.Enabled = false;
            this.inpHost.Location = new System.Drawing.Point(81, 13);
            this.inpHost.Name = "inpHost";
            this.inpHost.Size = new System.Drawing.Size(210, 27);
            this.inpHost.TabIndex = 2;
            this.inpHost.Text = "ws://127.0.0.1";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(542, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(94, 29);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(642, 11);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(94, 29);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // inpPort
            // 
            this.inpPort.Enabled = false;
            this.inpPort.Location = new System.Drawing.Point(353, 13);
            this.inpPort.Name = "inpPort";
            this.inpPort.Size = new System.Drawing.Size(125, 27);
            this.inpPort.TabIndex = 5;
            this.inpPort.Text = "1011";
            // 
            // lbHost
            // 
            this.lbHost.AutoSize = true;
            this.lbHost.Location = new System.Drawing.Point(13, 16);
            this.lbHost.Name = "lbHost";
            this.lbHost.Size = new System.Drawing.Size(62, 20);
            this.lbHost.TabIndex = 6;
            this.lbHost.Text = "Domain";
            // 
            // lbPort
            // 
            this.lbPort.AutoSize = true;
            this.lbPort.Location = new System.Drawing.Point(297, 16);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(35, 20);
            this.lbPort.TabIndex = 7;
            this.lbPort.Text = "Port";
            // 
            // frmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 520);
            this.Controls.Add(this.lbPort);
            this.Controls.Add(this.lbHost);
            this.Controls.Add(this.inpPort);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.inpHost);
            this.Controls.Add(this.lstMessage);
            this.Controls.Add(this.lstSocket);
            this.Name = "frmMonitor";
            this.Text = "Monitor Socket Server";
            this.Load += new System.EventHandler(this.frmMonitor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstSocket;
        private System.Windows.Forms.ListView lstMessage;
        private System.Windows.Forms.TextBox inpHost;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox inpPort;
        private System.Windows.Forms.Label lbHost;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ColumnHeader Sender;
        private System.Windows.Forms.ColumnHeader Message;
    }
}

