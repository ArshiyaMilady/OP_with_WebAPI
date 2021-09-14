using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXCEL = Microsoft.Office.Interop.Excel;


namespace OrdersProgress
{
    public partial class K1300_Items : X210_ExampleForm_Normal
    {
        List<Models.Item> lstItems = new List<Models.Item>();
        List<Models.Warehouse> lstWarehousess = new List<Models.Warehouse>();
        List<Models.Category> lstCats = new List<Models.Category>();
        bool bAnySearch = false; // آیا جستجویی انجام شده است؟

        public K1300_Items()
        {
            InitializeComponent();

            pictureBox3.Visible = Stack.Use_Web;

            tsmiDelete.Visible = (Stack.UserLevel_Type == 1) || (Stack.UserLevel_Type == 2);
            btnDeleteAllItems.Visible = Stack.UserLevel_Type == 1;
            btnDeleteAllImages.Visible = Stack.UserLevel_Type == 1;

            //panel2.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn2110"); // امکان تغییر
            btnGetImages.Visible =(Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jn2110"); // امکان افزودن تصویر
            btnAddNew.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jn2120"); // امکان افزودن
            btnImportDataFromExcel.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jn2130"); // امکان  ورود اطلاعات از اکسل
        }

        private void K1300_Items_Load(object sender, EventArgs e)
        {

        }

        private async void K1300_Items_Shown(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            //Application.DoEvents();

            cmbST_Name.SelectedIndex = 0;
            cmbST_SmallCode.SelectedIndex = 0;
            //dataGridView1.DataSource = Program.dbOperations.GetAllModulesAsync();
            //dgvData.DataSource = Program.dbOperations.GetAllItemsAsync(Stack.Company_Id);
            cmbItemsEnable.SelectedIndex = 0;

            #region تهیه فهرست انبارها در کامبوباکس مربوطه
            cmbWarehouses.Items.Add("تمام انبارها");
            if (Stack.Use_Web)
            {
                try
                {
                    lstWarehousess = await HttpClientExtensions.GetT<List<Models.Warehouse>>
                        (Stack.API_Uri_start_read + "/Warehouses?all=no&company_Id=" + Stack.Company_Id,Stack.token);
                }
                catch { }
            }
            else
            {
                lstWarehousess = Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Id, true);
            }

            cmbWarehouses.Items.AddRange(lstWarehousess.Select(d => d.Name).ToArray());
            if(cmbWarehouses.Items.Count>0)
                cmbWarehouses.SelectedIndex = 0;
            #endregion

            #region تهیه فهرست دسته ها در کامبوباکس مربوطه
            cmbCategories.Items.Add("تمام دسته ها");
            if (Stack.Use_Web)
            {
                //try
                {
                    lstCats = await HttpClientExtensions.GetT<List<Models.Category>>
                        (Stack.API_Uri_start_read + "/Categories?all=no&company_Id=" + Stack.Company_Id,Stack.token);
                }
                //catch { }
            }
            else
            {
                lstCats = Program.dbOperations.GetAllCategoriesAsync(Stack.Company_Id);
            }

            cmbCategories.Items.AddRange(lstCats.Select(d => d.Name).ToArray());
            if (cmbCategories.Items.Count>0)
                cmbCategories.SelectedIndex = 0;
            #endregion

            await GetData();
            dgvData.DataSource = lstItems;
            ShowData();

            Application.DoEvents();
            panel1.Visible = true;
            progressBar1.Visible = false;
            pictureBox3.Visible = false;
        }

        private async Task<List<Models.Item>> GetData(bool bForceReset = false)
        {
            if (!lstItems.Any() || bForceReset)
            {
                if (Stack.Use_Web)
                    lstItems = await HttpClientExtensions.GetT<List<Models.Item>>
                        (Stack.API_Uri_start_read + "/Items?all=no&company_Id="+ Stack.Company_Id, Stack.token);
                else lstItems = Program.dbOperations.GetAllItemsAsync(Stack.Company_Id,0,100);

                lstItems = lstItems.OrderBy(d => d.Code_Small).ToList();
            }

            //switch (cmbItemsEnable.SelectedIndex)
            //{
            //    case 0: return l
            //    case 1: enableType = -1; break;
            //    case 2: enableType = 0; break;
            //}

            //if (radModule.Checked) lstItems = lstItems.Where(d => d.Module).ToList();
            //else if (radNotModule.Checked) lstItems = lstItems.Where(d => !d.Module).ToList();
            ////else return Program.dbOperations.GetAllItemsAsync(Stack.Company_Id, enableType);

            //if(cmbWarehouses.SelectedIndex>0)
            //{
            //    long wh_index = Program.dbOperations.GetWarehouseAsync(Stack.Company_Id, cmbWarehouses.Text).Id;
            //    lstItems = lstItems.Where(d => d.Warehouse_Id == wh_index).ToList();
            //}

            //if(cmbCategories.SelectedIndex>0)
            //{
            //    long cat_index = Program.dbOperations.GetCategoryAsync(cmbCategories.Text, Stack.Company_Id).Id;
            //    lstItems = lstItems.Where(d => d.Category_Id == cat_index).ToList();
            //}

            return lstItems;
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            //if (ChangeHeaderTexts)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    switch (col.Name)
                    {
                        //case "Index":
                        //    col.HeaderText = "index";
                        //    break;
                        case "Enable":
                            col.HeaderText = "فعال؟";
                            col.Width = 50;
                            break;
                        case "Code_Small":
                            col.HeaderText = "کد";
                            col.Width = 100;
                            break;
                        case "Name_Samll":
                            col.HeaderText = "نام کالا";
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            col.Width = 200;
                            break;
                        case "Salable":
                            col.HeaderText = "قابل فروش؟";
                            col.Width = 100;
                            break;
                        case "Weight":
                            col.HeaderText = "وزن(کیلوگرم)";
                            col.Width = 100;
                            break;
                        case "FixedPrice":
                            col.HeaderText = "بهای تمام شده (ریال)";
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            col.Width = 100;
                            break;
                        case "SalesPrice":
                            col.HeaderText = "قیمت فروش (ریال)";
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            col.Width = 100;
                            break;
                        case "Code_Full":
                            col.HeaderText = "کد کامل";
                            col.Width = 100;
                            break;
                        case "Name_Full":
                            col.HeaderText = "نام کامل";
                            col.Width = 100;
                            break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        int iNewRow = -1;
        private async void BtnAddNew_Click(object sender, EventArgs e)
        {
            new K1302_Item_Details(2).ShowDialog();

            if (Stack.bx)
            {
                await GetData(true);
                dgvData.DataSource = lstItems;
                radNotModule.Checked = true;
                if(bAnySearch)
                    BtnSearch_Click(null, null);
                DataGridViewRow row = null;
                if ((row = dgvData.Rows.Cast<DataGridViewRow>().ToList().FirstOrDefault
                    (d => d.Cells["Id"].Value.ToString().Equals(Stack.lx.ToString()))) != null)
                {
                    dgvData.CurrentCell = row.Cells["Name_Samll"];
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (Stack.Use_Web)
            {
                MessageBox.Show("این امکان فعال نمی باشد");
                return;
            }
            else
            {
                if (MessageBox.Show("آیا از حذف همه کالاها اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

                if (MessageBox.Show("با انجام این عمل ، تمام روابط کالاها و جداول دیگر از بین خواهد رفت"
                    + "\n" + "آیا از حذف تمام کالا اطمینان دارید؟", "اخطار 2"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                Program.dbOperations.DeleteAllModulesAsync();
                Program.dbOperations.DeleteAllItemsAsync();
                dgvData.DataSource = Program.dbOperations.GetAllItemsAsync(Stack.Company_Id);
            }
        }

        private async void TsmiDelete_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            Models.Item item = lstItems.First(d => d.Id == id);// Program.dbOperations.GetItem(id);
            if (MessageBox.Show("آیا از حذف این مورد اطمینان دارید؟", item.Code_Small, MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            if (MessageBox.Show("با انجام این عمل تمام رابطه ها ی این کالا با قسمتهای مختلف از بین خواهد رفت."
                + "آیا از حذف این مورد اطمینان دارید؟", item.Code_Small, MessageBoxButtons.YesNo)
                    == DialogResult.No) return;

            try
            {
                //string sCode_Small = Convert.ToString(dgvData.CurrentRow.Cells["Code_Small"].Value);
                //Models.Item item = Program.dbOperations.GetItemAsync(Stack.Company_Id,sCode_Small);
                int row_index = dgvData.CurrentRow.Index;

                #region اگر کالا، یک ماژول یاشد، باید تمام رابطه های آن با زیرساختهایش حذف شود
                if (item.Module)
                {
                    // تمام کالاهای زیر ساخت ماژول باید حذف شوند
                    List<Models.Module> modules = new List<Models.Module>();
                    if (Stack.Use_Web)
                        modules = await HttpClientExtensions.GetT<List<Models.Module>>
                            (Stack.API_Uri_start_read + "/Modules?all=no&company_Id=" + Stack.Company_Id
                            + "&EnableType=0&ModuleSmallCode=" + item.Code_Small, Stack.token);
                    else modules = Program.dbOperations.GetAllModulesAsync(Stack.Company_Id, 0, item.Code_Small);

                    if (modules.Any())
                    {
                        //MessageBox.Show(item.Code_Small);
                        //List<Models.Module> lst = Program.dbOperations.GetAllModulesAsync(Stack.Company_Id,0, item.Code_Small);
                        if (Stack.Use_Web)
                            foreach (var m in modules)
                                await HttpClientExtensions.DeleteAsJsonAsync2
                                    (Stack.API_Uri_start_read + "/Modules/" + m.Id,Stack.token);
                        else
                            foreach (var m in modules) Program.dbOperations.DeleteModule(m);
                    }
                }
                #endregion

                if (Stack.Use_Web)
                    await HttpClientExtensions.DeleteAsJsonAsync2(Stack.API_Uri_start_read
                        + "/Items/" + item.Id, Stack.token);
                else
                    Program.dbOperations.DeleteItemAsync(item);

                lstItems.Remove(item);

                BtnSearch_Click(null, null);
                //dgvData.DataSource = Program.dbOperations.GetAllItemsAsync(Stack.Company_Id);
                if (dgvData.Rows.Count > 0)
                {
                    if (row_index > 0) dgvData.CurrentCell = dgvData["Name_Samll", row_index - 1];
                    else if (row_index < dgvData.Rows.Count-1) dgvData.CurrentCell = dgvData["Name_Samll", row_index + 1];
                }
                //dataGridView1.DataSource =  Program.dbOperations.GetAllModulesAsync();
            }
            catch { MessageBox.Show("خطا در اجرای عملیات"); }

        }

        int iX = 0, iY = 0;
        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            iX = e.X;
            iY = e.Y;
        }

        private async void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
            long id = Convert.ToInt64(dgvData["Id", e.RowIndex].Value);
            string sc = Convert.ToString(dgvData["Code_Small", e.RowIndex].Value);
            //#region نمایش تصویر کالا در صورت وجود
            //Models.Item_File item_file = Program.dbOperations.GetItem_FileAsync(sc, 1,true);
            //if (item_file != null)
            //    pictureBox2.Image = new ThisProject().ByteToImage
            //        (Program.dbOperations.GetFileAsync(item_file.File_Index).Content);
            //else pictureBox2.Image = null;
            //#endregion

            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                Models.Item item2 = null;
                if (Stack.Use_Web)
                    item2 = await HttpClientExtensions.GetT<Models.Item>
                        (Stack.API_Uri_start_read + "/Items/" + id, Stack.token);
                else
                    item2 = Program.dbOperations.GetItemAsync(Stack.Company_Id,sc);

                if (item2 == null) return;
                //tsmiShowModuleItems.Visible = item2.Module;
                tsmiDiagram.Visible = item2.Module;
                //if (Program.dbOperations.GetWarehouse_InventoryAsync(sc) == null)
                //    tsmiItem_Warehouse.Visible = true;

                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        object InitailValue = null;
        private void DgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            InitailValue = dgvData[e.ColumnIndex, e.RowIndex].Value;//.ToString();
        }

        private async void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvData[e.ColumnIndex, e.RowIndex].Value == InitailValue) return;

            bool bSaveChange = true;   // آیا تغییر ذخیره شود؟
            panel1.Enabled = !Stack.Use_Web;
            pictureBox3.Visible = Stack.Use_Web;

            long id = Convert.ToInt64(dgvData["Id", e.RowIndex].Value);

            Models.Item item = lstItems.First(d => d.Id == id); // Program.dbOperations.GetItemAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Code_Small":
                    item.Code_Small = Convert.ToString(dgvData["Code_Small", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(item.Code_Small))
                        return;
                    #region اگر کالایی دیگر با این کد تعریف شده باشد
                    else if (lstItems.Where(d => d.Id != id)
                        .Any(j => j.Code_Small.ToLower().Equals(item.Code_Small.ToLower())))
                    {
                        bSaveChange = MessageBox.Show("کد قبلا استفاده شده است. آیا مایل به تعریف ورژن جدیدی از این کد می باشید؟"
                            + "\n" + "دقت نمایید با تأیید این عمل، کدهای قبلی غیر فعال شده، و تمام ارتباطات از این پس از ورژن جدید استفاده می شوند"
                            , "اخطار", MessageBoxButtons.YesNo) == DialogResult.Yes;
                        if (bSaveChange)
                        {
                            foreach (Models.Item it1 in lstItems.Where(d => d.Id != id)
                                .Where(j => j.Code_Small.ToLower().Equals(item.Code_Small.ToLower())).ToList())
                            {
                                it1.Enable = false;
                                if (Stack.Use_Web)
                                    await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read
                                        + "/Items/" + id, item, Stack.token);
                                else Program.dbOperations.UpdateItemAsync(it1);
                            }

                        }
                        //bSaveChange=false;
                        #endregion
                    }
                    break;
                case "Name_Samll":
                    item.Name_Samll = Convert.ToString(dgvData["Name_Samll", e.RowIndex].Value);
                    break;
                case "Code_Full":
                    item.Code_Full = Convert.ToString(dgvData["Code_Full", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(item.Code_Full))
                        return;
                    #region اگر کالایی دیگر با این کد تعریف شده باشد
                    else if (lstItems.Where(d => d.Id != id)
                        .Any(j => j.Code_Full.ToLower().Equals(item.Code_Full.ToLower())))
                    {
                        MessageBox.Show("کد قبلا استفاده شده است" , "خطا");
                        bSaveChange = false;
                    }
                    #endregion
                    break;
                case "Name_Full":
                    item.Name_Full = Convert.ToString(dgvData["Name_Full", e.RowIndex].Value);
                    break;
                case "Module":
                    bSaveChange = false;
                    //item.Module = Convert.ToBoolean(dgvData["Module", e.RowIndex].Value);
                    break;
                case "Salable":
                    item.Salable = Convert.ToBoolean(dgvData["Salable", e.RowIndex].Value);
                    break;
                case "Weight":
                    item.Weight = Convert.ToDouble(dgvData["Weight", e.RowIndex].Value);
                    break;
                case "FixedPrice":
                    item.FixedPrice = Convert.ToInt64(dgvData["FixedPrice", e.RowIndex].Value);
                    break;
                case "SalesPrice":
                    item.SalesPrice = Convert.ToInt64(dgvData["SalesPrice", e.RowIndex].Value);
                    break;
                case "Enable":
                    item.Enable = Convert.ToBoolean(dgvData["Enable", e.RowIndex].Value);

                    #region if 'item.Enable = false', remove this from <Module> table 
                    if (item.Enable)
                    {
                        if (lstItems.Where(d=>d.Enable).Where(j => j.Code_Small.ToLower().Equals(item.Code_Small.ToLower()))
                            .Any(d => d.Id != id))
                        {
                            MessageBox.Show("امکان فعال سازی این کالا وجود ندارد، زیرا ورژن دیگری از این کالا فعال است","خطا");
                            bSaveChange = false;
                        }
                    }
                    else
                    {
                        List<Models.Module> lstModules = new List<Models.Module>();
                        if (Stack.Use_Web)
                            try
                            {
                                lstModules = await HttpClientExtensions.GetT<List<Models.Module>>
                                    (Stack.API_Uri_start_read + "/Modules?all=no&company_Id=" + Stack.Company_Id
                                    + "&EnableType=1", Stack.token);
                            }
                            catch { }
                        lstModules = Program.dbOperations.GetAllModulesAsync(Stack.Company_Id, 1);
                        
                        bool b1 = lstModules.Any(d => d.Module_Code_Small.ToLower().Equals(item.Code_Small));
                        bool b2 = lstModules.Any(d => d.Item_Code_Small.ToLower().Equals(item.Code_Small));
                        if (b1 || b2)
                        {
                            if (MessageBox.Show("غیر فعال کردن این کالا باعث حذف رابطه های این کالا "
                                + "با سایر کالاها خواهد شد. آیا از انجام عمل اطمینان دارید؟"
                                , "اخطار", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                if (Stack.Use_Web)
                                {
                                    if (b1)
                                        foreach (Models.Module md in lstModules.Where(d => d.Module_Code_Small.ToLower().Equals(item.Code_Small)).ToList())
                                            await HttpClientExtensions.DeleteAsJsonAsync2(Stack.API_Uri_start_read
                                                + "/Modules/" + md.Id, Stack.token);
                                    if (b2)
                                        foreach (Models.Module md in lstModules.Where(d => d.Item_Code_Small.ToLower().Equals(item.Code_Small)).ToList())
                                            await HttpClientExtensions.DeleteAsJsonAsync2(Stack.API_Uri_start_read
                                                + "/Modules/" + md.Id, Stack.token);

                                    await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read
                                        + "/Items/" + item.Id, item, Stack.token);
                                }
                                else
                                {
                                    if (b1)
                                        foreach (Models.Module md in lstModules.Where(d => d.Module_Code_Small.ToLower().Equals(item.Code_Small)).ToList())
                                            Program.dbOperations.DeleteModule(md);
                                    if (b2)
                                        foreach (Models.Module md in lstModules.Where(d => d.Item_Code_Small.ToLower().Equals(item.Code_Small)).ToList())
                                            Program.dbOperations.DeleteModule(md);

                                    Program.dbOperations.UpdateItemAsync(item);
                                    //return;
                                }

                                lstItems.Remove(lstItems.First(d => d.Id == id));
                                lstItems.Add(item);

                                Application.DoEvents();
                                panel1.Enabled = true;
                                pictureBox3.Visible = false;
                            }

                            bSaveChange = false;
                        }
                    }
                    #endregion
                    break;
            }

            if (bSaveChange)
            {
                // برای ذخیره تغییرات در ردیف جدید ، پیغامی نمایش داده نشود
                if (e.RowIndex == iNewRow)
                {
                    if (Stack.Use_Web)
                        await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read + "/Items/" + item.Id
                            , item, Stack.token);
                    else
                        Program.dbOperations.UpdateItemAsync(item);

                    lstItems.Remove(lstItems.First(d => d.Id == id));
                    lstItems.Add(item);

                    bSaveChange = false;
                    //AddUpdateItem_to_WarehouseInventory(item, true,JustEdit);
                    //JustEdit = true;
                }
                else
                {
                    if (chkCanEdit.Checked)
                    {
                        if (chkShowUpdateMessage.Checked)
                        {
                            bSaveChange = MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                                , "", MessageBoxButtons.YesNo) == DialogResult.Yes;
                        }
                    }
                }
            }

            if (bSaveChange)
            {
                if (Stack.Use_Web)
                    await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read + "/Items/" + item.Id, item, Stack.token);
                else
                    Program.dbOperations.UpdateItemAsync(item);

                lstItems.Remove(lstItems.First(d => d.Id == id));
                lstItems.Add(item);
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        private void DgvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridViewCell cell = dgvData[e.ColumnIndex, e.RowIndex];

            if (dgvData.Columns[e.ColumnIndex].Name.Equals("Module"))
            { cell.ReadOnly = true; return; }

            if (!chkCanEdit.Checked)
            {
                if (iNewRow >= 0)
                {
                    //MessageBox.Show(iNewRow.ToString());
                    dgvData.ReadOnly = false;
                    if (e.RowIndex == iNewRow)
                        cell.ReadOnly = false;
                    else cell.ReadOnly = true;
                }
            }

        }

        private async void BtnImportDataFromExcel_Click(object sender, EventArgs e)
        {
            if(Program.dbOperations.GetAllModulesAsync(Stack.Company_Id,0).Any())
                MessageBox.Show("دقت نمایید که کدهایی که قبلا در جدول ثبت شده اند، مجددا وارد نمی شوند", "مهم");

            await GetDataFromExcel_OneSheet2(Application.StartupPath + @"\_Requirements\MainData.xlsx", "Modules_Items");
        }

        // امکان استفاده از حالت مِرژ ماژولها در اکسل
        private async Task<bool> GetDataFromExcel_OneSheet2(string ExcelFilePath, string SheetName)
        {
            if (MessageBox.Show(
                "لطفا در فایل اکسل باز شده، و در شیت " + SheetName + " اطلاعات خود را وارد نموده و سپس آنرا ذخیره نمایید."
                +"\n"+"دقت نمایید فایل اکسل نباید ببندید"
                , "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return true;

            panel1.Enabled = false;
            if (Stack.Use_Web)
                pictureBox3.Visible = true;
            else
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.Visible = true;
            }
            Application.DoEvents();
            //pictureBox1.Visible = true;

            EXCEL.Application excelApp = new EXCEL.Application();
            excelApp.DisplayAlerts = false;
            excelApp.Visible = true;
            excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
            EXCEL.Workbook wb = excelApp.Workbooks.Open(ExcelFilePath);
            try
            {
                //MessageBox.Show(openFileDialog1.FileName,"100");
                //wb = excelApp.Workbooks.Open(openFileDialog1.FileName);
                bool bIsBOM_sheetExists = wb.Worksheets.OfType<EXCEL.Worksheet>()
                    .Any(ws => ws.Name.ToLower().Equals(SheetName.ToLower()));
                //MessageBox.Show("", "200");
                if (bIsBOM_sheetExists)
                {
                    //progressBar1.Maximum = 303;
                    EXCEL.Worksheet ws = wb.Worksheets[SheetName];
                    ws.Activate();
                    if (MessageBox.Show("آیا اطلاعات وارد شوند؟", "", MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                    {
                        #region بررسی اینکه آیا کاربر فایل اکسل را بسته است یا خیر. در صورت بسته بودن، آنرا باز میکند
                        try
                        {
                            ws = wb.Worksheets[SheetName];
                        }
                        catch
                        {
                            excelApp = new EXCEL.Application();
                            excelApp.DisplayAlerts = false;
                            excelApp.Visible = false;
                            excelApp.WindowState = EXCEL.XlWindowState.xlMaximized;
                            wb = excelApp.Workbooks.Open(ExcelFilePath);
                            ws = wb.Worksheets[SheetName];
                        }
                        #endregion

                        #region Get Items
                        int n = 1;
                        while (ws.Cells[n, 1].Value != null) n++;
                        //while ((ws.Cells[n, 1].Value != null) || ws.Cells[n, 3].Value != null) n++;

                        progressBar1.Style = ProgressBarStyle.Blocks;
                        progressBar1.Maximum = n;
                        progressBar1.Value = 0;

                        #region لیست تمام ماژول ها
                        List<Models.Module> modules = new List<Models.Module>();
                        if (Stack.Use_Web)
                            modules = await HttpClientExtensions.GetT<List<Models.Module>>
                                (Stack.API_Uri_start_read + "/Modules?all=no&company_Id=" + Stack.Company_Id, Stack.token);
                        else modules = Program.dbOperations.GetAllModulesAsync(Stack.Company_Id,0);
                        #endregion

                        string module_name = null;
                        string module_Small_code = null;
                        int i = 1;
                        while (++i < n)
                        {
                            #region بررسی وضعیت سلولهای یک ردیف و شرط پایان حلقه
                            bool b4 = ws.Cells[i, 4].Value != null;
                            bool b5 = ws.Cells[i, 5].Value != null;

                            //if (b1) b1 = ws.Cells[i, 1].Value.ToString().Length > 0;
                            if (b4) b4 = ws.Cells[i, 4].Value.ToString().Length > 0;
                            if (b5) b5 = ws.Cells[i, 5].Value.ToString().Length > 0;
                            if (!b4 || !b5) continue;

                            bool b2 = ws.Cells[i, 2].Value != null;
                            bool b3 = ws.Cells[i, 3].Value != null;
                            if (b2) b2 = ws.Cells[i, 2].Value.ToString().Length > 0;
                            if (b3) b3 = ws.Cells[i, 3].Value.ToString().Length > 0;
                            //bool bStop = !b1 && !b2 && !b3 && !b4;
                            //if (bStop) break;

                            //bool bContinue = (!b1 || !b2) && (!b3 || !b4);
                            //if (bContinue) continue;
                            #endregion

                            #region تغییرات در جدول کالاها

                            #region تعریف ماژول
                            if (b2) module_name = ws.Cells[i, 2].Value.ToString();

                            if (b3)
                            {
                                // کد ماژول
                                module_Small_code = ws.Cells[i, 3].Value.ToString();
                                //if (Program.dbOperations.GetItemAsync(Stack.Company_Id,module_Small_code,true) == null)
                                if (lstItems.Where(d => d.Enable == true)
                                    .FirstOrDefault(d => d.Code_Small.ToLower().Equals(module_Small_code.ToLower())) == null)
                                {
                                    Models.Item item1 = new Models.Item
                                    {
                                        Company_Id = Stack.Company_Id,
                                        Warehouse_Id = 1,
                                        Name_Samll = module_name,// + " - " + module_Small_code,
                                        Code_Small = module_Small_code,
                                        Enable = true,
                                        Module = true,
                                        Salable = true,
                                    };

                                    if (Stack.Use_Web)
                                        await HttpClientExtensions.PostAsJsonAsync(Stack.API_Uri_start_read
                                            + "/Items", item1, Stack.token);
                                    else
                                        Program.dbOperations.AddItem(item1);

                                    lstItems.Add(item1);
                                }
                            }
                            #endregion

                            #region تعریف کالا
                            string item_Small_code = ws.Cells[i, 5].Value.ToString();
                            double quantity = Convert.ToDouble(ws.Cells[i, 7].Value);

                            Models.Item item2 = lstItems.Where(d => d.Enable == true)
                                    .FirstOrDefault(d => d.Code_Small.ToLower().Equals(item_Small_code.ToLower()));
                            if (item2 == null)
                            {
                                //long index = Program.dbOperations.GetNewIndex_Item();
                                item2 = new Models.Item
                                {
                                    Company_Id = Stack.Company_Id,
                                    Warehouse_Id = 1,
                                    Name_Samll = ws.Cells[i, 4].Value.ToString(),
                                    Code_Small = item_Small_code,
                                    Enable = true,
                                    Salable = true,
                                };

                                if (Stack.Use_Web)
                                    await HttpClientExtensions.PostAsJsonAsync(Stack.API_Uri_start_read
                                        + "/Items", item2, Stack.token);
                                else
                                    Program.dbOperations.AddItem(item2);

                                lstItems.Add(item2);
                            }
                            #endregion
                            #endregion
                            
                            #region رابطه بین ماژول و کالاها
                            // در جدول ماژول ها نباید رکوردی وجود داشته باشد که کد ماژول و کد کالای آن 
                            // با کد ماژول و کد کالای وارد شده یکسان باشد
                            if (!string.IsNullOrEmpty(module_Small_code) && !string.IsNullOrEmpty(item_Small_code))
                            {
                                // غیر فعال کردن رابطه قبلی در صورت وجود
                                Models.Module module1 = modules.FirstOrDefault(d => 
                                    d.Module_Code_Small.Equals(module_Small_code)
                                    && d.Item_Code_Small.Equals(item_Small_code));

                                #region غیرفعال کردن ماژول قبلی
                                if (module1 != null)
                                {
                                    //Models.Module module = Program.dbOperations.GetModuleAsync(module_Small_code, item_Small_code);
                                    module1.Enable = false;
                                    if (Stack.Use_Web)
                                        await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read 
                                            + "/Modules/" + module1.Id, module1, Stack.token);
                                    else
                                        Program.dbOperations.UpdateModuleAsync(module1);
                                }
                                #endregion

                                #region اضافه کردن ماژول جدید
                                Models.Module module2 = new Models.Module
                                {
                                    Company_Id = Stack.Company_Id,
                                    Module_Code_Small = module_Small_code,
                                    Item_Id = item2.Id,
                                    Item_Code_Small = item_Small_code,
                                    Quantity = quantity,
                                    Enable = true,
                                };

                                if (Stack.Use_Web)
                                    await HttpClientExtensions.PostAsJsonAsync(Stack.API_Uri_start_read
                                        + "/Modules" , module2, Stack.token);
                                else
                                    Program.dbOperations.AddModuleAsync(module2);
                                #endregion
                            }
                            #endregion

                            if (progressBar1.Value < progressBar1.Maximum)
                                progressBar1.Value++;
                            Application.DoEvents();
                        }


                        #endregion
                    }

                }
                else
                {
                    MessageBox.Show("شیت " + SheetName + " یافت نشد!", "خطا");
                }
            }
            catch {
                //if (wb == null) wb = excelApp.Workbooks.Add();
            }
            finally
            {
                try
                {
                    wb.Close(SaveChanges: false);
                    excelApp.Workbooks.Close();
                    excelApp.Quit();

                    while (Marshal.ReleaseComObject(wb) != 0) { }
                    while (Marshal.ReleaseComObject(excelApp.Workbooks) != 0) { }
                    while (Marshal.ReleaseComObject(excelApp) != 0) { }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    await GetData(true);
                    dgvData.DataSource = lstItems;
                    Application.DoEvents();
                    //panel1.Enabled = true;
                }
                catch { }
            }

            Application.DoEvents();
            panel1.Enabled = true;
            if (Stack.Use_Web)
                pictureBox3.Visible = false;
            else
            {
                progressBar1.Visible = false;
            }
            //pictureBox1.Visible = false;
            return true;
        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.SelectionMode = chkCanEdit.Checked ? DataGridViewSelectionMode.RowHeaderSelect
                : DataGridViewSelectionMode.FullRowSelect;
            dgvData.ReadOnly = !chkCanEdit.Checked;
            //dgvData.Columns["Code_Small"].ReadOnly = true;  // ستون کدها در هر حالتی باید غیرقابل تغییر باشد
            dgvData.Columns["Module"].ReadOnly = true;  // ستون ماژول در هر حالتی باید غیرقابل تغییر باشد

            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dgvData.DataSource = GetData();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            //dgvData.Visible = false;
            Application.DoEvents();
            //List<Models.Item> lstItems = GetData();
            //dgvData.DataSource = GetData();
            List<Models.Item> lstItems1 = (List<Models.Item>)dgvData.DataSource;

            //MessageBox.Show(lstItems.Count.ToString());

            //if (!string.IsNullOrWhiteSpace(txtST_Name.Text)
            //   || !string.IsNullOrWhiteSpace(txtST_SmallCode.Text))
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstItems1 = SearchThis(lstItems1, c.Name);
                                if ((lstItems1 == null) || !lstItems1.Any()) break;
                            }
                    }
                }
            }

            switch (cmbItemsEnable.SelectedIndex)
            {
                case 0: lstItems1 = lstItems1.Where(d => d.Enable).ToList(); break;
                case 1: lstItems1 = lstItems1.Where(d => !d.Enable).ToList(); break;
                //case 2: enableType = 0; break;
            }

            if (radModule.Checked) lstItems1 = lstItems1.Where(d => d.Module).ToList();
            else if (radNotModule.Checked) lstItems1 = lstItems1.Where(d => !d.Module).ToList();
            //else return Program.dbOperations.GetAllItemsAsync(Stack.Company_Id, enableType);

            if(cmbWarehouses.SelectedIndex>0)
            {
                long wh_id = lstWarehousess.First(d=>d.Name.Equals(cmbWarehouses.Text)).Id;
                lstItems1 = lstItems1.Where(d => d.Warehouse_Id == wh_id).ToList();
            }

            if(cmbCategories.SelectedIndex>0)
            {
                long cat_id = lstCats.First(d=>d.Name.Equals(cmbCategories.Text)).Id;
                lstItems1 = lstItems1.Where(d => d.Category_Id == cat_id).ToList();
            }

            dgvData.DataSource = lstItems1;// GetData();

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;
            bAnySearch = lstItems.Count != lstItems1.Count;
            
        }

        // جستجوی موردی
        private List<Models.Item> SearchThis(List<Models.Item> lstItems1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_SmallCode":
                    switch (cmbST_SmallCode.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Code_Small.ToLower().Contains(txtST_SmallCode.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Code_Small.ToLower().StartsWith(txtST_SmallCode.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Code_Small.ToLower().Equals(txtST_SmallCode.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
                //break;
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Name_Samll.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Name_Samll.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Name_Samll.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
                case "txtST_FullCode":
                    switch (cmbST_FullCode.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Code_Full.ToLower().Contains(txtST_FullCode.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Code_Full.ToLower().StartsWith(txtST_FullCode.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Code_Full.ToLower().Equals(txtST_FullCode.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
                //break;
                case "txtST_FullName":
                    switch (cmbST_FullName.SelectedIndex)
                    {
                        case 0:
                            return lstItems1.Where(d => d.Name_Full.ToLower().Contains(txtST_FullName.Text.ToLower())).ToList();
                        case 1:
                            return lstItems1.Where(d => d.Name_Full.ToLower().StartsWith(txtST_FullName.Text.ToLower())).ToList();
                        case 2:
                            return lstItems1.Where(d => d.Name_Full.ToLower().Equals(txtST_FullName.Text.ToLower())).ToList();
                        default: return lstItems1;
                    }
            }

            return null;
        }

        private async void tsmiItemStructure_Click(object sender, EventArgs e)
        {
            bool enable = Convert.ToBoolean(dgvData.CurrentRow.Cells["Enable"].Value);
            if (!enable)
            {
                MessageBox.Show("این کالا فعال نمی باشد، در نتیجه امکان تعریف یا تغییر زیر ساخت برای آن وجود ندارد"
                    , "خطا");
            }
            else
            {
                Stack.bx = false;   // آیا تغییری انجام شده است؟
                long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
                //Models.Item item = Program.dbOperations.GetItemAsync(index);
                new K1320_Modules(lstItems.First(d=>d.Id==id)).ShowDialog();

                if (Stack.bx)
                {
                    await GetData(true);
                    dgvData.DataSource = lstItems;
                }
            }
        }

        private void DgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Don't throw an exception when we're done.
            e.ThrowException = false;
            if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("FixedPrice")
                || dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.Equals("SalesPrice"))
            {
                MessageBox.Show("لطفا «بهای تمام شده» را به صورت عدد وارد نمایید.", "خطای نوع داده", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("خطای نوع داده", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }

        private void RadModule_CheckedChanged(object sender, EventArgs e)
        {
            //if (radModule.Checked) dgvData.DataSource = GetData().Where(d => d.Module).ToList();
            //else if (radNotModule.Checked) dgvData.DataSource = GetData().Where(d => !d.Module).ToList();
            //else dgvData.DataSource = GetData();
        }

        private void TxtST_SmallCode_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSearch;
        }

        private void TxtST_SmallCode_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private async void BtnShowAll_Click(object sender, EventArgs e)
        {
            radModule.Checked = radNotModule.Checked = false;
            //RadModule_CheckedChanged(null, null);
            await GetData();
            dgvData.DataSource = lstItems;
            bAnySearch = false;
        }

        private void TsmiProperties_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            new K1330_Item_Properties(lstItems.First(d => d.Id == id)).ShowDialog();
        }

        private async void TsmiItem_Warehouse_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            Models.Item item = lstItems.First(d => d.Id == id);
            new K1304_Item_ChooseWarehouse(item).ShowDialog();

            if (Stack.ix > 0)
            {
                item.Warehouse_Id = Stack.ix;

                if(Stack.Use_Web)
                    await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read
                        + "/Items/" + id, item, Stack.token);
                else 
                    Program.dbOperations.UpdateItemAsync(item);
                //Application.DoEvents();
                //pictureBox1.Visible = true;
                //timer1.Enabled = true;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            timer1.Enabled = false;
        }

        private void TsmiDiagram_Click(object sender, EventArgs e)
        {
            new K1322_Module_Diagram(dgvData.CurrentRow.Cells["Code_Small"].Value.ToString()).ShowDialog();
        }

        private void DgvData_Enter(object sender, EventArgs e)
        {
            CancelButton = null;
        }

        private void DgvData_Leave(object sender, EventArgs e)
        {
            CancelButton = btnReturn;
        }

        private async void TsmiAdd_ChangeItemImage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("حجم فایل نباید بیشتر از 50 کیلو بایت باشد", "توجه نمایید");

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                if(new FileInfo(file).Length> 51200000)
                {
                    MessageBox.Show("حجم فایل انتخاب شده، بیشتر از 50 کیلو باید می باشد", "خطا");
                    return;
                }

                //string item_code= Convert.ToString(dgvData.CurrentRow.Cells["Code_Small"].Value);
                long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
                if (Stack.Use_Web) await Add_ChangeImage_web(lstItems.First(d => d.Id == id), file, true);
                else Add_ChangeImage(lstItems.First(d=>d.Id == id),file);
                Application.DoEvents();

                pictureBox2.Image =new Bitmap(file);
                pictureBox1.Visible = true;
                timer1.Enabled = true;
            }
        }

        private async void TsmiDeleteItemImage_Click(object sender, EventArgs e)
        {
            // در اصل تصویر غیرفعال می شود
            if (MessageBox.Show("آیا از حذف تصویر اطمینان دارید؟", "اخطار"
                , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            string code_cmall = Convert.ToString(dgvData.CurrentRow.Cells["Code_Small"].Value);
            Models.Item_File item_file = null;

            if (Stack.Use_Web)
            {
                List<Models.Item_File> item_files = await HttpClientExtensions.GetT<List< Models.Item_File>>
                    (Stack.API_Uri_start_read + "/Item_File?all=no&company_Id=" + Stack.Company_Id
                    + "&ItemSmallCode=" + code_cmall, Stack.token);
                if (item_files.Any(d => d.Enable))
                {
                    item_file = item_files.First(d => d.Enable);
                    item_file.Enable = false;
                    await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read
                        + "/Item_File/" + item_file.Id, item_file, Stack.token);
                }
            }
            else
            {
                item_file = Program.dbOperations.GetItem_FileAsync(code_cmall, 1, true);
                item_file.Enable = false;
                Program.dbOperations.UpdateItem_FileAsync(item_file);
            }
            pictureBox1.Visible = true;
            timer1.Enabled = true;
            pictureBox2.Image = null;
        }

        private async void BtnGetImages_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا مایلید تمام تصاویر از مسیر مشخص شده، به دیتابیس اضافه گردد؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            //return;

            string ImageDirectoryPath = Application.StartupPath + @"\_Requirements\Images\";
            List<string> files = Directory.EnumerateFiles(
                ImageDirectoryPath, "*.jpg", SearchOption.AllDirectories).ToList();

            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = 0;
            progressBar1.Maximum = files.Count;
            panel1.Enabled = false;
            progressBar1.Visible = true;

            //foreach (string file in Directory.EnumerateFiles(
            //               ImageDirectoryPath, "*.jpg", SearchOption.AllDirectories))
            foreach (string file in files)
            {
                string item_code = Path.GetFileNameWithoutExtension(file);
                Models.Item item = lstItems.Where(d => d.Enable).FirstOrDefault(d => d.Code_Small.ToLower()
                      .Equals(item_code.ToLower()));
                if(item==null)
                    item = lstItems.Where(d => !d.Enable).FirstOrDefault(d => d.Code_Small.ToLower()
                      .Equals(item_code.ToLower()));

                if (item != null)
                {
                    if (Stack.Use_Web) await Add_ChangeImage_web(item, file);
                    else  Add_ChangeImage(item, file);
                }

                progressBar1.Value++;
                Application.DoEvents();
            }

            progressBar1.Visible = false;
            panel1.Enabled = true;
        }

        private void btnDeleteAllImages_Click(object sender, EventArgs e)
        {
            if (Stack.Use_Web)
            {
                MessageBox.Show("این امکان فعال نمی باشد");
                return;
            }
            else
            {
                if (MessageBox.Show("آیا از حذف تمام تصاویر اطمینان دارید؟", "اخطار 1"
               , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                if (MessageBox.Show("با انجام این عمل ، تمام تصاویر کالاها از بین خواهد رفت"
                    + "\n" + "آیا از حذف آنها اطمینان دارید؟", "اخطار 2"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                return;
                // تمام فایلها حذف می شوند. نمیتونم اجازه بدم
                Program.dbOperations.DeleteAllFilesAsync();
                Program.dbOperations.DeleteAllItem_FilesAsync();
                pictureBox2.Image = null;
            }
        }

        private async void DgvData_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            long id = Convert.ToInt64(dgvData["Id", e.RowIndex].Value);
            string sc = Convert.ToString(dgvData["Code_Small", e.RowIndex].Value);
            #region نمایش تصویر کالا در صورت وجود
            Models.Item_File item_file = null;
            if (Stack.Use_Web)
            {
                List<Models.Item_File> item_files = await HttpClientExtensions.GetT<List<Models.Item_File>>
                    (Stack.API_Uri_start_read + "/Item_File?all=no&company_Id=" + Stack.Company_Id + "&ItemSmallCode=" + sc, Stack.token);
                if((item_files!=null) && item_files.Any(d=>d.Enable))
                    item_file = item_files.First(d => d.Enable);
            }
            else
                item_file = Program.dbOperations.GetItem_FileAsync(sc, 1, true);

            if (item_file != null)
            {
                Models.File file = null;
                if (Stack.Use_Web)
                    file = await HttpClientExtensions.GetT<Models.File>(Stack.API_Uri_start_read
                        + "/Files/" + item_file.File_Id, Stack.token);
                else file = Program.dbOperations.GetFileAsync(item_file.File_Id);

                if(file!=null)
                    pictureBox2.Image = new ThisProject().ByteToImage(file.Content);
                else pictureBox2.Image = null;
            }
            else pictureBox2.Image = null;
            #endregion
        }

        private async void TsmiItem_Details_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            Models.Item item = lstItems.First(d => d.Id == id); // Program.dbOperations.GetItemAsync(id);

            int type = (Stack.UserLevel_Type==1) || Stack.lstUser_ULF_UniquePhrase.Contains("jn2110") ? 1 : 0;
            new K1302_Item_Details(type,item).ShowDialog();

            if (Stack.bx)
            {
                panel1.Enabled = false;
                pictureBox3.Visible = true;
                //dgvData.Enabled = false;
                Application.DoEvents();
                lstItems = await GetData(true);
                //radNotModule.Checked = true;
                if (bAnySearch)
                    BtnSearch_Click(null, null);
                else dgvData.DataSource = lstItems;

                DataGridViewRow row = null;
                if ((row = dgvData.Rows.Cast<DataGridViewRow>().ToList().FirstOrDefault
                    (d => d.Cells["Id"].Value.ToString().Equals(id.ToString()))) != null)
                {
                    dgvData.CurrentCell = row.Cells["Name_Samll"];
                }

                Application.DoEvents();
                //dgvData.Enabled = true;
                panel1.Enabled = true;
                pictureBox3.Visible = false;

            }

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSearch;
            //MessageBox.Show("in");
        }

        private void GroupBox1_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
            //MessageBox.Show("out");
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TsmiItem_Details_Click(null, null);
        }

        private void Add_ChangeImage(Models.Item item,string file)
        {
            
            // اگر کالا قبلا تصویری داشته باشد، ابتدا آنرا غیرفعال می کند
            if (Program.dbOperations.GetItem_FileAsync(item.Code_Small, 1, true) != null)
            {
                Models.Item_File item_file = Program.dbOperations.GetItem_FileAsync(item.Code_Small, 1, true);
                item_file.Enable = false;
                Program.dbOperations.UpdateItem_FileAsync(item_file);
            }

            long file_id = Program.dbOperations.AddFileAsync(new Models.File
            {
                Company_Id = Stack.Company_Id,
                Content = new ThisProject().ConvertImageToByteArray(file),
                OriginalFileName = Path.GetFileName(file),
                Description = "تصویر کالای " + item.Code_Small,
                DateTime_mi = DateTime.Now.ToString(),
                DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                Enable = true,
            }) ;

            Program.dbOperations.AddItem_FileAsync(new Models.Item_File
            {
                Company_Id = Stack.Company_Id,
                File_Id = file_id,
                Item_Id = item.Id,
                Item_Code_Small = item.Code_Small,
                Type = 1,   // مربوط به تصویر یک کالا
                Enable = true,
            });
        }

        private async void BtnChangeCategoryForAll_Click(object sender, EventArgs e)
        {
            List<Models.Item> lstItems1 = (List<Models.Item>) dgvData.DataSource;

            if (!lstItems1.Any()) return;

            //if (MessageBox.Show("آیا از تغییر دسته اطمینان دارید؟", "اخطار"
            //    , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            new K1306_Item_ChooseCategory().ShowDialog();

            if(Stack.lx>0)
            {
                panel1.Enabled = false;
                Application.DoEvents();

                if (Stack.Use_Web)
                {
                    pictureBox3.Visible = true;
                    foreach(Models.Item item in lstItems1.Where(d=>d.Category_Id!=Stack.lx).ToList())
                    {
                        item.Category_Id = Stack.lx;
                        await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read
                          + "/Items/" + item.Id, item, Stack.token);
                    }
                }
                else
                {
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    progressBar1.Visible = true;
                    foreach (Models.Item item in lstItems1.Where(d => d.Category_Id != Stack.lx).ToList())
                    {
                        item.Category_Id = Stack.lx;
                        Program.dbOperations.UpdateItem(item);
                        Application.DoEvents();
                    }
                }

                lstItems = await GetData(true);
                //radNotModule.Checked = true;
                if (bAnySearch)
                    BtnSearch_Click(null, null);
                else dgvData.DataSource = lstItems;

                panel1.Enabled = true;
                pictureBox3.Visible = false;
                progressBar1.Visible = false;
            }
        }

        private async void BtnChangeWarehouseForAll_Click(object sender, EventArgs e)
        {
            List<Models.Item> lstItems1 = (List<Models.Item>)dgvData.DataSource;

            if (!lstItems1.Any()) return;

            //if (MessageBox.Show("آیا از تغییر دسته اطمینان دارید؟", "اخطار"
            //    , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            new K1304_Item_ChooseWarehouse().ShowDialog();

            if (Stack.lx > 0)
            {
                panel1.Enabled = false;
                Application.DoEvents();

                if (Stack.Use_Web)
                {
                    pictureBox3.Visible = true;
                    foreach (Models.Item item in lstItems1.Where(d => d.Warehouse_Id != Stack.lx).ToList())
                    {
                        item.Warehouse_Id = Stack.lx;
                        await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read
                          + "/Items/" + item.Id, item, Stack.token);
                    }
                }
                else
                {
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    progressBar1.Visible = true;
                    foreach (Models.Item item in lstItems1.Where(d => d.Warehouse_Id != Stack.lx).ToList())
                    {
                        item.Warehouse_Id = Stack.lx;
                        Program.dbOperations.UpdateItem(item);
                        Application.DoEvents();
                    }
                }

                lstItems = await GetData(true);
                //radNotModule.Checked = true;
                if (bAnySearch)
                    BtnSearch_Click(null, null);
                else dgvData.DataSource = lstItems;

                panel1.Enabled = true;
                pictureBox3.Visible = false;
                progressBar1.Visible = false;
            }
        }

        private async Task<bool> Add_ChangeImage_web(Models.Item item,string file,bool prepare_form=false)
        {
            if(prepare_form)
            {
                panel1.Enabled = false;
                pictureBox3.Visible = true;
                Application.DoEvents();
            }

            List<Models.Item_File> item_files = await HttpClientExtensions.GetT<List<Models.Item_File>>
                (Stack.API_Uri_start_read + "/Item_File?all=no&company_Id="+Stack.Company_Id+"&ItemSmallCode="+item.Code_Small, Stack.token);

            // اگر کالا قبلا تصویری داشته باشد، ابتدا آنرا غیرفعال می کند
            if (item_files.Any(d=>d.Enable))
            {
                return false;

                foreach (Models.Item_File item_file in item_files)
                {
                    item_file.Enable = false;
                    await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read
                        + "/Item_File/" + item_file.Id, item_file, Stack.token);
                }
            }

            Models.File file1 = new Models.File
            {
                Company_Id = Stack.Company_Id,
                Content = new ThisProject().ConvertImageToByteArray(file),
                OriginalFileName = Path.GetFileName(file),
                Description = "تصویر کالای " + item.Code_Small,
                DateTime_mi = DateTime.Now.ToString(),
                DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                Enable = true,
            };

            long file_id = -1;
            var response = await HttpClientExtensions.PostAsJsonAsync
                (Stack.API_Uri_start_read + "/Files", file1, Stack.token);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("اشکال در ثبت اطلاعات", "خطا");
                return false;
            }
            else
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                Models.File file2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.File>(responseString);
                file_id = file2.Id;
            }

            if (file_id > 0)
            {
                Models.Item_File item_file = new Models.Item_File
                {
                    Company_Id = Stack.Company_Id,
                    File_Id = file_id,
                    Item_Id = item.Id,
                    Item_Code_Small = item.Code_Small,
                    Type = 1,   // مربوط به تصویر یک کالا
                    Enable = true,
                };

                await HttpClientExtensions.PostAsJsonAsync
                    (Stack.API_Uri_start_read + "/Item_File", item_file, Stack.token);
            }

            if (prepare_form)
            {
                Application.DoEvents();
                panel1.Enabled = true;
                pictureBox3.Visible = false;
            }

            return true;
        }


    }
}
