namespace 华泽_会计基础模拟考试
{
    partial class 功能测试
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmb借贷 = new System.Windows.Forms.ComboBox();
            this.cmb借贷科目 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(517, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(555, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "－";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // cmb借贷
            // 
            this.cmb借贷.FormattingEnabled = true;
            this.cmb借贷.Location = new System.Drawing.Point(58, 110);
            this.cmb借贷.Name = "cmb借贷";
            this.cmb借贷.Size = new System.Drawing.Size(81, 20);
            this.cmb借贷.TabIndex = 2;
            this.cmb借贷.Text = "借";
            // 
            // cmb借贷科目
            // 
            this.cmb借贷科目.FormattingEnabled = true;
            this.cmb借贷科目.Location = new System.Drawing.Point(159, 110);
            this.cmb借贷科目.Name = "cmb借贷科目";
            this.cmb借贷科目.Size = new System.Drawing.Size(206, 20);
            this.cmb借贷科目.TabIndex = 3;
            this.cmb借贷科目.Text = "投资收益";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(371, 111);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(84, 21);
            this.textBox1.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(335, 232);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(162, 30);
            this.button3.TabIndex = 5;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // 功能测试
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 467);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cmb借贷科目);
            this.Controls.Add(this.cmb借贷);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "功能测试";
            this.Text = "功能测试";
            this.Load += new System.EventHandler(this.功能测试_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cmb借贷;
        private System.Windows.Forms.ComboBox cmb借贷科目;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
    }
}