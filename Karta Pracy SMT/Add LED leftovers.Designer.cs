namespace Karta_Pracy_SMT
{
    partial class Add_LED_leftovers
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewRankA = new System.Windows.Forms.DataGridView();
            this.RankANazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankARank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankAId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankAIlosc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewRankB = new System.Windows.Forms.DataGridView();
            this.RankBNc12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankBRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankBId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankBIlosc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRankA)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRankB)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(886, 44);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridViewRankA);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 218);
            this.panel1.TabIndex = 3;
            // 
            // dataGridViewRankA
            // 
            this.dataGridViewRankA.AllowUserToAddRows = false;
            this.dataGridViewRankA.AllowUserToResizeRows = false;
            this.dataGridViewRankA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRankA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RankANazwa,
            this.RankARank,
            this.RankAId,
            this.RankAIlosc});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewRankA.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewRankA.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewRankA.Location = new System.Drawing.Point(0, 51);
            this.dataGridViewRankA.Name = "dataGridViewRankA";
            this.dataGridViewRankA.RowHeadersVisible = false;
            this.dataGridViewRankA.Size = new System.Drawing.Size(450, 167);
            this.dataGridViewRankA.TabIndex = 2;
            // 
            // RankANazwa
            // 
            this.RankANazwa.HeaderText = "Nazwa";
            this.RankANazwa.Name = "RankANazwa";
            this.RankANazwa.ReadOnly = true;
            this.RankANazwa.Width = 250;
            // 
            // RankARank
            // 
            this.RankARank.HeaderText = "Rank";
            this.RankARank.Name = "RankARank";
            this.RankARank.ReadOnly = true;
            // 
            // RankAId
            // 
            this.RankAId.HeaderText = "ID";
            this.RankAId.Name = "RankAId";
            this.RankAId.ReadOnly = true;
            // 
            // RankAIlosc
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.RankAIlosc.DefaultCellStyle = dataGridViewCellStyle1;
            this.RankAIlosc.HeaderText = "Ilość";
            this.RankAIlosc.Name = "RankAIlosc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(152, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rank A";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridViewRankB);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(450, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(436, 218);
            this.panel2.TabIndex = 4;
            // 
            // dataGridViewRankB
            // 
            this.dataGridViewRankB.AllowUserToAddRows = false;
            this.dataGridViewRankB.AllowUserToResizeRows = false;
            this.dataGridViewRankB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRankB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RankBNc12,
            this.RankBRank,
            this.RankBId,
            this.RankBIlosc});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewRankB.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewRankB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewRankB.Location = new System.Drawing.Point(0, 51);
            this.dataGridViewRankB.Name = "dataGridViewRankB";
            this.dataGridViewRankB.RowHeadersVisible = false;
            this.dataGridViewRankB.Size = new System.Drawing.Size(436, 167);
            this.dataGridViewRankB.TabIndex = 3;
            // 
            // RankBNc12
            // 
            this.RankBNc12.HeaderText = "Nazwa";
            this.RankBNc12.Name = "RankBNc12";
            this.RankBNc12.ReadOnly = true;
            this.RankBNc12.Width = 250;
            // 
            // RankBRank
            // 
            this.RankBRank.HeaderText = "Rank";
            this.RankBRank.Name = "RankBRank";
            this.RankBRank.ReadOnly = true;
            // 
            // RankBId
            // 
            this.RankBId.HeaderText = "ID";
            this.RankBId.Name = "RankBId";
            this.RankBId.ReadOnly = true;
            // 
            // RankBIlosc
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.RankBIlosc.DefaultCellStyle = dataGridViewCellStyle3;
            this.RankBIlosc.HeaderText = "Ilość";
            this.RankBIlosc.Name = "RankBIlosc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(182, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rank B";
            // 
            // Add_LED_leftovers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 262);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "Add_LED_leftovers";
            this.Text = "Końcówki LED";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRankA)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRankB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridViewRankA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridViewRankB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankANazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankARank;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankAId;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankAIlosc;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankBNc12;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankBRank;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankBId;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankBIlosc;
    }
}