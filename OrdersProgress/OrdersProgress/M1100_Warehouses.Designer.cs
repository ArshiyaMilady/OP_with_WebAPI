﻿namespace OrdersProgress
{
    partial class M1100_Warehouses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M1100_Warehouses));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radEnabledLevel = new System.Windows.Forms.RadioButton();
            this.radDisabledLevel = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkCanEdit = new System.Windows.Forms.CheckBox();
            this.chkShowUpdateMessage = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtST_Name = new System.Windows.Forms.TextBox();
            this.txtST_Phone = new System.Windows.Forms.TextBox();
            this.txtST_Address = new System.Windows.Forms.TextBox();
            this.cmbST_Name = new System.Windows.Forms.ComboBox();
            this.cmbST_Phone = new System.Windows.Forms.ComboBox();
            this.cmbST_Address = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnImportDataFromExcel = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnAddNew);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnImportDataFromExcel);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnDeleteAll);
            this.panel1.Controls.Add(this.dgvData);
            this.panel1.Location = new System.Drawing.Point(-1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(822, 335);
            this.panel1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.radAll);
            this.panel3.Controls.Add(this.radEnabledLevel);
            this.panel3.Controls.Add(this.radDisabledLevel);
            this.panel3.Location = new System.Drawing.Point(572, 76);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 83);
            this.panel3.TabIndex = 92;
            // 
            // radAll
            // 
            this.radAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radAll.AutoSize = true;
            this.radAll.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAll.Location = new System.Drawing.Point(141, 55);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(98, 19);
            this.radAll.TabIndex = 59;
            this.radAll.Text = "نمایش تمام انبارها";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.Visible = false;
            // 
            // radEnabledLevel
            // 
            this.radEnabledLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radEnabledLevel.AutoSize = true;
            this.radEnabledLevel.Checked = true;
            this.radEnabledLevel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEnabledLevel.Location = new System.Drawing.Point(130, 5);
            this.radEnabledLevel.Name = "radEnabledLevel";
            this.radEnabledLevel.Size = new System.Drawing.Size(109, 19);
            this.radEnabledLevel.TabIndex = 57;
            this.radEnabledLevel.TabStop = true;
            this.radEnabledLevel.Text = "نمایش انبارهای فعال";
            this.radEnabledLevel.UseVisualStyleBackColor = true;
            this.radEnabledLevel.CheckedChanged += new System.EventHandler(this.Rad_CheckedChanged);
            // 
            // radDisabledLevel
            // 
            this.radDisabledLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radDisabledLevel.AutoSize = true;
            this.radDisabledLevel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDisabledLevel.Location = new System.Drawing.Point(111, 30);
            this.radDisabledLevel.Name = "radDisabledLevel";
            this.radDisabledLevel.Size = new System.Drawing.Size(128, 19);
            this.radDisabledLevel.TabIndex = 58;
            this.radDisabledLevel.Text = "نمایش انبارهای غیر فعال";
            this.radDisabledLevel.UseVisualStyleBackColor = true;
            this.radDisabledLevel.CheckedChanged += new System.EventHandler(this.Rad_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.chkCanEdit);
            this.panel2.Controls.Add(this.chkShowUpdateMessage);
            this.panel2.Location = new System.Drawing.Point(561, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 67);
            this.panel2.TabIndex = 77;
            this.panel2.Visible = false;
            // 
            // chkCanEdit
            // 
            this.chkCanEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCanEdit.AutoSize = true;
            this.chkCanEdit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCanEdit.Location = new System.Drawing.Point(23, 3);
            this.chkCanEdit.Name = "chkCanEdit";
            this.chkCanEdit.Size = new System.Drawing.Size(229, 21);
            this.chkCanEdit.TabIndex = 4;
            this.chkCanEdit.Text = "فعال کردن حالت «امکان تغییر» در جدول";
            this.chkCanEdit.UseVisualStyleBackColor = true;
            this.chkCanEdit.CheckedChanged += new System.EventHandler(this.ChkCanEdit_CheckedChanged);
            // 
            // chkShowUpdateMessage
            // 
            this.chkShowUpdateMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowUpdateMessage.AutoSize = true;
            this.chkShowUpdateMessage.Checked = true;
            this.chkShowUpdateMessage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowUpdateMessage.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowUpdateMessage.Location = new System.Drawing.Point(70, 29);
            this.chkShowUpdateMessage.Name = "chkShowUpdateMessage";
            this.chkShowUpdateMessage.Size = new System.Drawing.Size(183, 19);
            this.chkShowUpdateMessage.TabIndex = 4;
            this.chkShowUpdateMessage.Text = "نمایش پیغام اخطار قبل از ثبت تغییرات";
            this.chkShowUpdateMessage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnShowAll);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtST_Name);
            this.groupBox1.Controls.Add(this.txtST_Phone);
            this.groupBox1.Controls.Add(this.txtST_Address);
            this.groupBox1.Controls.Add(this.cmbST_Name);
            this.groupBox1.Controls.Add(this.cmbST_Phone);
            this.groupBox1.Controls.Add(this.cmbST_Address);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(561, 161);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 161);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجو";
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Location = new System.Drawing.Point(168, 129);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(79, 26);
            this.btnShowAll.TabIndex = 57;
            this.btnShowAll.Text = "نمایش همه";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(6, 129);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 26);
            this.btnSearch.TabIndex = 56;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtST_Name
            // 
            this.txtST_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_Name.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_Name.Location = new System.Drawing.Point(9, 31);
            this.txtST_Name.Name = "txtST_Name";
            this.txtST_Name.Size = new System.Drawing.Size(93, 22);
            this.txtST_Name.TabIndex = 34;
            this.txtST_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_Name.Enter += new System.EventHandler(this.TxtST_Enter);
            this.txtST_Name.Leave += new System.EventHandler(this.TxtST_Leave);
            // 
            // txtST_Phone
            // 
            this.txtST_Phone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_Phone.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_Phone.Location = new System.Drawing.Point(9, 59);
            this.txtST_Phone.Name = "txtST_Phone";
            this.txtST_Phone.Size = new System.Drawing.Size(93, 22);
            this.txtST_Phone.TabIndex = 34;
            this.txtST_Phone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_Phone.Enter += new System.EventHandler(this.TxtST_Enter);
            this.txtST_Phone.Leave += new System.EventHandler(this.TxtST_Leave);
            // 
            // txtST_Address
            // 
            this.txtST_Address.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_Address.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_Address.Location = new System.Drawing.Point(9, 89);
            this.txtST_Address.Name = "txtST_Address";
            this.txtST_Address.Size = new System.Drawing.Size(93, 22);
            this.txtST_Address.TabIndex = 34;
            this.txtST_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_Address.Enter += new System.EventHandler(this.TxtST_Enter);
            this.txtST_Address.Leave += new System.EventHandler(this.TxtST_Leave);
            // 
            // cmbST_Name
            // 
            this.cmbST_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_Name.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_Name.FormattingEnabled = true;
            this.cmbST_Name.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_Name.Location = new System.Drawing.Point(108, 29);
            this.cmbST_Name.Name = "cmbST_Name";
            this.cmbST_Name.Size = new System.Drawing.Size(91, 23);
            this.cmbST_Name.TabIndex = 30;
            // 
            // cmbST_Phone
            // 
            this.cmbST_Phone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_Phone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_Phone.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_Phone.FormattingEnabled = true;
            this.cmbST_Phone.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_Phone.Location = new System.Drawing.Point(108, 57);
            this.cmbST_Phone.Name = "cmbST_Phone";
            this.cmbST_Phone.Size = new System.Drawing.Size(91, 23);
            this.cmbST_Phone.TabIndex = 30;
            // 
            // cmbST_Address
            // 
            this.cmbST_Address.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_Address.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_Address.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_Address.FormattingEnabled = true;
            this.cmbST_Address.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_Address.Location = new System.Drawing.Point(108, 87);
            this.cmbST_Address.Name = "cmbST_Address";
            this.cmbST_Address.Size = new System.Drawing.Size(91, 23);
            this.cmbST_Address.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(199, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "نام انبار :";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(199, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 15);
            this.label6.TabIndex = 29;
            this.label6.Text = "تلفن ثابت :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(199, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "آدرس :";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnAddNew.Location = new System.Drawing.Point(391, 300);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(158, 29);
            this.btnAddNew.TabIndex = 1;
            this.btnAddNew.Text = "اضافه کردن انبار جدید";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Visible = false;
            this.btnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(134, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(411, 18);
            this.label4.TabIndex = 75;
            this.label4.Text = "* برای مشاهده اطلاعات بیشتر درباره هر انبار، بر روی ردیف مورد نظر کلیک راست نمایی" +
    "د";
            // 
            // btnImportDataFromExcel
            // 
            this.btnImportDataFromExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportDataFromExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImportDataFromExcel.BackgroundImage")));
            this.btnImportDataFromExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnImportDataFromExcel.Location = new System.Drawing.Point(195, 297);
            this.btnImportDataFromExcel.Name = "btnImportDataFromExcel";
            this.btnImportDataFromExcel.Size = new System.Drawing.Size(34, 34);
            this.btnImportDataFromExcel.TabIndex = 72;
            this.btnImportDataFromExcel.UseVisualStyleBackColor = true;
            this.btnImportDataFromExcel.Visible = false;
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Location = new System.Drawing.Point(3, 300);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 29);
            this.btnReturn.TabIndex = 2;
            this.btnReturn.Text = "بازگشت";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAll.Location = new System.Drawing.Point(235, 300);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 29);
            this.btnDeleteAll.TabIndex = 2;
            this.btnDeleteAll.Text = "حذف همه";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Visible = false;
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(2, 27);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.Size = new System.Drawing.Size(547, 267);
            this.dgvData.TabIndex = 0;
            this.dgvData.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvData_CellBeginEdit);
            this.dgvData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellEndEdit);
            this.dgvData.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseDown);
            this.dgvData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DgvData_DataError);
            this.dgvData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(100, 26);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(99, 22);
            this.tsmiDelete.Text = "حذف";
            this.tsmiDelete.Visible = false;
            this.tsmiDelete.Click += new System.EventHandler(this.TsmiDelete_Click);
            // 
            // M1100_Warehouses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnReturn;
            this.ClientSize = new System.Drawing.Size(821, 336);
            this.Controls.Add(this.panel1);
            this.Name = "M1100_Warehouses";
            this.Text = "   انبارها";
            this.Shown += new System.EventHandler(this.M1100_Warehouses_Shown);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkCanEdit;
        private System.Windows.Forms.CheckBox chkShowUpdateMessage;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtST_Name;
        private System.Windows.Forms.TextBox txtST_Phone;
        private System.Windows.Forms.TextBox txtST_Address;
        private System.Windows.Forms.ComboBox cmbST_Name;
        private System.Windows.Forms.ComboBox cmbST_Phone;
        private System.Windows.Forms.ComboBox cmbST_Address;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnImportDataFromExcel;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RadioButton radEnabledLevel;
        private System.Windows.Forms.RadioButton radDisabledLevel;
    }
}