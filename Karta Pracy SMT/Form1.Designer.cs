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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnSaved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnLot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQualityCheck = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rank12NC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRankA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRankB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scrap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnButtonLed = new System.Windows.Forms.DataGridViewButtonColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnSave = new System.Windows.Forms.DataGridViewButtonColumn();
            this.timerMiraeStalker = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1443, 89);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 82);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSaved,
            this.ColumnLot,
            this.ColumnModel,
            this.connQty,
            this.ColumnQualityCheck,
            this.ColumnQty,
            this.Rank12NC,
            this.ColumnRankA,
            this.ColumnRankB,
            this.goodQty,
            this.Ng,
            this.Scrap,
            this.ColumnButtonLed,
            this.StartDate,
            this.EndDate,
            this.BtnSave});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 89);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1443, 768);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridView1_Paint);
            // 
            // ColumnSaved
            // 
            this.ColumnSaved.FillWeight = 50F;
            this.ColumnSaved.HeaderText = "Zapisano";
            this.ColumnSaved.Name = "ColumnSaved";
            this.ColumnSaved.Width = 50;
            // 
            // ColumnLot
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ColumnLot.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnLot.HeaderText = "LOT";
            this.ColumnLot.Name = "ColumnLot";
            this.ColumnLot.ReadOnly = true;
            // 
            // ColumnModel
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ColumnModel.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnModel.HeaderText = "Model";
            this.ColumnModel.Name = "ColumnModel";
            this.ColumnModel.ReadOnly = true;
            // 
            // connQty
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.connQty.DefaultCellStyle = dataGridViewCellStyle3;
            this.connQty.HeaderText = "Ilość złączek";
            this.connQty.Name = "connQty";
            this.connQty.Width = 50;
            // 
            // ColumnQualityCheck
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            this.ColumnQualityCheck.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnQualityCheck.HeaderText = "Kontrola 1 sztuki";
            this.ColumnQualityCheck.Name = "ColumnQualityCheck";
            this.ColumnQualityCheck.ReadOnly = true;
            this.ColumnQualityCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnQualityCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ColumnQty
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ColumnQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnQty.HeaderText = "Ilość zlecona";
            this.ColumnQty.Name = "ColumnQty";
            this.ColumnQty.ReadOnly = true;
            // 
            // Rank12NC
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Rank12NC.DefaultCellStyle = dataGridViewCellStyle6;
            this.Rank12NC.HeaderText = "Dioda";
            this.Rank12NC.Name = "Rank12NC";
            this.Rank12NC.ReadOnly = true;
            this.Rank12NC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColumnRankA
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ColumnRankA.DefaultCellStyle = dataGridViewCellStyle7;
            this.ColumnRankA.HeaderText = "Rank A";
            this.ColumnRankA.Name = "ColumnRankA";
            this.ColumnRankA.ReadOnly = true;
            // 
            // ColumnRankB
            // 
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ColumnRankB.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColumnRankB.HeaderText = "Rank B";
            this.ColumnRankB.Name = "ColumnRankB";
            this.ColumnRankB.ReadOnly = true;
            // 
            // goodQty
            // 
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.goodQty.DefaultCellStyle = dataGridViewCellStyle9;
            this.goodQty.HeaderText = "Wyprodukowane dobre";
            this.goodQty.Name = "goodQty";
            // 
            // Ng
            // 
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Red;
            this.Ng.DefaultCellStyle = dataGridViewCellStyle10;
            this.Ng.HeaderText = "NG";
            this.Ng.Name = "Ng";
            // 
            // Scrap
            // 
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            this.Scrap.DefaultCellStyle = dataGridViewCellStyle11;
            this.Scrap.HeaderText = "Scrap";
            this.Scrap.Name = "Scrap";
            // 
            // ColumnButtonLed
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            this.ColumnButtonLed.DefaultCellStyle = dataGridViewCellStyle12;
            this.ColumnButtonLed.HeaderText = "Końcówki LED";
            this.ColumnButtonLed.Name = "ColumnButtonLed";
            this.ColumnButtonLed.ReadOnly = true;
            // 
            // StartDate
            // 
            this.StartDate.HeaderText = "Start";
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            this.StartDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StartDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EndDate
            // 
            this.EndDate.HeaderText = "Koniec";
            this.EndDate.Name = "EndDate";
            this.EndDate.ReadOnly = true;
            // 
            // BtnSave
            // 
            this.BtnSave.HeaderText = "Zapisz";
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.ReadOnly = true;
            // 
            // timerMiraeStalker
            // 
            this.timerMiraeStalker.Enabled = true;
            this.timerMiraeStalker.Interval = 5000;
            this.timerMiraeStalker.Tick += new System.EventHandler(this.timerMiraeStalker_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 857);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn connQty;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnQualityCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rank12NC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRankA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRankB;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ng;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scrap;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnButtonLed;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridViewButtonColumn BtnSave;
        private System.Windows.Forms.Timer timerMiraeStalker;
    }
}

