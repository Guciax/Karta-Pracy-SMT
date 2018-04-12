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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.dataGridView3DaysInfo = new System.Windows.Forms.DataGridView();
            this.pictureBoxShifts = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.labelLine = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelRight = new System.Windows.Forms.Panel();
            this.labelWasteLed = new System.Windows.Forms.Label();
            this.labelModuleWaste = new System.Windows.Forms.Label();
            this.labelLotsThisShift = new System.Windows.Forms.Label();
            this.labelEfficiency = new System.Windows.Forms.Label();
            this.pbChart = new System.Windows.Forms.PictureBox();
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
            this.Operator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stencil = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerMiraeStalker = new System.Windows.Forms.Timer(this.components);
            this.timerLedLeftSave = new System.Windows.Forms.Timer(this.components);
            this.EfficiencyTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3DaysInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShifts)).BeginInit();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelLeft);
            this.panel1.Controls.Add(this.panelRight);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 100);
            this.panel1.TabIndex = 1;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.dataGridView3DaysInfo);
            this.panelLeft.Controls.Add(this.pictureBoxShifts);
            this.panelLeft.Controls.Add(this.button2);
            this.panelLeft.Controls.Add(this.labelLine);
            this.panelLeft.Controls.Add(this.button1);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(602, 100);
            this.panelLeft.TabIndex = 10;
            // 
            // dataGridView3DaysInfo
            // 
            this.dataGridView3DaysInfo.AllowUserToAddRows = false;
            this.dataGridView3DaysInfo.AllowUserToDeleteRows = false;
            this.dataGridView3DaysInfo.AllowUserToResizeColumns = false;
            this.dataGridView3DaysInfo.AllowUserToResizeRows = false;
            this.dataGridView3DaysInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView3DaysInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3DaysInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView3DaysInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView3DaysInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView3DaysInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridView3DaysInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView3DaysInfo.Location = new System.Drawing.Point(99, 0);
            this.dataGridView3DaysInfo.Name = "dataGridView3DaysInfo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3DaysInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView3DaysInfo.Size = new System.Drawing.Size(335, 100);
            this.dataGridView3DaysInfo.TabIndex = 5;
            this.dataGridView3DaysInfo.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentDoubleClick);
            this.dataGridView3DaysInfo.SelectionChanged += new System.EventHandler(this.dataGridView3DaysInfo_SelectionChanged);
            // 
            // pictureBoxShifts
            // 
            this.pictureBoxShifts.BackColor = System.Drawing.Color.Black;
            this.pictureBoxShifts.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBoxShifts.Location = new System.Drawing.Point(434, 0);
            this.pictureBoxShifts.Name = "pictureBoxShifts";
            this.pictureBoxShifts.Size = new System.Drawing.Size(168, 100);
            this.pictureBoxShifts.TabIndex = 6;
            this.pictureBoxShifts.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelLine
            // 
            this.labelLine.AutoSize = true;
            this.labelLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelLine.Location = new System.Drawing.Point(3, 71);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(50, 20);
            this.labelLine.TabIndex = 1;
            this.labelLine.Text = "Linia: ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 59);
            this.button1.TabIndex = 0;
            this.button1.Text = "Nowy LOT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.labelWasteLed);
            this.panelRight.Controls.Add(this.labelModuleWaste);
            this.panelRight.Controls.Add(this.labelLotsThisShift);
            this.panelRight.Controls.Add(this.labelEfficiency);
            this.panelRight.Controls.Add(this.pbChart);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(602, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(768, 100);
            this.panelRight.TabIndex = 9;
            // 
            // labelWasteLed
            // 
            this.labelWasteLed.AutoSize = true;
            this.labelWasteLed.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelWasteLed.Location = new System.Drawing.Point(3, 6);
            this.labelWasteLed.Name = "labelWasteLed";
            this.labelWasteLed.Size = new System.Drawing.Size(64, 22);
            this.labelWasteLed.TabIndex = 3;
            this.labelWasteLed.Text = "Odpad";
            // 
            // labelModuleWaste
            // 
            this.labelModuleWaste.AutoSize = true;
            this.labelModuleWaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelModuleWaste.Location = new System.Drawing.Point(3, 28);
            this.labelModuleWaste.Name = "labelModuleWaste";
            this.labelModuleWaste.Size = new System.Drawing.Size(64, 22);
            this.labelModuleWaste.TabIndex = 7;
            this.labelModuleWaste.Text = "Odpad";
            // 
            // labelLotsThisShift
            // 
            this.labelLotsThisShift.AutoSize = true;
            this.labelLotsThisShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelLotsThisShift.Location = new System.Drawing.Point(3, 50);
            this.labelLotsThisShift.Name = "labelLotsThisShift";
            this.labelLotsThisShift.Size = new System.Drawing.Size(55, 22);
            this.labelLotsThisShift.TabIndex = 4;
            this.labelLotsThisShift.Text = "LOTy";
            // 
            // labelEfficiency
            // 
            this.labelEfficiency.AutoSize = true;
            this.labelEfficiency.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEfficiency.Location = new System.Drawing.Point(3, 72);
            this.labelEfficiency.Name = "labelEfficiency";
            this.labelEfficiency.Size = new System.Drawing.Size(55, 22);
            this.labelEfficiency.TabIndex = 6;
            this.labelEfficiency.Text = "LOTy";
            // 
            // pbChart
            // 
            this.pbChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbChart.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbChart.Location = new System.Drawing.Point(535, 0);
            this.pbChart.Name = "pbChart";
            this.pbChart.Size = new System.Drawing.Size(233, 100);
            this.pbChart.TabIndex = 8;
            this.pbChart.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
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
            this.Operator,
            this.Stencil});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 100);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 50;
            this.dataGridView1.RowTemplate.Height = 35;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dataGridView1.Size = new System.Drawing.Size(1370, 649);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // ColumnSaved
            // 
            this.ColumnSaved.FillWeight = 50F;
            this.ColumnSaved.HeaderText = "Zapisano";
            this.ColumnSaved.Name = "ColumnSaved";
            this.ColumnSaved.ReadOnly = true;
            this.ColumnSaved.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnSaved.Width = 50;
            // 
            // ColumnLot
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ColumnLot.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnLot.HeaderText = "LOT";
            this.ColumnLot.Name = "ColumnLot";
            this.ColumnLot.ReadOnly = true;
            this.ColumnLot.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnModel
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ColumnModel.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColumnModel.HeaderText = "Model";
            this.ColumnModel.Name = "ColumnModel";
            this.ColumnModel.ReadOnly = true;
            this.ColumnModel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // connQty
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.connQty.DefaultCellStyle = dataGridViewCellStyle7;
            this.connQty.HeaderText = "Złączki";
            this.connQty.Name = "connQty";
            this.connQty.ReadOnly = true;
            this.connQty.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.connQty.Width = 50;
            // 
            // ColumnQualityCheck
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            this.ColumnQualityCheck.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColumnQualityCheck.HeaderText = "Kontrola";
            this.ColumnQualityCheck.Name = "ColumnQualityCheck";
            this.ColumnQualityCheck.ReadOnly = true;
            this.ColumnQualityCheck.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnQualityCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ColumnQty
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ColumnQty.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColumnQty.HeaderText = "Ilość";
            this.ColumnQty.Name = "ColumnQty";
            this.ColumnQty.ReadOnly = true;
            this.ColumnQty.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Rank12NC
            // 
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Rank12NC.DefaultCellStyle = dataGridViewCellStyle10;
            this.Rank12NC.HeaderText = "Dioda";
            this.Rank12NC.Name = "Rank12NC";
            this.Rank12NC.ReadOnly = true;
            this.Rank12NC.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnRankA
            // 
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ColumnRankA.DefaultCellStyle = dataGridViewCellStyle11;
            this.ColumnRankA.HeaderText = "Rank A";
            this.ColumnRankA.Name = "ColumnRankA";
            this.ColumnRankA.ReadOnly = true;
            this.ColumnRankA.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnRankB
            // 
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ColumnRankB.DefaultCellStyle = dataGridViewCellStyle12;
            this.ColumnRankB.HeaderText = "Rank B";
            this.ColumnRankB.Name = "ColumnRankB";
            this.ColumnRankB.ReadOnly = true;
            this.ColumnRankB.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // goodQty
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.goodQty.DefaultCellStyle = dataGridViewCellStyle13;
            this.goodQty.HeaderText = "Zrobione";
            this.goodQty.Name = "goodQty";
            this.goodQty.ReadOnly = true;
            this.goodQty.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Ng
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Red;
            this.Ng.DefaultCellStyle = dataGridViewCellStyle14;
            this.Ng.HeaderText = "NG";
            this.Ng.Name = "Ng";
            this.Ng.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Scrap
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            this.Scrap.DefaultCellStyle = dataGridViewCellStyle15;
            this.Scrap.HeaderText = "Scrap";
            this.Scrap.Name = "Scrap";
            this.Scrap.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Scrap.Visible = false;
            // 
            // ColumnButtonLed
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.White;
            this.ColumnButtonLed.DefaultCellStyle = dataGridViewCellStyle16;
            this.ColumnButtonLed.HeaderText = "Końcówki LED";
            this.ColumnButtonLed.Name = "ColumnButtonLed";
            this.ColumnButtonLed.ReadOnly = true;
            this.ColumnButtonLed.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // StartDate
            // 
            this.StartDate.HeaderText = "Start";
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            this.StartDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StartDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EndDate
            // 
            this.EndDate.HeaderText = "Koniec";
            this.EndDate.Name = "EndDate";
            this.EndDate.ReadOnly = true;
            this.EndDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Operator
            // 
            this.Operator.HeaderText = "Operator";
            this.Operator.Name = "Operator";
            this.Operator.ReadOnly = true;
            this.Operator.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Stencil
            // 
            this.Stencil.HeaderText = "Stencil";
            this.Stencil.Name = "Stencil";
            this.Stencil.Visible = false;
            // 
            // timerMiraeStalker
            // 
            this.timerMiraeStalker.Interval = 5000;
            this.timerMiraeStalker.Tick += new System.EventHandler(this.timerMiraeStalker_Tick);
            // 
            // timerLedLeftSave
            // 
            this.timerLedLeftSave.Enabled = true;
            this.timerLedLeftSave.Interval = 5000;
            this.timerLedLeftSave.Tick += new System.EventHandler(this.timerLedLeftSave_Tick);
            // 
            // EfficiencyTimer
            // 
            this.EfficiencyTimer.Enabled = true;
            this.EfficiencyTimer.Interval = 60000;
            this.EfficiencyTimer.Tick += new System.EventHandler(this.EfficiencyTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Karta pracy SMT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3DaysInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShifts)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timerMiraeStalker;
        private System.Windows.Forms.Label labelLine;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timerLedLeftSave;
        private System.Windows.Forms.Timer EfficiencyTimer;
        private System.Windows.Forms.Label labelWasteLed;
        private System.Windows.Forms.Label labelLotsThisShift;
        private System.Windows.Forms.DataGridView dataGridView3DaysInfo;
        private System.Windows.Forms.Label labelEfficiency;
        private System.Windows.Forms.Label labelModuleWaste;
        private System.Windows.Forms.PictureBox pbChart;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.PictureBox pictureBoxShifts;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Operator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stencil;
    }
}

