﻿namespace Karta_Pracy_SMT
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
            this.comboBoxInspect = new System.Windows.Forms.ComboBox();
            this.comboBoxTechn = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxInspector = new Karta_Pracy_SMT.MyCheckBox();
            this.checkBoxTechncian = new Karta_Pracy_SMT.MyCheckBox();
            this.SuspendLayout();
            // 
            // comboBoxInspect
            // 
            this.comboBoxInspect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxInspect.FormattingEnabled = true;
            this.comboBoxInspect.Location = new System.Drawing.Point(12, 175);
            this.comboBoxInspect.Name = "comboBoxInspect";
            this.comboBoxInspect.Size = new System.Drawing.Size(267, 32);
            this.comboBoxInspect.TabIndex = 0;
            this.comboBoxInspect.TextChanged += new System.EventHandler(this.comboBoxInspect_TextChanged);
            // 
            // comboBoxTechn
            // 
            this.comboBoxTechn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxTechn.FormattingEnabled = true;
            this.comboBoxTechn.Location = new System.Drawing.Point(12, 96);
            this.comboBoxTechn.Name = "comboBoxTechn";
            this.comboBoxTechn.Size = new System.Drawing.Size(267, 32);
            this.comboBoxTechn.TabIndex = 1;
            this.comboBoxTechn.TextChanged += new System.EventHandler(this.comboBoxTechn_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Technik (wybierz lub wpisz)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Inspektor jakości (wybierz lub wpisz)";
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
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(12, 228);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(418, 62);
            this.button1.TabIndex = 8;
            this.button1.Text = "Uzupełnij dane";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxInspector
            // 
            this.checkBoxInspector.Location = new System.Drawing.Point(286, 175);
            this.checkBoxInspector.Name = "checkBoxInspector";
            this.checkBoxInspector.Size = new System.Drawing.Size(104, 32);
            this.checkBoxInspector.TabIndex = 7;
            this.checkBoxInspector.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxInspector.UseVisualStyleBackColor = true;
            this.checkBoxInspector.CheckedChanged += new System.EventHandler(this.checkBoxTechncian_CheckedChanged_1);
            // 
            // checkBoxTechncian
            // 
            this.checkBoxTechncian.Location = new System.Drawing.Point(286, 96);
            this.checkBoxTechncian.Name = "checkBoxTechncian";
            this.checkBoxTechncian.Size = new System.Drawing.Size(104, 32);
            this.checkBoxTechncian.TabIndex = 6;
            this.checkBoxTechncian.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxTechncian.UseVisualStyleBackColor = true;
            this.checkBoxTechncian.CheckedChanged += new System.EventHandler(this.checkBoxTechncian_CheckedChanged_1);
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
            this.Controls.Add(this.comboBoxTechn);
            this.Controls.Add(this.comboBoxInspect);
            this.Name = "ChangeOverConfirmationCard";
            this.Text = "ChangeOverConfirmationCard";
            this.Load += new System.EventHandler(this.ChangeOverConfirmationCard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxInspect;
        private System.Windows.Forms.ComboBox comboBoxTechn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private MyCheckBox checkBoxTechncian;
        private MyCheckBox checkBoxInspector;
        private System.Windows.Forms.Button button1;
    }
}