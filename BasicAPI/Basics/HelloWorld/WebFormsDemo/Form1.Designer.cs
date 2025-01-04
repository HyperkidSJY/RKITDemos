namespace WebFormsDemo
{
    partial class Form1
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(74, 46);
            button1.Name = "button1";
            button1.Size = new Size(157, 36);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(74, 95);
            button2.Name = "button2";
            button2.Size = new Size(157, 36);
            button2.TabIndex = 2;
            button2.Text = "button2";
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(74, 145);
            button3.Name = "button3";
            button3.Size = new Size(157, 35);
            button3.TabIndex = 3;
            button3.Text = "button3";
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(74, 194);
            button4.Name = "button4";
            button4.Size = new Size(157, 35);
            button4.TabIndex = 4;
            button4.Text = "button4";
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(74, 243);
            button5.Name = "button5";
            button5.Size = new Size(157, 35);
            button5.TabIndex = 5;
            button5.Text = "button5";
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(74, 292);
            button6.Name = "button6";
            button6.Size = new Size(157, 36);
            button6.TabIndex = 6;
            button6.Text = "button6";
            button6.Click += button6_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(286, 46);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(378, 264);
            listBox1.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleBaseSize = new Size(7, 20);
            ClientSize = new Size(720, 385);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ListBox listBox1;
    }
}
