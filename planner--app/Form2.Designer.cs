namespace planner__app
{
    partial class Log_in_form
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
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe Print", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(94, 154);
            label1.Name = "label1";
            label1.Size = new Size(150, 40);
            label1.TabIndex = 0;
            label1.Text = "Username :";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe Print", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(94, 189);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(374, 48);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe Print", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(94, 231);
            label2.Name = "label2";
            label2.Size = new Size(0, 40);
            label2.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe Print", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(94, 318);
            label3.Name = "label3";
            label3.Size = new Size(143, 40);
            label3.TabIndex = 3;
            label3.Text = "Password :";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe Print", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(94, 353);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.Size = new Size(374, 48);
            textBox2.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe Print", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(94, 395);
            label4.Name = "label4";
            label4.Size = new Size(0, 40);
            label4.TabIndex = 5;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe Print", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(95, 523);
            button1.Name = "button1";
            button1.Size = new Size(234, 66);
            button1.TabIndex = 6;
            button1.Text = "Log in";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe Print", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(398, 523);
            button2.Name = "button2";
            button2.Size = new Size(234, 66);
            button2.TabIndex = 7;
            button2.Text = "Sign in";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Log_in_form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.log_in_form;
            ClientSize = new Size(730, 810);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Log_in_form";
            Text = "Log in";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private Label label4;
        private Button button1;
        private Button button2;
    }
}