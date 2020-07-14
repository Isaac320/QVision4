namespace QVision.Frm
{
    partial class EndTrayFrm
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
            this.lb_reject = new System.Windows.Forms.Label();
            this.lb_InFrame = new System.Windows.Forms.Label();
            this.lb_lot = new System.Windows.Forms.Label();
            this.lb_noProcess = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_reject
            // 
            this.lb_reject.AutoSize = true;
            this.lb_reject.Location = new System.Drawing.Point(42, 70);
            this.lb_reject.Name = "lb_reject";
            this.lb_reject.Size = new System.Drawing.Size(143, 12);
            this.lb_reject.TabIndex = 6;
            this.lb_reject.Text = "Found reject units: 199";
            // 
            // lb_InFrame
            // 
            this.lb_InFrame.AutoSize = true;
            this.lb_InFrame.Location = new System.Drawing.Point(42, 47);
            this.lb_InFrame.Name = "lb_InFrame";
            this.lb_InFrame.Size = new System.Drawing.Size(143, 12);
            this.lb_InFrame.TabIndex = 5;
            this.lb_InFrame.Text = "Inspected Frames： 1/16";
            // 
            // lb_lot
            // 
            this.lb_lot.AutoSize = true;
            this.lb_lot.Location = new System.Drawing.Point(42, 24);
            this.lb_lot.Name = "lb_lot";
            this.lb_lot.Size = new System.Drawing.Size(59, 12);
            this.lb_lot.TabIndex = 4;
            this.lb_lot.Text = "Lot : 123";
            // 
            // lb_noProcess
            // 
            this.lb_noProcess.AutoSize = true;
            this.lb_noProcess.ForeColor = System.Drawing.Color.Red;
            this.lb_noProcess.Location = new System.Drawing.Point(44, 478);
            this.lb_noProcess.Name = "lb_noProcess";
            this.lb_noProcess.Size = new System.Drawing.Size(149, 12);
            this.lb_noProcess.TabIndex = 10;
            this.lb_noProcess.Text = "托盘未处理，请处理完继续";
            this.lb_noProcess.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 420);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(275, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Please move to the tray to the reject station";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(44, 96);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(284, 304);
            this.listBox1.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(253, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 64);
            this.button1.TabIndex = 7;
            this.button1.Text = "继续";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EndTrayFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 542);
            this.Controls.Add(this.lb_noProcess);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lb_reject);
            this.Controls.Add(this.lb_InFrame);
            this.Controls.Add(this.lb_lot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EndTrayFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "托盘完成";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_reject;
        private System.Windows.Forms.Label lb_InFrame;
        private System.Windows.Forms.Label lb_lot;
        private System.Windows.Forms.Label lb_noProcess;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
    }
}