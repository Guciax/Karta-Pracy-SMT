namespace Karta_Pracy_SMT
{
    partial class ChangeOverConfirmationCard
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxTechncian = new Karta_Pracy_SMT.MyCheckBox();
            this.checkBoxInspector = new Karta_Pracy_SMT.MyCheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 175);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(267, 32);
            this.comboBox1.TabIndex = 0;
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(12, 96);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(267, 32);
            this.comboBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Technik";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Inspektor jakości";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(405, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Potwierdzenie gotowości linii SMT do produkcji";
            // 
            // checkBoxTechncian
            // 
            this.checkBoxTechncian.Location = new System.Drawing.Point(286, 96);
            this.checkBoxTechncian.Name = "checkBoxTechncian";
            this.checkBoxTechncian.Size = new System.Drawing.Size(104, 32);
            this.checkBoxTechncian.TabIndex = 6;
            this.checkBoxTechncian.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxTechncian.UseVisualStyleBackColor = true;
            this.checkBoxTechncian.CheckedChanged += new System.EventHandler(this.checkBoxTechncian_CheckedChanged);
            // 
            // checkBoxInspector
            // 
            this.checkBoxInspector.Location = new System.Drawing.Point(286, 175);
            this.checkBoxInspector.Name = "checkBoxInspector";
            this.checkBoxInspector.Size = new System.Drawing.Size(104, 32);
            this.checkBoxInspector.TabIndex = 7;
            this.checkBoxInspector.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxInspector.UseVisualStyleBackColor = true;
            this.checkBoxInspector.CheckedChanged += new System.EventHandler(this.checkBoxInspector_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(12, 228);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(418, 62);
            this.button1.TabIndex = 8;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChangeOverConfirmationCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 302);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxInspector);
            this.Controls.Add(this.checkBoxTechncian);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Name = "ChangeOverConfirmationCard";
            this.Text = "ChangeOverConfirmationCard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private MyCheckBox checkBoxTechncian;
        private MyCheckBox checkBoxInspector;
        private System.Windows.Forms.Button button1;
    }
}