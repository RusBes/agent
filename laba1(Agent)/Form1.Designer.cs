namespace laba1_Agent_
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.Panel();
            this.butNextStep = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbRandLoc = new System.Windows.Forms.CheckBox();
            this.chbRandType = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.butNewClient = new System.Windows.Forms.Button();
            this.cbCustomerType = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(613, 382);
            this.panel.TabIndex = 0;
            // 
            // butNextStep
            // 
            this.butNextStep.Location = new System.Drawing.Point(619, 323);
            this.butNextStep.Name = "butNextStep";
            this.butNextStep.Size = new System.Drawing.Size(177, 47);
            this.butNextStep.TabIndex = 1;
            this.butNextStep.Text = "Наступний крок";
            this.butNextStep.UseVisualStyleBackColor = true;
            this.butNextStep.Click += new System.EventHandler(this.butNextStep_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbRandLoc);
            this.groupBox1.Controls.Add(this.chbRandType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudY);
            this.groupBox1.Controls.Add(this.nudX);
            this.groupBox1.Controls.Add(this.butNewClient);
            this.groupBox1.Controls.Add(this.cbCustomerType);
            this.groupBox1.Location = new System.Drawing.Point(619, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 143);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Новий відвідувач";
            // 
            // chbRandLoc
            // 
            this.chbRandLoc.AutoSize = true;
            this.chbRandLoc.Location = new System.Drawing.Point(93, 58);
            this.chbRandLoc.Name = "chbRandLoc";
            this.chbRandLoc.Size = new System.Drawing.Size(78, 17);
            this.chbRandLoc.TabIndex = 6;
            this.chbRandLoc.Text = "Навмання";
            this.chbRandLoc.UseVisualStyleBackColor = true;
            this.chbRandLoc.CheckedChanged += new System.EventHandler(this.chbRandLoc_CheckedChanged);
            // 
            // chbRandType
            // 
            this.chbRandType.AutoSize = true;
            this.chbRandType.Location = new System.Drawing.Point(94, 21);
            this.chbRandType.Name = "chbRandType";
            this.chbRandType.Size = new System.Drawing.Size(78, 17);
            this.chbRandType.TabIndex = 5;
            this.chbRandType.Text = "Навмання";
            this.chbRandType.UseVisualStyleBackColor = true;
            this.chbRandType.CheckedChanged += new System.EventHandler(this.chbRandType_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "x";
            // 
            // nudY
            // 
            this.nudY.Location = new System.Drawing.Point(22, 46);
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(65, 20);
            this.nudY.TabIndex = 3;
            // 
            // nudX
            // 
            this.nudX.Location = new System.Drawing.Point(23, 72);
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(65, 20);
            this.nudX.TabIndex = 2;
            // 
            // butNewClient
            // 
            this.butNewClient.Location = new System.Drawing.Point(6, 98);
            this.butNewClient.Name = "butNewClient";
            this.butNewClient.Size = new System.Drawing.Size(165, 39);
            this.butNewClient.TabIndex = 1;
            this.butNewClient.Text = "Новий клієнт";
            this.butNewClient.UseVisualStyleBackColor = true;
            this.butNewClient.Click += new System.EventHandler(this.butNewClient_Click);
            // 
            // cbCustomerType
            // 
            this.cbCustomerType.FormattingEnabled = true;
            this.cbCustomerType.Location = new System.Drawing.Point(6, 19);
            this.cbCustomerType.Name = "cbCustomerType";
            this.cbCustomerType.Size = new System.Drawing.Size(82, 21);
            this.cbCustomerType.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(619, 161);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 382);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butNextStep);
            this.Controls.Add(this.panel);
            this.Name = "Form1";
            this.Text = "Інтелектуальний агент";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button butNextStep;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.Button butNewClient;
        private System.Windows.Forms.ComboBox cbCustomerType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbRandType;
        private System.Windows.Forms.CheckBox chbRandLoc;
        private System.Windows.Forms.ListBox listBox1;
    }
}

