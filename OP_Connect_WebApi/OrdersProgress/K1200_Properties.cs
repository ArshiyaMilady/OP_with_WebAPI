using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdersProgress
{
    public partial class K1200_Properties : X210_ExampleForm_Normal
    {
        List<Models.Property> lstProperties = new List<Models.Property>();

        public K1200_Properties()
        {
            InitializeComponent();
        }

        private async void K1200_Properties_Shown(object sender, EventArgs e)
        {
            //btnDeleteAll.Visible = Stack.UserLevel_Type==1;
            panel2.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jn1110");
            btnAddNew.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jn1120");

            cmbST_Name.SelectedIndex = 0;
            cmbST_Description.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;

            dgvData.DataSource = await GetData();
            ShowData();
        }

        private async Task<List<Models.Property>> GetData(bool bForceReset = false)
        {
            if (!lstProperties.Any() || bForceReset)
            {
                if (Stack.Use_Web)
                    lstProperties = await HttpClientExtensions.GetT<List<Models.Property>>
                        (Stack.API_Uri_start_read + "/Properties?all=no&company_Id=" + Stack.Company_Id, Stack.token);
                else
                    lstProperties = Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Id, 0);
            }

            switch (comboBox1.SelectedIndex)
            {
                case 0: return lstProperties.Where(d => d.Enable).ToList();
                case 2: return lstProperties.Where(d => !d.Enable).ToList();
            }

            return lstProperties;
        }

        private void ShowData(bool ChangeHeaderTexts = true)
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            if (ChangeHeaderTexts)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    switch (col.Name)
                    {
                        case "Name":
                            col.HeaderText = "نام مشخصه";
                            col.Width = 200;
                            break;
                        case "Description":
                            col.HeaderText = "شرح";
                            col.Width = 100;
                            break;
                        case "Enable":
                            col.HeaderText = "فعال؟";
                            col.Width = 50;
                            break;
                        //case "ChangingValue":
                        //    col.HeaderText = "آیا مشخصه در هنگام سفارش قابل تغییر باشد؟";
                        //    //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        //    col.Width = 150;
                        //    break;
                        default: col.Visible = false; break;
                    }
                }
            }
            #endregion
        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            //long index = Program.dbOperations.GetNewIndex_Property();
            Models.Property property = new Models.Property
            {
                Company_Id = Stack.Company_Id,
                //Index = index,
                Enable = true,
                Name = "نام x",// + (index%100000),
            };

            if (Stack.Use_Web)
            {
                var response = await HttpClientExtensions.PostAsJsonAsync
                    (Stack.API_Uri_start_read + "/Properties", property, Stack.token);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("اشکال در ثبت اطلاعات", "خطا");
                    return;
                }
                else
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    property = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Property>(responseString);
                }
            }
            else
                property.Id = Program.dbOperations.AddPropertyAsync(property);

            lstProperties.Add(property);
            dgvData.DataSource = await GetData();// true);// Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Id);
            dgvData.CurrentCell = dgvData["Name", dgvData.Rows.Count - 1];
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
                if (MessageBox.Show("آیا از حذف تمام مشخصات اطمینان دارید؟", "اخطار 1"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                if (MessageBox.Show("با انجام این عمل ، تمام روابط مشخصه ها و کالاها از بین خواهد رفت"
                    + "\n" + "آیا از حذف تمام مشخصات اطمینان دارید؟", "اخطار 2"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                Program.dbOperations.DeleteAllPropertiesAsync();
                Program.dbOperations.DeleteAllItem_PropertiesAsync();
                dgvData.DataSource = Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Id);
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
            object value = dgvData[e.ColumnIndex, e.RowIndex].Value;
            if (value == InitailValue) return;

            bool bEnableChanged = false;
            bool bSaveChange = true;   // آیا تغییر ذخیره شود؟
            long id = Convert.ToInt64(dgvData["Id", e.RowIndex].Value);

            Models.Property property = lstProperties.First(d => d.Id == id);// Program.dbOperations.GetPropertyAsync(index);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Name":
                    property.Name = Convert.ToString(dgvData["Name", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(property.Name)) return;
                    else if (lstProperties.Where(d => d.Id != id)
                        .Any(j => j.Name.ToLower().Equals(property.Name.ToLower())))
                    {
                        MessageBox.Show("نام مشخصه قبلا استفاده شده است", "خطا");
                        bSaveChange = false;
                    }
                    break;
                case "Description":
                    property.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
                    break;
                case "Enable":
                    property.Enable = Convert.ToBoolean(dgvData["Enable", e.RowIndex].Value);
                    bEnableChanged = !property.Enable && Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Id, id).Any();
                    break;

            }

            if (bSaveChange)
            {
                string msg = null;
                string title = property.Name;
                if (bEnableChanged)
                    msg = "با انجام این عمل ، مشخصه انتخاب شده از کالاهایی که دارای آن می باشند، حذف می گردد. آیا تغییرات ثبت شود؟";
                else if (chkShowUpdateMessage.Checked)
                    msg = "آیا از ثبت تغییرات اطمینان دارید؟";

                if (!string.IsNullOrEmpty(msg))
                {
                    msg = dgvData.Columns[e.ColumnIndex].HeaderText + " = " + value + "\n" + msg;
                    bSaveChange = MessageBox.Show(msg, title, MessageBoxButtons.YesNo) == DialogResult.Yes;
                }

                if (bSaveChange)
                {
                    panel1.Enabled = false;
                    pictureBox3.Visible = true;
                    Application.DoEvents();

                    if (Stack.Use_Web)
                    {
                        var res = await HttpClientExtensions.PutAsJsonAsync
                            (Stack.API_Uri_start_read + "/Properties/" + id, property, Stack.token);
                        if (res.IsSuccessStatusCode)
                        {
                            pictureBox3.Visible = false;
                            panel1.Enabled = true;
                        }
                    }
                    else
                        Program.dbOperations.UpdatePropertyAsync(property);

                    #region اگر مشخصه غیرفعال شود تمام ارتباطات آن با کالاهای دیگر حذف می گردد
                    if (bEnableChanged)
                    {

                        List<Models.Item_Property> lstItem_Properties = new List<Models.Item_Property>();
                        if (Stack.Use_Web) lstItem_Properties = await HttpClientExtensions.GetT<List<Models.Item_Property>>
                             (Stack.API_Uri_start_read + "/Item_Property?all=no&company_Id=" + Stack.Company_Id
                             + "&Item_Id=0&Property_Id=" + id, Stack.token);
                        else lstItem_Properties = Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Id, 0, id);

                        if (lstItem_Properties.Any())
                        {
                            progressBar1.Value = 0;
                            progressBar1.Visible = true;
                            progressBar1.Maximum = lstItem_Properties.Count;
                            Application.DoEvents();

                            foreach (Models.Item_Property ip in lstItem_Properties)
                            {
                                if (Stack.Use_Web) await HttpClientExtensions.DeleteAsJsonAsync2
                                    (Stack.API_Uri_start_read + "/Item_Property/" + id, Stack.token);
                                else Program.dbOperations.DeleteItem_PropertyAsync(ip);

                                progressBar1.Value++;
                                Application.DoEvents();
                            }
                            progressBar1.Visible = false;
                        }
                    }
                    #endregion
                    dgvData.DataSource = await GetData(true);
                    dgvData.CurrentCell = dgvData["Name", e.RowIndex];

                    Application.DoEvents();
                    panel1.Enabled = true;
                    pictureBox3.Visible = false;
                }
            }

            if(!bSaveChange)
                dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        private void K1200_Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel1.Focus();
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        int iX = 0, iY = 0;
        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            iX = e.X;
            iY = e.Y;
        }

        private void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.ReadOnly = !chkCanEdit.Checked;
            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
        }

        private async void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           dgvData.DataSource = await GetData();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //List<Models.Property> lstPr = Program.dbOperations.GetAllPropertiesAsync(Stack.Company_Id,1);

            //if (string.IsNullOrWhiteSpace(txtST_Name.Text)
            //    && string.IsNullOrWhiteSpace(txtST_Description.Text))
            //{
            //    GetData();
            //    return;
            //}


            panel1.Enabled = false;
            dgvData.Visible = false;
            Application.DoEvents();

            List<Models.Property> lstPr = (List<Models.Property>)dgvData.DataSource;
            //MessageBox.Show(lstItems.Count.ToString());

            foreach (Control c in groupBox1.Controls)
            {
                //MessageBox.Show(c.Text);
                if (c.Name.Length > 4)
                {
                    if (c.Name.Substring(0, 5).Equals("txtST"))
                        if (!string.IsNullOrWhiteSpace(c.Text))
                        {
                            lstPr = SearchThis(lstPr, c.Name);
                            //MessageBox.Show(c.Text, c.Name);
                            //MessageBox.Show(lstItems.Count.ToString(), c.Name);
                            if ((lstPr == null) || !lstPr.Any()) break;
                        }
                }
            }

            dgvData.DataSource = lstPr;

            Application.DoEvents();
            panel1.Enabled = true;
            dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.Property> SearchThis(List<Models.Property> lstPr1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Description":
                    switch (cmbST_Description.SelectedIndex)
                    {
                        case 0:
                            return lstPr1.Where(d => d.Description.ToLower().Contains(txtST_Description.Text.ToLower())).ToList();
                        case 1:
                            return lstPr1.Where(d => d.Description.ToLower().StartsWith(txtST_Description.Text.ToLower())).ToList();
                        case 2:
                            return lstPr1.Where(d => d.Description.ToLower().Equals(txtST_Description.Text.ToLower())).ToList();
                        default: return lstPr1;
                    }
                //break;
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstPr1.Where(d => d.Name.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstPr1.Where(d => d.Name.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstPr1.Where(d => d.Name.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstPr1;
                    }
            }

            return null;
        }


        // مشخصه انتخابی را به تمام کالاها (در صورتیکه آن کالا ، مشخصه را نداشت) اضافه میکند
        private async void TsmiAddToAllItems_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            Models.Property pr = lstProperties.First(d=>d.Id==id);

            if(!pr.Enable)
            {
                MessageBox.Show("امکان اضافه کردن مشخصه های غیرفعال وجود ندارد", "خطا");
                return;
            }

            progressBar1.Value = 0;
            panel1.Enabled=false;
            pictureBox3.Visible = true;
            progressBar1.Visible = true;
            Application.DoEvents();

            List<Models.Item> lstItems = new List<Models.Item>();
            if (Stack.Use_Web) lstItems = await HttpClientExtensions.GetT<List<Models.Item>>
                     (Stack.API_Uri_start_read + "/Items?all=no&company_Id=" + Stack.Company_Id, Stack.token);
            else lstItems = Program.dbOperations.GetAllItemsAsync(Stack.Company_Id, 0);

            List<Models.Item_Property> lstItem_Properties = new List<Models.Item_Property>();
            if (Stack.Use_Web) lstItem_Properties = await HttpClientExtensions.GetT<List<Models.Item_Property>>
                 (Stack.API_Uri_start_read + "/Item_Property?all=no&company_Id=" + Stack.Company_Id
                 + "&Item_Id=0&Property_Id=" + id, Stack.token);
            else lstItem_Properties = Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Id, 0, id);

            progressBar1.Maximum = lstItems.Count;
            foreach(Models.Item item in lstItems)
            //foreach(string sCode_Small in lstItems.Select(d=>d.Code_Small))
            {
                // اگر کالا با مشخصه در ارتباط نبود
                if(!lstItem_Properties.Where(d=>d.Item_Id == item.Id).Any())
                {
                    Models.Item_Property ip = new Models.Item_Property
                    {
                        Item_Id = item.Id,
                        Item_Code_Small = item.Code_Small,
                        //Item_Enable = item.Enable,
                        Property_Index = id,
                        //Property_Enable = pr.Enable,
                    };
                    if (Stack.Use_Web)
                        await HttpClientExtensions.PostAsJsonAsync
                            (Stack.API_Uri_start_read + "/Item_Property", ip, Stack.token);
                    else Program.dbOperations.AddItem_PropertyAsync(ip);
                }
                progressBar1.Value++;
                Application.DoEvents();
            }

            dgvData.DataSource = await GetData(true);
            
            Application.DoEvents();
            panel1.Enabled = true;
            pictureBox3.Visible = false;
            progressBar1.Visible = false;
        }

        // مشخصه انتخابی را از تمام کالاها (در صورتیکه آن کالا ، مشخصه را داشت) حذف میکند
        private async void TsmiDeleteFromAllItems_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف این مشخصه از کلیه کالاها اطمینان دارید؟", ""
                , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            progressBar1.Value = 0;
            pictureBox3.Visible = true;
            panel1.Enabled = false;
            progressBar1.Visible = true;
            Application.DoEvents();

            long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);

            List<Models.Item_Property> lstItem_Properties = new List<Models.Item_Property>();
            if (Stack.Use_Web) lstItem_Properties = await HttpClientExtensions.GetT<List<Models.Item_Property>>
                 (Stack.API_Uri_start_read + "/Item_Property?all=no&company_Id=" + Stack.Company_Id
                 + "&Item_Id=0&Property_Id=" + id, Stack.token);
            else lstItem_Properties = Program.dbOperations.GetAllItem_PropertiesAsync(Stack.Company_Id, 0, id);

            foreach (Models.Item_Property ip in lstItem_Properties)
            {
                if (Stack.Use_Web)
                    await HttpClientExtensions.DeleteAsJsonAsync2
                        (Stack.API_Uri_start_read + "/Item_Property/" + id, Stack.token);
                else Program.dbOperations.DeleteItem_PropertyAsync(ip);

                progressBar1.Value++;
                Application.DoEvents();
            }

            Application.DoEvents();
            panel1.Enabled = true;
            pictureBox3.Visible = false;
            progressBar1.Visible = false;
        }

        private async void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = await GetData();
        }

        private async void TsmiDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف این مورد اطمینان دارید؟", "", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            try
            {
                long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
                Models.Property property = lstProperties.First(d=>d.Id == id);

                if (Stack.Use_Web)
                    await HttpClientExtensions.DeleteAsJsonAsync2(Stack.API_Uri_start_read
                        + "/Properties/" + id, Stack.token);
                else Program.dbOperations.DeletePropertyAsync(property);

                lstProperties.Remove(property);
                dgvData.DataSource = await GetData();
            }
            catch { MessageBox.Show("خطا در اجرای عملیات"); }
        }



    }
}
