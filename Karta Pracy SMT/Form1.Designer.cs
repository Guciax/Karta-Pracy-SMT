namespace Karta_Pracy_SMT
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnSaved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnLot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQualityCheck = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRankA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankAInfo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnRankB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RankBInfo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnButtonLed = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1924, 110);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 98);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSaved,
            this.ColumnLot,
            this.ColumnModel,
            this.ColumnQualityCheck,
            this.ColumnQty,
            this.ColumnRankA,
            this.RankAInfo,
            this.ColumnRankB,
            this.RankBInfo,
            this.ColumnButtonLed,
            this.ColumnEndTime});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 110);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1924, 945);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // ColumnSaved
            // 
            this.ColumnSaved.FillWeight = 50F;
            this.ColumnSaved.HeaderText = "Zapisano";
            this.ColumnSaved.Name = "ColumnSaved";
            this.ColumnSaved.ReadOnly = true;
            this.ColumnSaved.Width = 50;
            // 
            // ColumnLot
            // 
            this.ColumnLot.HeaderText = "LOT";
            this.ColumnLot.Name = "ColumnLot";
            this.ColumnLot.ReadOnly = true;
            // 
            // ColumnModel
            // 
            this.ColumnModel.HeaderText = "Model";
            this.ColumnModel.Name = "ColumnModel";
            this.ColumnModel.ReadOnly = true;
            // 
            // ColumnQualityCheck
            // 
            this.ColumnQualityCheck.HeaderText = "Kontrola 1 sztuki";
            this.ColumnQualityCheck.Name = "ColumnQualityCheck";
            this.ColumnQualityCheck.ReadOnly = true;
            // 
            // ColumnQty
            // 
            this.ColumnQty.HeaderText = "Ilość";
            this.ColumnQty.Name = "ColumnQty";
            this.ColumnQty.ReadOnly = true;
            // 
            // ColumnRankA
            // 
            this.ColumnRankA.HeaderText = "Rank A";
            this.ColumnRankA.Name = "ColumnRankA";
            this.ColumnRankA.ReadOnly = true;
            // 
            // RankAInfo
            // 
            this.RankAInfo.HeaderText = "Rank A info";
            this.RankAInfo.Name = "RankAInfo";
            this.RankAInfo.ReadOnly = true;
            this.RankAInfo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.RankAInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ColumnRankB
            // 
            this.ColumnRankB.HeaderText = "Rank B";
            this.ColumnRankB.Name = "ColumnRankB";
            this.ColumnRankB.ReadOnly = true;
            // 
            // RankBInfo
            // 
            this.RankBInfo.HeaderText = "Rank B info";
            this.RankBInfo.Name = "RankBInfo";
            this.RankBInfo.ReadOnly = true;
            // 
            // ColumnButtonLed
            // 
            this.ColumnButtonLed.HeaderText = "Końcówki LED";
            this.ColumnButtonLed.Name = "ColumnButtonLed";
            this.ColumnButtonLed.ReadOnly = true;
            // 
            // ColumnEndTime
            // 
            this.ColumnEndTime.HeaderText = "Czas zakończenia";
            this.ColumnEndTime.Name = "ColumnEndTime";
            this.ColumnEndTime.ReadOnly = true;
            this.ColumnEndTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnEndTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Karta pracy SMT";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSaved;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLot;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQualityCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRankA;
        private System.Windows.Forms.DataGridViewButtonColumn RankAInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRankB;
        private System.Windows.Forms.DataGridViewButtonColumn RankBInfo;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnButtonLed;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEndTime;
    }
}

