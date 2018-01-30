namespace Karta_Pracy_SMT
{
    partial class NewLotForm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelOrderedQty = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.textBoxLotNo = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelRankA = new System.Windows.Forms.Label();
            this.textBoxRankAQr = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewRankA = new System.Windows.Forms.DataGridView();
            this.RankANc12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankAId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankAIlosc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankAZlecenie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewRankB = new System.Windows.Forms.DataGridView();
            this.rankBNc12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankBId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankBIlosc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankBZlecenie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelRankB = new System.Windows.Forms.Label();
            this.textBoxRankBQr = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRankA)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRankB)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.labelOrderedQty);
            this.panel3.Controls.Add(this.labelModel);
            this.panel3.Controls.Add(this.textBoxLotNo);
            this.panel3.Controls.Add(this.textBox4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(884, 100);
            this.panel3.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nr zlecenia:";
            // 
            // labelOrderedQty
            // 
            this.labelOrderedQty.AutoSize = true;
            this.labelOrderedQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOrderedQty.ForeColor = System.Drawing.SystemColors.Control;
            this.labelOrderedQty.Location = new System.Drawing.Point(171, 64);
            this.labelOrderedQty.Name = "labelOrderedQty";
            this.labelOrderedQty.Size = new System.Drawing.Size(48, 24);
            this.labelOrderedQty.TabIndex = 4;
            this.labelOrderedQty.Text = "Ilość";
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelModel.ForeColor = System.Drawing.SystemColors.Control;
            this.labelModel.Location = new System.Drawing.Point(171, 37);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(73, 24);
            this.labelModel.TabIndex = 3;
            this.labelModel.Text = "Model: ";
            // 
            // textBoxLotNo
            // 
            this.textBoxLotNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxLotNo.Location = new System.Drawing.Point(123, 0);
            this.textBoxLotNo.Name = "textBoxLotNo";
            this.textBoxLotNo.Size = new System.Drawing.Size(635, 29);
            this.textBoxLotNo.TabIndex = 2;
            this.textBoxLotNo.TextChanged += new System.EventHandler(this.textBoxLotNo_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox4.Location = new System.Drawing.Point(64, 264);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(201, 26);
            this.textBox4.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonOK.Location = new System.Drawing.Point(0, 628);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(884, 38);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelRankA
            // 
            this.labelRankA.AutoSize = true;
            this.labelRankA.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRankA.Location = new System.Drawing.Point(140, 24);
            this.labelRankA.Name = "labelRankA";
            this.labelRankA.Size = new System.Drawing.Size(103, 31);
            this.labelRankA.TabIndex = 4;
            this.labelRankA.Text = "Rank A";
            // 
            // textBoxRankAQr
            // 
            this.textBoxRankAQr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxRankAQr.Location = new System.Drawing.Point(3, 358);
            this.textBoxRankAQr.Name = "textBoxRankAQr";
            this.textBoxRankAQr.Size = new System.Drawing.Size(440, 26);
            this.textBoxRankAQr.TabIndex = 6;
            this.textBoxRankAQr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dataGridViewRankA);
            this.panel1.Controls.Add(this.textBoxRankAQr);
            this.panel1.Controls.Add(this.labelRankA);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 528);
            this.panel1.TabIndex = 8;
            // 
            // dataGridViewRankA
            // 
            this.dataGridViewRankA.AllowUserToAddRows = false;
            this.dataGridViewRankA.AllowUserToResizeRows = false;
            this.dataGridViewRankA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRankA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RankANc12,
            this.RankAId,
            this.RankAIlosc,
            this.RankAZlecenie});
            this.dataGridViewRankA.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewRankA.Location = new System.Drawing.Point(0, 390);
            this.dataGridViewRankA.Name = "dataGridViewRankA";
            this.dataGridViewRankA.RowHeadersVisible = false;
            this.dataGridViewRankA.Size = new System.Drawing.Size(448, 136);
            this.dataGridViewRankA.TabIndex = 7;
            // 
            // RankANc12
            // 
            this.RankANc12.HeaderText = "Nazwa";
            this.RankANc12.Name = "RankANc12";
            this.RankANc12.Width = 250;
            // 
            // RankAId
            // 
            this.RankAId.HeaderText = "ID";
            this.RankAId.Name = "RankAId";
            // 
            // RankAIlosc
            // 
            this.RankAIlosc.HeaderText = "Ilosc ";
            this.RankAIlosc.Name = "RankAIlosc";
            // 
            // RankAZlecenie
            // 
            this.RankAZlecenie.HeaderText = "Zlecenie";
            this.RankAZlecenie.Name = "RankAZlecenie";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.textBoxRankBQr);
            this.panel2.Controls.Add(this.dataGridViewRankB);
            this.panel2.Controls.Add(this.labelRankB);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(450, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(434, 528);
            this.panel2.TabIndex = 10;
            // 
            // dataGridViewRankB
            // 
            this.dataGridViewRankB.AllowUserToAddRows = false;
            this.dataGridViewRankB.AllowUserToResizeRows = false;
            this.dataGridViewRankB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRankB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rankBNc12,
            this.rankBId,
            this.rankBIlosc,
            this.RankBZlecenie});
            this.dataGridViewRankB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewRankB.Location = new System.Drawing.Point(0, 390);
            this.dataGridViewRankB.Name = "dataGridViewRankB";
            this.dataGridViewRankB.RowHeadersVisible = false;
            this.dataGridViewRankB.Size = new System.Drawing.Size(432, 136);
            this.dataGridViewRankB.TabIndex = 8;
            // 
            // rankBNc12
            // 
            this.rankBNc12.HeaderText = "Nazwa";
            this.rankBNc12.Name = "rankBNc12";
            this.rankBNc12.Width = 250;
            // 
            // rankBId
            // 
            this.rankBId.HeaderText = "ID";
            this.rankBId.Name = "rankBId";
            // 
            // rankBIlosc
            // 
            this.rankBIlosc.HeaderText = "Ilosc ";
            this.rankBIlosc.Name = "rankBIlosc";
            // 
            // RankBZlecenie
            // 
            this.RankBZlecenie.HeaderText = "Zlecenie";
            this.RankBZlecenie.Name = "RankBZlecenie";
            // 
            // labelRankB
            // 
            this.labelRankB.AutoSize = true;
            this.labelRankB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRankB.Location = new System.Drawing.Point(143, 24);
            this.labelRankB.Name = "labelRankB";
            this.labelRankB.Size = new System.Drawing.Size(103, 31);
            this.labelRankB.TabIndex = 5;
            this.labelRankB.Text = "Rank B";
            // 
            // textBoxRankBQr
            // 
            this.textBoxRankBQr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxRankBQr.Location = new System.Drawing.Point(12, 358);
            this.textBoxRankBQr.Name = "textBoxRankBQr";
            this.textBoxRankBQr.Size = new System.Drawing.Size(417, 26);
            this.textBoxRankBQr.TabIndex = 9;
            this.textBoxRankBQr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRankBQr_KeyDown_1);
            // 
            // NewLotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 666);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.panel3);
            this.Name = "NewLotForm";
            this.Text = "NewLotForm";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRankA)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRankB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelOrderedQty;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.TextBox textBoxLotNo;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelRankA;
        private System.Windows.Forms.TextBox textBoxRankAQr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridViewRankA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankANc12;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankAId;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankAIlosc;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankAZlecenie;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridViewRankB;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankBNc12;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankBId;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankBIlosc;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankBZlecenie;
        private System.Windows.Forms.Label labelRankB;
        private System.Windows.Forms.TextBox textBoxRankBQr;
    }
}