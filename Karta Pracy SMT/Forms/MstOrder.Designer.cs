namespace Karta_Pracy_SMT.Forms
{
    partial class MstOrder
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox12NC = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panelOK = new System.Windows.Forms.Panel();
            this.textBoxPcbOnMb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelQuantity = new System.Windows.Forms.Panel();
            this.textBoxQuantity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panelOrderNo = new System.Windows.Forms.Panel();
            this.textBoxOrderNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelOperator = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxOperator = new System.Windows.Forms.ComboBox();
            this.panelStencil = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxStencil = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelOK.SuspendLayout();
            this.panelQuantity.SuspendLayout();
            this.panelOrderNo.SuspendLayout();
            this.panelOperator.SuspendLayout();
            this.panelStencil.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonOK, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelOK, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panelQuantity, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panelOrderNo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelOperator, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelStencil, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 361);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonOK.Location = new System.Drawing.Point(3, 303);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(794, 57);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "Uzupełnij dane";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.textBox12NC);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 101);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 48);
            this.panel1.TabIndex = 4;
            // 
            // textBox12NC
            // 
            this.textBox12NC.BackColor = System.Drawing.Color.Red;
            this.textBox12NC.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox12NC.Location = new System.Drawing.Point(212, 9);
            this.textBox12NC.Name = "textBox12NC";
            this.textBox12NC.Size = new System.Drawing.Size(279, 29);
            this.textBox12NC.TabIndex = 4;
            this.textBox12NC.Leave += new System.EventHandler(this.textBoxOrderNumber_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(86, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 24);
            this.label10.TabIndex = 2;
            this.label10.Text = "Model 12NC:";
            // 
            // panelOK
            // 
            this.panelOK.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelOK.Controls.Add(this.textBoxPcbOnMb);
            this.panelOK.Controls.Add(this.label4);
            this.panelOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOK.Location = new System.Drawing.Point(1, 251);
            this.panelOK.Margin = new System.Windows.Forms.Padding(1);
            this.panelOK.Name = "panelOK";
            this.panelOK.Size = new System.Drawing.Size(798, 48);
            this.panelOK.TabIndex = 6;
            // 
            // textBoxPcbOnMb
            // 
            this.textBoxPcbOnMb.BackColor = System.Drawing.Color.Red;
            this.textBoxPcbOnMb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxPcbOnMb.Location = new System.Drawing.Point(212, 10);
            this.textBoxPcbOnMb.Name = "textBoxPcbOnMb";
            this.textBoxPcbOnMb.Size = new System.Drawing.Size(279, 29);
            this.textBoxPcbOnMb.TabIndex = 9;
            this.textBoxPcbOnMb.TextChanged += new System.EventHandler(this.textBoxPcbOnMb_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(52, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ilość PCB na MB:";
            // 
            // panelQuantity
            // 
            this.panelQuantity.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelQuantity.Controls.Add(this.textBoxQuantity);
            this.panelQuantity.Controls.Add(this.label7);
            this.panelQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQuantity.Location = new System.Drawing.Point(1, 201);
            this.panelQuantity.Margin = new System.Windows.Forms.Padding(1);
            this.panelQuantity.Name = "panelQuantity";
            this.panelQuantity.Size = new System.Drawing.Size(798, 48);
            this.panelQuantity.TabIndex = 5;
            // 
            // textBoxQuantity
            // 
            this.textBoxQuantity.BackColor = System.Drawing.Color.Red;
            this.textBoxQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxQuantity.Location = new System.Drawing.Point(212, 10);
            this.textBoxQuantity.Name = "textBoxQuantity";
            this.textBoxQuantity.Size = new System.Drawing.Size(279, 29);
            this.textBoxQuantity.TabIndex = 8;
            this.textBoxQuantity.TextChanged += new System.EventHandler(this.textBoxQuantity_TextChanged_1);
            this.textBoxQuantity.Leave += new System.EventHandler(this.textBoxQuantity_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(32, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 24);
            this.label7.TabIndex = 7;
            this.label7.Text = "Ilość do wykonania:";
            // 
            // panelOrderNo
            // 
            this.panelOrderNo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelOrderNo.Controls.Add(this.textBoxOrderNumber);
            this.panelOrderNo.Controls.Add(this.label2);
            this.panelOrderNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrderNo.Location = new System.Drawing.Point(1, 51);
            this.panelOrderNo.Margin = new System.Windows.Forms.Padding(1);
            this.panelOrderNo.Name = "panelOrderNo";
            this.panelOrderNo.Size = new System.Drawing.Size(798, 48);
            this.panelOrderNo.TabIndex = 1;
            // 
            // textBoxOrderNumber
            // 
            this.textBoxOrderNumber.BackColor = System.Drawing.Color.Red;
            this.textBoxOrderNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxOrderNumber.Location = new System.Drawing.Point(212, 9);
            this.textBoxOrderNumber.Name = "textBoxOrderNumber";
            this.textBoxOrderNumber.Size = new System.Drawing.Size(279, 29);
            this.textBoxOrderNumber.TabIndex = 3;
            this.textBoxOrderNumber.TextChanged += new System.EventHandler(this.textBoxOrderNumber_TextChanged);
            this.textBoxOrderNumber.Leave += new System.EventHandler(this.textBoxOrderNumber_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(96, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nr zlecenia:";
            // 
            // panelOperator
            // 
            this.panelOperator.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelOperator.Controls.Add(this.label1);
            this.panelOperator.Controls.Add(this.comboBoxOperator);
            this.panelOperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOperator.Location = new System.Drawing.Point(1, 1);
            this.panelOperator.Margin = new System.Windows.Forms.Padding(1);
            this.panelOperator.Name = "panelOperator";
            this.panelOperator.Size = new System.Drawing.Size(798, 48);
            this.panelOperator.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(117, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Operator:";
            // 
            // comboBoxOperator
            // 
            this.comboBoxOperator.BackColor = System.Drawing.Color.Red;
            this.comboBoxOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxOperator.FormattingEnabled = true;
            this.comboBoxOperator.Location = new System.Drawing.Point(212, 9);
            this.comboBoxOperator.Name = "comboBoxOperator";
            this.comboBoxOperator.Size = new System.Drawing.Size(279, 32);
            this.comboBoxOperator.TabIndex = 0;
            this.comboBoxOperator.SelectedIndexChanged += new System.EventHandler(this.comboBoxOperator_SelectedIndexChanged);
            this.comboBoxOperator.Leave += new System.EventHandler(this.comboBoxOperator_Leave_1);
            // 
            // panelStencil
            // 
            this.panelStencil.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelStencil.Controls.Add(this.label3);
            this.panelStencil.Controls.Add(this.textBoxStencil);
            this.panelStencil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStencil.Location = new System.Drawing.Point(1, 151);
            this.panelStencil.Margin = new System.Windows.Forms.Padding(1);
            this.panelStencil.Name = "panelStencil";
            this.panelStencil.Size = new System.Drawing.Size(798, 48);
            this.panelStencil.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(135, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Stencil:";
            // 
            // textBoxStencil
            // 
            this.textBoxStencil.BackColor = System.Drawing.Color.Red;
            this.textBoxStencil.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxStencil.Location = new System.Drawing.Point(212, 11);
            this.textBoxStencil.Name = "textBoxStencil";
            this.textBoxStencil.Size = new System.Drawing.Size(279, 29);
            this.textBoxStencil.TabIndex = 4;
            this.textBoxStencil.TextChanged += new System.EventHandler(this.textBoxStencil_TextChanged);
            this.textBoxStencil.Leave += new System.EventHandler(this.textBoxOrderNumber_Leave);
            // 
            // MstOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 361);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MstOrder";
            this.Text = "MstOrder";
            this.Load += new System.EventHandler(this.MstOrder_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelOK.ResumeLayout(false);
            this.panelOK.PerformLayout();
            this.panelQuantity.ResumeLayout(false);
            this.panelQuantity.PerformLayout();
            this.panelOrderNo.ResumeLayout(false);
            this.panelOrderNo.PerformLayout();
            this.panelOperator.ResumeLayout(false);
            this.panelOperator.PerformLayout();
            this.panelStencil.ResumeLayout(false);
            this.panelStencil.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelOK;
        private System.Windows.Forms.Panel panelQuantity;
        private System.Windows.Forms.Panel panelStencil;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxStencil;
        private System.Windows.Forms.Panel panelOrderNo;
        private System.Windows.Forms.TextBox textBoxOrderNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelOperator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxOperator;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxQuantity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox12NC;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxPcbOnMb;
        private System.Windows.Forms.Label label4;
    }
}