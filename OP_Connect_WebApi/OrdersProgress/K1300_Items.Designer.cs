﻿namespace OrdersProgress
{
    partial class K1300_Items
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(K1300_Items));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiItem_Details = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdd_ChangeItemImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteItemImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiItemStructure = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiItem_Warehouse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnImportDataFromExcel = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbST_FullName = new System.Windows.Forms.ComboBox();
            this.cmbST_Name = new System.Windows.Forms.ComboBox();
            this.cmbST_FullCode = new System.Windows.Forms.ComboBox();
            this.cmbST_SmallCode = new System.Windows.Forms.ComboBox();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbWarehouses = new System.Windows.Forms.ComboBox();
            this.cmbCategories = new System.Windows.Forms.ComboBox();
            this.cmbItemsEnable = new System.Windows.Forms.ComboBox();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radNotModule = new System.Windows.Forms.RadioButton();
            this.radModule = new System.Windows.Forms.RadioButton();
            this.txtST_FullName = new System.Windows.Forms.TextBox();
            this.txtST_Name = new System.Windows.Forms.TextBox();
            this.txtST_FullCode = new System.Windows.Forms.TextBox();
            this.txtST_SmallCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkCanEdit = new System.Windows.Forms.CheckBox();
            this.chkShowUpdateMessage = new System.Windows.Forms.CheckBox();
            this.btnGetImages = new System.Windows.Forms.Button();
            this.btnDeleteAllImages = new System.Windows.Forms.Button();
            this.btnDeleteAllItems = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiItem_Details,
            this.tsmiImage,
            this.tsmiDiagram,
            this.tsmiItemStructure,
            this.tsmiProperties,
            this.tsmiItem_Warehouse,
            this.tsmiDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 158);
            // 
            // tsmiItem_Details
            // 
            this.tsmiItem_Details.Name = "tsmiItem_Details";
            this.tsmiItem_Details.Size = new System.Drawing.Size(154, 22);
            this.tsmiItem_Details.Text = "جزییات";
            this.tsmiItem_Details.Click += new System.EventHandler(this.TsmiItem_Details_Click);
            // 
            // tsmiImage
            // 
            this.tsmiImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd_ChangeItemImage,
            this.tsmiDeleteItemImage});
            this.tsmiImage.Name = "tsmiImage";
            this.tsmiImage.Size = new System.Drawing.Size(154, 22);
            this.tsmiImage.Text = "تصویر کالا";
            // 
            // tsmiAdd_ChangeItemImage
            // 
            this.tsmiAdd_ChangeItemImage.Name = "tsmiAdd_ChangeItemImage";
            this.tsmiAdd_ChangeItemImage.Size = new System.Drawing.Size(181, 22);
            this.tsmiAdd_ChangeItemImage.Text = "افزودن یا ویرایش تصویر";
            this.tsmiAdd_ChangeItemImage.Click += new System.EventHandler(this.TsmiAdd_ChangeItemImage_Click);
            // 
            // tsmiDeleteItemImage
            // 
            this.tsmiDeleteItemImage.Name = "tsmiDeleteItemImage";
            this.tsmiDeleteItemImage.Size = new System.Drawing.Size(181, 22);
            this.tsmiDeleteItemImage.Text = "حذف تصویر";
            this.tsmiDeleteItemImage.Click += new System.EventHandler(this.TsmiDeleteItemImage_Click);
            // 
            // tsmiDiagram
            // 
            this.tsmiDiagram.Name = "tsmiDiagram";
            this.tsmiDiagram.Size = new System.Drawing.Size(154, 22);
            this.tsmiDiagram.Text = "دیاگرام زیرساخت";
            this.tsmiDiagram.Visible = false;
            this.tsmiDiagram.Click += new System.EventHandler(this.TsmiDiagram_Click);
            // 
            // tsmiItemStructure
            // 
            this.tsmiItemStructure.Name = "tsmiItemStructure";
            this.tsmiItemStructure.Size = new System.Drawing.Size(154, 22);
            this.tsmiItemStructure.Text = "ساختار کالا";
            this.tsmiItemStructure.Click += new System.EventHandler(this.tsmiItemStructure_Click);
            // 
            // tsmiProperties
            // 
            this.tsmiProperties.Name = "tsmiProperties";
            this.tsmiProperties.Size = new System.Drawing.Size(154, 22);
            this.tsmiProperties.Text = "مشخصه ها";
            this.tsmiProperties.Click += new System.EventHandler(this.TsmiProperties_Click);
            // 
            // tsmiItem_Warehouse
            // 
            this.tsmiItem_Warehouse.Name = "tsmiItem_Warehouse";
            this.tsmiItem_Warehouse.Size = new System.Drawing.Size(154, 22);
            this.tsmiItem_Warehouse.Text = "تعیین انبار";
            this.tsmiItem_Warehouse.Visible = false;
            this.tsmiItem_Warehouse.Click += new System.EventHandler(this.TsmiItem_Warehouse_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(154, 22);
            this.tsmiDelete.Text = "حذف";
            this.tsmiDelete.Visible = false;
            this.tsmiDelete.Click += new System.EventHandler(this.TsmiDelete_Click);
            // 
            // btnImportDataFromExcel
            // 
            this.btnImportDataFromExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportDataFromExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImportDataFromExcel.BackgroundImage")));
            this.btnImportDataFromExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnImportDataFromExcel.Location = new System.Drawing.Point(3, 447);
            this.btnImportDataFromExcel.Name = "btnImportDataFromExcel";
            this.btnImportDataFromExcel.Size = new System.Drawing.Size(34, 34);
            this.btnImportDataFromExcel.TabIndex = 80;
            this.toolTip1.SetToolTip(this.btnImportDataFromExcel, "دریافت اطلاعات از فایل اکسل");
            this.btnImportDataFromExcel.UseVisualStyleBackColor = true;
            this.btnImportDataFromExcel.Visible = false;
            this.btnImportDataFromExcel.Click += new System.EventHandler(this.BtnImportDataFromExcel_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Location = new System.Drawing.Point(3, 486);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 29);
            this.btnReturn.TabIndex = 80;
            this.btnReturn.Text = "بازگشت";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(399, 237);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnImportDataFromExcel);
            this.panel1.Controls.Add(this.btnGetImages);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnDeleteAllImages);
            this.panel1.Controls.Add(this.btnDeleteAllItems);
            this.panel1.Controls.Add(this.btnAddNew);
            this.panel1.Controls.Add(this.dgvData);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(884, 521);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(234, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(423, 18);
            this.label4.TabIndex = 93;
            this.label4.Text = " * برای مشاهده جزییات بیشتر بر روی ردیف مورد نظر کلیک راست نمایید";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.cmbST_FullName);
            this.groupBox1.Controls.Add(this.cmbST_Name);
            this.groupBox1.Controls.Add(this.cmbST_FullCode);
            this.groupBox1.Controls.Add(this.cmbST_SmallCode);
            this.groupBox1.Controls.Add(this.btnShowAll);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.cmbWarehouses);
            this.groupBox1.Controls.Add(this.cmbCategories);
            this.groupBox1.Controls.Add(this.cmbItemsEnable);
            this.groupBox1.Controls.Add(this.radAll);
            this.groupBox1.Controls.Add(this.radNotModule);
            this.groupBox1.Controls.Add(this.radModule);
            this.groupBox1.Controls.Add(this.txtST_FullName);
            this.groupBox1.Controls.Add(this.txtST_Name);
            this.groupBox1.Controls.Add(this.txtST_FullCode);
            this.groupBox1.Controls.Add(this.txtST_SmallCode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(665, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 441);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجو";
            this.groupBox1.Enter += new System.EventHandler(this.GroupBox1_Enter);
            this.groupBox1.Leave += new System.EventHandler(this.GroupBox1_Leave);
            // 
            // cmbST_FullName
            // 
            this.cmbST_FullName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_FullName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_FullName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_FullName.FormattingEnabled = true;
            this.cmbST_FullName.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_FullName.Location = new System.Drawing.Point(76, 349);
            this.cmbST_FullName.Name = "cmbST_FullName";
            this.cmbST_FullName.Size = new System.Drawing.Size(91, 23);
            this.cmbST_FullName.TabIndex = 40;
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
            this.cmbST_Name.Location = new System.Drawing.Point(95, 235);
            this.cmbST_Name.Name = "cmbST_Name";
            this.cmbST_Name.Size = new System.Drawing.Size(91, 23);
            this.cmbST_Name.TabIndex = 40;
            // 
            // cmbST_FullCode
            // 
            this.cmbST_FullCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_FullCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_FullCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_FullCode.FormattingEnabled = true;
            this.cmbST_FullCode.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_FullCode.Location = new System.Drawing.Point(76, 283);
            this.cmbST_FullCode.Name = "cmbST_FullCode";
            this.cmbST_FullCode.Size = new System.Drawing.Size(91, 23);
            this.cmbST_FullCode.TabIndex = 30;
            // 
            // cmbST_SmallCode
            // 
            this.cmbST_SmallCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_SmallCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_SmallCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_SmallCode.FormattingEnabled = true;
            this.cmbST_SmallCode.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_SmallCode.Location = new System.Drawing.Point(95, 206);
            this.cmbST_SmallCode.Name = "cmbST_SmallCode";
            this.cmbST_SmallCode.Size = new System.Drawing.Size(91, 23);
            this.cmbST_SmallCode.TabIndex = 30;
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowAll.Location = new System.Drawing.Point(130, 411);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(79, 26);
            this.btnShowAll.TabIndex = 56;
            this.btnShowAll.Text = "نمایش همه";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(6, 411);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 26);
            this.btnSearch.TabIndex = 50;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // cmbWarehouses
            // 
            this.cmbWarehouses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWarehouses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarehouses.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWarehouses.FormattingEnabled = true;
            this.cmbWarehouses.Location = new System.Drawing.Point(27, 29);
            this.cmbWarehouses.Name = "cmbWarehouses";
            this.cmbWarehouses.Size = new System.Drawing.Size(182, 25);
            this.cmbWarehouses.TabIndex = 15;
            this.cmbWarehouses.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // cmbCategories
            // 
            this.cmbCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategories.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategories.FormattingEnabled = true;
            this.cmbCategories.Location = new System.Drawing.Point(27, 92);
            this.cmbCategories.Name = "cmbCategories";
            this.cmbCategories.Size = new System.Drawing.Size(182, 25);
            this.cmbCategories.TabIndex = 15;
            this.cmbCategories.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // cmbItemsEnable
            // 
            this.cmbItemsEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbItemsEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemsEnable.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbItemsEnable.FormattingEnabled = true;
            this.cmbItemsEnable.Items.AddRange(new object[] {
            "نمایش موارد فعال",
            "نمایش موارد غیرفعال",
            "نمایش همه موارد"});
            this.cmbItemsEnable.Location = new System.Drawing.Point(27, 61);
            this.cmbItemsEnable.Name = "cmbItemsEnable";
            this.cmbItemsEnable.Size = new System.Drawing.Size(182, 25);
            this.cmbItemsEnable.TabIndex = 15;
            this.cmbItemsEnable.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // radAll
            // 
            this.radAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radAll.AutoSize = true;
            this.radAll.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAll.Location = new System.Drawing.Point(73, 176);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(132, 19);
            this.radAll.TabIndex = 26;
            this.radAll.Text = "نمایش ماژول ها و قطعات";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.Visible = false;
            this.radAll.CheckedChanged += new System.EventHandler(this.RadModule_CheckedChanged);
            // 
            // radNotModule
            // 
            this.radNotModule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radNotModule.AutoSize = true;
            this.radNotModule.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radNotModule.Location = new System.Drawing.Point(124, 152);
            this.radNotModule.Name = "radNotModule";
            this.radNotModule.Size = new System.Drawing.Size(81, 19);
            this.radNotModule.TabIndex = 24;
            this.radNotModule.Text = "نمایش قطعات";
            this.radNotModule.UseVisualStyleBackColor = true;
            this.radNotModule.CheckedChanged += new System.EventHandler(this.RadModule_CheckedChanged);
            // 
            // radModule
            // 
            this.radModule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radModule.AutoSize = true;
            this.radModule.Checked = true;
            this.radModule.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radModule.Location = new System.Drawing.Point(114, 127);
            this.radModule.Name = "radModule";
            this.radModule.Size = new System.Drawing.Size(92, 19);
            this.radModule.TabIndex = 22;
            this.radModule.TabStop = true;
            this.radModule.Text = "نمایش ماژول ها";
            this.radModule.UseVisualStyleBackColor = true;
            this.radModule.CheckedChanged += new System.EventHandler(this.RadModule_CheckedChanged);
            // 
            // txtST_FullName
            // 
            this.txtST_FullName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_FullName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_FullName.Location = new System.Drawing.Point(7, 378);
            this.txtST_FullName.Name = "txtST_FullName";
            this.txtST_FullName.Size = new System.Drawing.Size(160, 22);
            this.txtST_FullName.TabIndex = 45;
            this.txtST_FullName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_FullName.Enter += new System.EventHandler(this.TxtST_SmallCode_Enter);
            this.txtST_FullName.Leave += new System.EventHandler(this.TxtST_SmallCode_Leave);
            // 
            // txtST_Name
            // 
            this.txtST_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_Name.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_Name.Location = new System.Drawing.Point(7, 237);
            this.txtST_Name.Name = "txtST_Name";
            this.txtST_Name.Size = new System.Drawing.Size(82, 22);
            this.txtST_Name.TabIndex = 45;
            this.txtST_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_Name.Enter += new System.EventHandler(this.TxtST_SmallCode_Enter);
            this.txtST_Name.Leave += new System.EventHandler(this.TxtST_SmallCode_Leave);
            // 
            // txtST_FullCode
            // 
            this.txtST_FullCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_FullCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_FullCode.Location = new System.Drawing.Point(7, 312);
            this.txtST_FullCode.Name = "txtST_FullCode";
            this.txtST_FullCode.Size = new System.Drawing.Size(160, 22);
            this.txtST_FullCode.TabIndex = 34;
            this.txtST_FullCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_FullCode.Enter += new System.EventHandler(this.TxtST_SmallCode_Enter);
            this.txtST_FullCode.Leave += new System.EventHandler(this.TxtST_SmallCode_Leave);
            // 
            // txtST_SmallCode
            // 
            this.txtST_SmallCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_SmallCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_SmallCode.Location = new System.Drawing.Point(7, 208);
            this.txtST_SmallCode.Name = "txtST_SmallCode";
            this.txtST_SmallCode.Size = new System.Drawing.Size(82, 22);
            this.txtST_SmallCode.TabIndex = 34;
            this.txtST_SmallCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_SmallCode.Enter += new System.EventHandler(this.TxtST_SmallCode_Enter);
            this.txtST_SmallCode.Leave += new System.EventHandler(this.TxtST_SmallCode_Leave);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(166, 352);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 29;
            this.label6.Text = "نام کامل :";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(186, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "نام :";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(167, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "کد کامل :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(186, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "کد :";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Location = new System.Drawing.Point(3, 33);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(154, 223);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 91;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(424, 221);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.chkCanEdit);
            this.panel2.Controls.Add(this.chkShowUpdateMessage);
            this.panel2.Location = new System.Drawing.Point(9, 327);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(145, 62);
            this.panel2.TabIndex = 1;
            this.panel2.Visible = false;
            // 
            // chkCanEdit
            // 
            this.chkCanEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCanEdit.AutoSize = true;
            this.chkCanEdit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCanEdit.Location = new System.Drawing.Point(-89, 7);
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
            this.chkShowUpdateMessage.Enabled = false;
            this.chkShowUpdateMessage.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowUpdateMessage.Location = new System.Drawing.Point(-43, 32);
            this.chkShowUpdateMessage.Name = "chkShowUpdateMessage";
            this.chkShowUpdateMessage.Size = new System.Drawing.Size(183, 19);
            this.chkShowUpdateMessage.TabIndex = 10;
            this.chkShowUpdateMessage.Text = "نمایش پیغام اخطار قبل از ثبت تغییرات";
            this.chkShowUpdateMessage.UseVisualStyleBackColor = true;
            // 
            // btnGetImages
            // 
            this.btnGetImages.Location = new System.Drawing.Point(3, 262);
            this.btnGetImages.Name = "btnGetImages";
            this.btnGetImages.Size = new System.Drawing.Size(154, 29);
            this.btnGetImages.TabIndex = 80;
            this.btnGetImages.Text = "دریافت تصاویر";
            this.btnGetImages.UseVisualStyleBackColor = true;
            this.btnGetImages.Visible = false;
            this.btnGetImages.Click += new System.EventHandler(this.BtnGetImages_Click);
            // 
            // btnDeleteAllImages
            // 
            this.btnDeleteAllImages.Location = new System.Drawing.Point(3, 295);
            this.btnDeleteAllImages.Name = "btnDeleteAllImages";
            this.btnDeleteAllImages.Size = new System.Drawing.Size(154, 25);
            this.btnDeleteAllImages.TabIndex = 75;
            this.btnDeleteAllImages.Text = "حذف همه تصویرها";
            this.btnDeleteAllImages.UseVisualStyleBackColor = true;
            this.btnDeleteAllImages.Visible = false;
            this.btnDeleteAllImages.Click += new System.EventHandler(this.btnDeleteAllImages_Click);
            // 
            // btnDeleteAllItems
            // 
            this.btnDeleteAllItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteAllItems.Location = new System.Drawing.Point(84, 486);
            this.btnDeleteAllItems.Name = "btnDeleteAllItems";
            this.btnDeleteAllItems.Size = new System.Drawing.Size(75, 29);
            this.btnDeleteAllItems.TabIndex = 75;
            this.btnDeleteAllItems.Text = "حذف همه";
            this.btnDeleteAllItems.UseVisualStyleBackColor = true;
            this.btnDeleteAllItems.Visible = false;
            this.btnDeleteAllItems.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnAddNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNew.ForeColor = System.Drawing.SystemColors.Window;
            this.btnAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddNew.Location = new System.Drawing.Point(664, 484);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(217, 32);
            this.btnAddNew.TabIndex = 70;
            this.btnAddNew.Text = "تعریف کالای جدید";
            this.btnAddNew.UseVisualStyleBackColor = false;
            this.btnAddNew.Visible = false;
            this.btnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(165, 33);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(494, 485);
            this.dgvData.TabIndex = 90;
            this.dgvData.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvData_CellBeginEdit);
            this.dgvData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellDoubleClick);
            this.dgvData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellEndEdit);
            this.dgvData.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellEnter);
            this.dgvData.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseDown);
            this.dgvData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DgvData_DataError);
            this.dgvData.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_RowEnter);
            this.dgvData.Enter += new System.EventHandler(this.DgvData_Enter);
            this.dgvData.Leave += new System.EventHandler(this.DgvData_Leave);
            this.dgvData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Interval = 600;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Image Files(*.BMP;*.JPG)|*.BMP;*.JPG";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(426, 234);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(46, 44);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 95;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            // 
            // K1300_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnReturn;
            this.ClientSize = new System.Drawing.Size(883, 520);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.panel1);
            this.Name = "K1300_Items";
            this.Text = "   کالاها";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.K1300_Items_Load);
            this.Shown += new System.EventHandler(this.K1300_Items_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnDeleteAllItems;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.Button btnImportDataFromExcel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkCanEdit;
        private System.Windows.Forms.ComboBox cmbItemsEnable;
        private System.Windows.Forms.CheckBox chkShowUpdateMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtST_SmallCode;
        private System.Windows.Forms.ComboBox cmbST_SmallCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RadioButton radNotModule;
        private System.Windows.Forms.RadioButton radModule;
        private System.Windows.Forms.TextBox txtST_Name;
        private System.Windows.Forms.ComboBox cmbST_Name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolStripMenuItem tsmiItemStructure;
        private System.Windows.Forms.ToolStripMenuItem tsmiProperties;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiItem_Warehouse;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDiagram;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripMenuItem tsmiImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd_ChangeItemImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteItemImage;
        private System.Windows.Forms.Button btnGetImages;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDeleteAllImages;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiItem_Details;
        private System.Windows.Forms.TextBox txtST_FullName;
        private System.Windows.Forms.TextBox txtST_FullCode;
        private System.Windows.Forms.ComboBox cmbST_FullName;
        private System.Windows.Forms.ComboBox cmbST_FullCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbWarehouses;
        private System.Windows.Forms.ComboBox cmbCategories;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}