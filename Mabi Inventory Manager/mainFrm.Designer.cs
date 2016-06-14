namespace Mabi_Inventory_Manager
{
    partial class mainFrm
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
            this.fromFileBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.chooseDirBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fromFileBtn
            // 
            this.fromFileBtn.Location = new System.Drawing.Point(43, 156);
            this.fromFileBtn.Name = "fromFileBtn";
            this.fromFileBtn.Size = new System.Drawing.Size(75, 23);
            this.fromFileBtn.TabIndex = 2;
            this.fromFileBtn.Text = "From file";
            this.fromFileBtn.UseVisualStyleBackColor = true;
            this.fromFileBtn.Click += new System.EventHandler(this.fromFileBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(43, 45);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(43, 101);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 1;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // chooseDirBtn
            // 
            this.chooseDirBtn.Location = new System.Drawing.Point(169, 101);
            this.chooseDirBtn.Name = "chooseDirBtn";
            this.chooseDirBtn.Size = new System.Drawing.Size(75, 23);
            this.chooseDirBtn.TabIndex = 3;
            this.chooseDirBtn.Text = "Choose Dir";
            this.chooseDirBtn.UseVisualStyleBackColor = true;
            this.chooseDirBtn.Click += new System.EventHandler(this.chooseDirBtn_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.chooseDirBtn);
            this.Controls.Add(this.fromFileBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.startBtn);
            this.Name = "mainFrm";
            this.Text = "Mabi Packets";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button fromFileBtn;
        private System.Windows.Forms.Button chooseDirBtn;
    }
}

