namespace testSMBUryuUtility
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
            button1 = new Button();
            treeView1 = new TreeView();
            label1 = new Label();
            checkBox1 = new CheckBox();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            richTextBox1 = new RichTextBox();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(86, 25);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(384, 12);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(404, 572);
            treeView1.TabIndex = 1;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 290);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(12, 43);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(76, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "isEnabled";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(12, 68);
            button2.Name = "button2";
            button2.Size = new Size(132, 23);
            button2.TabIndex = 4;
            button2.Text = "arc zipfile";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(12, 97);
            button3.Name = "button3";
            button3.Size = new Size(132, 23);
            button3.TabIndex = 5;
            button3.Text = "skyline zipfile";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(12, 126);
            button4.Name = "button4";
            button4.Size = new Size(181, 23);
            button4.TabIndex = 6;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 155);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(262, 132);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            // 
            // button5
            // 
            button5.Location = new Point(12, 308);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 8;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(199, 126);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 9;
            button6.Text = "button6";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(12, 337);
            button7.Name = "button7";
            button7.Size = new Size(75, 23);
            button7.TabIndex = 10;
            button7.Text = "button7";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(93, 341);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 11;
            label2.Text = "label2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 596);
            Controls.Add(label2);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(richTextBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(checkBox1);
            Controls.Add(label1);
            Controls.Add(treeView1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TreeView treeView1;
        private Label label1;
        private CheckBox checkBox1;
        private Button button2;
        private Button button3;
        private Button button4;
        private RichTextBox richTextBox1;
        private Button button5;
        private Button button6;
        private Button button7;
        private Label label2;
    }
}
