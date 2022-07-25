
namespace ToolInsertSourceDictionaryToDB
{
    partial class Form1
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
            this.btnRun = new System.Windows.Forms.Button();
            this.lbInfo = new System.Windows.Forms.Label();
            this.lbCounter = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(12, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(94, 29);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Bắt đầu";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Location = new System.Drawing.Point(12, 44);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(185, 20);
            this.lbInfo.TabIndex = 1;
            this.lbInfo.Text = "Số lượng bản ghi đã thêm:";
            // 
            // lbCounter
            // 
            this.lbCounter.AutoSize = true;
            this.lbCounter.Location = new System.Drawing.Point(211, 44);
            this.lbCounter.Name = "lbCounter";
            this.lbCounter.Size = new System.Drawing.Size(17, 20);
            this.lbCounter.TabIndex = 2;
            this.lbCounter.Text = "0";
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 85);
            this.Controls.Add(this.lbCounter);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.btnRun);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCounter;
        private System.Windows.Forms.Label lbInfo;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}

