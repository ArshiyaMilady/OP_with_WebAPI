using Newtonsoft.Json;
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
    public partial class J2210_UL_Features : X210_ExampleForm_Normal
    {
        List<Models.UL_Feature> lstULFs = new List<Models.UL_Feature>();
        
        public J2210_UL_Features()
        {
            InitializeComponent();

            //btnDeleteAll.Visible = !Stack.Use_Web;
        }

        private async void J2210_UL_Features_Shown(object sender, EventArgs e)
        {
            cmbST_Unique_Phrase.SelectedIndex = 0;
            cmbST_Description.SelectedIndex = 0;

            dgvData.DataSource = await GetData();
            ShowData();
            ColorDgv();

            Application.DoEvents();
            panel1.Enabled = true;
        }

        private async Task<List<Models.UL_Feature>> GetData()
        {
            if (!lstULFs.Any())
            {
                if (Stack.Use_Web)
                    lstULFs = await HttpClientExtensions.GetT<List<Models.UL_Feature>>
                        (Stack.API_Uri_start_read + "UL_Feature?all=no&company_Id=" + Stack.Company_Id
                        + "&EnableType=0&ul_Id=" + Stack.UserLevel_Id, Stack.token);
                else
                {
                    lstULFs = Program.dbOperations.GetAllUL_FeaturesAsync(Stack.Company_Id, 0);
                    if (Stack.UserLevel_Type == 2)
                        lstULFs = lstULFs.Where(d => !d.Unique_Phrase.Substring(0, 1).Equals("d")).ToList();
                    else if (Stack.UserLevel_Type != 1) lstULFs = new List<Models.UL_Feature>();
                }
            }

            // do not use 'else'
            if (!lstULFs.Any())
            {
                lstULFs = lstULFs.OrderBy(d => d.Unique_Phrase).ToList();

                if (radEnabled.Checked) return lstULFs.Where(d => d.Enabled).ToList();
                else if (radDisabled.Checked) return lstULFs.Where(d => !d.Enabled).ToList();
            }

            return lstULFs;
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    //case "Index":
                    //    col.HeaderText = "شناسه";
                    //    col.Width = 50;
                    //    break;
                    case "Description":
                        col.HeaderText = "شرح";
                        col.Width = 500;
                        break;
                    case "Unique_Phrase":
                        col.HeaderText = "عبارت شاخص";
                        //col.ReadOnly = true;
                        col.Width = 100;
                        break;
                    case "Enabled":
                        col.HeaderText = "فعال؟";
                        col.Width = 50;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        // رنگ بندی امکانات
        private void ColorDgv()
        {
            foreach (DataGridViewRow row in dgvData.Rows.Cast<DataGridViewRow>()
                .Where(d => d.Cells["Unique_Phrase"].Value.ToString().Length>2)
                .Where(d => d.Cells["Unique_Phrase"].Value.ToString().Substring(2).Equals("0000")).ToList())
                row.DefaultCellStyle.BackColor = Color.Yellow;
        }

        private async void BtnAddNew_Click(object sender, EventArgs e)
        {
            if (!chkCanEdit.Checked)
                chkCanEdit.Checked = true;

            bool b2 = true;
            if (chkShowUpdateMessage.Checked)
                b2 = chkShowUpdateMessage.Checked = false;

            //long index = Program.dbOperations.GetNewIndex_UL_Feature();

            long id = -1;

            Models.UL_Feature ulf = new Models.UL_Feature
            {
                Company_Id = Stack.Company_Id,
                //Index = index,
                Unique_Phrase = "x",// + index,
                Description = "؟",
                Enabled=true,
            };

            if (Stack.Use_Web)
            {
                var response = await HttpClientExtensions.PostAsJsonAsync
                    (Stack.API_Uri_start_read + "/UL_Feature", ulf, Stack.token);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("اشکال در ثبت اطلاعات", "خطا");
                    return;
                }
                else
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    Models.UL_Feature ulf1 = JsonConvert.DeserializeObject<Models.UL_Feature>(responseString);
                    id = ulf1.Id;
                }
            }
            else id= Program.dbOperations.AddUL_Feature(ulf);

            if (id > 0)
            {
                ulf.Id = id;
                lstULFs.Add(ulf);

                dgvData.DataSource = await GetData();
                ColorDgv();
                ShowData();
                int iNewRow = dgvData.Rows.Count - 1;
                dgvData.CurrentCell = dgvData["Unique_Phrase", iNewRow];
                dgvData.Focus();
            }

            Application.DoEvents();
            if (!b2) chkShowUpdateMessage.Checked = true;
        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.SelectionMode = chkCanEdit.Checked ? DataGridViewSelectionMode.RowHeaderSelect
                : DataGridViewSelectionMode.FullRowSelect;
            dgvData.ReadOnly = !chkCanEdit.Checked;

            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
            ShowData();
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
                Application.DoEvents();
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        object InitailValue = null;
        private void DgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            CancelButton = null;

            InitailValue = dgvData[e.ColumnIndex, e.RowIndex].Value;//.ToString();
        }

        private async void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool bSaveChange = true;   // آیا تغییر ذخیره شود؟
            bool bEnbaled_Changed = false;

            if (e.RowIndex < 0 || e.ColumnIndex < 0) bSaveChange = false;
            if (dgvData[e.ColumnIndex, e.RowIndex].Value == InitailValue) bSaveChange = false;

            if (!bSaveChange)
            {
                CancelButton = btnReturn;
                return;
            }

            panel1.Enabled = !Stack.Use_Web;
            pictureBox3.Visible = Stack.Use_Web;

            long id = Convert.ToInt64(dgvData["Id", e.RowIndex].Value);

            Models.UL_Feature ul_feature = lstULFs.Find(d => d.Id == id);// Program.dbOperations.GetUL_FeatureAsync(id);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Unique_Phrase":
                    ul_feature.Unique_Phrase = Convert.ToString(dgvData["Unique_Phrase", e.RowIndex].Value);
                    break;
                case "Description":
                    ul_feature.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
                    break;
                case "Enabled":
                    ul_feature.Enabled = Convert.ToBoolean(dgvData["Enabled", e.RowIndex].Value);
                    bEnbaled_Changed = true;
                    break;
            }

            if (bSaveChange)
            {
                // برای ذخیره تغییرات در ردیف جدید ، پیغامی نمایش داده نشود
                if (chkCanEdit.Checked)
                {
                    if (chkShowUpdateMessage.Checked)
                    {
                        bSaveChange = MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                            , "", MessageBoxButtons.YesNo) == DialogResult.Yes;
                    }
                }
            }

            if (bSaveChange)
            {
                if (Stack.Use_Web)
                {
                    var res = await HttpClientExtensions.PutAsJsonAsync
                        (Stack.API_Uri_start_read + "/UL_Feature/" + ul_feature.Id, ul_feature, Stack.token);
                    if (res.IsSuccessStatusCode)
                    {
                        pictureBox3.Visible = false;
                        panel1.Enabled = true;
                    }
                }
                else
                    Program.dbOperations.UpdateUL_FeatureAsync(ul_feature);

                #region اگر فعال بودن امکانی تغییر نماید
                if (bEnbaled_Changed)
                {
                    panel1.Enabled = false;
                    Application.DoEvents();

                    List<Models.User_Level_UL_Feature> lstULULF = new List<Models.User_Level_UL_Feature>();

                    if (Stack.Use_Web)
                    {
                        lstULULF = await HttpClientExtensions.GetT<List<Models.User_Level_UL_Feature>>
                            (Stack.API_Uri_start_read + "/User_Level_UL_Feature?all=no&company_Id=" + Stack.Company_Id, Stack.token);
                    }
                    else
                        lstULULF = Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Id, 0, 0);

                    lstULULF = lstULULF.Where(d => d.UL_Feature_Id == ul_feature.Id).ToList();

                    foreach (Models.User_Level_UL_Feature ul_ulf in lstULULF)
                    {
                        ul_ulf.UL_Feature_Enabled = ul_feature.Enabled;

                        if (Stack.Use_Web)
                            await HttpClientExtensions.PutAsJsonAsync(Stack.API_Uri_start_read
                                + "/User_Level_UL_Feature", ul_feature, Stack.token);
                        else
                            Program.dbOperations.UpdateUser_Level_UL_FeatureAsync(ul_ulf);
                    }
                    Application.DoEvents();
                    panel1.Enabled = true;

                }
                #endregion
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;

            CancelButton = btnReturn;
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (Stack.Use_Web)
            {
                MessageBox.Show("این امکان فعال نمی باشد");
                return;
            }
            else
            {
                if (MessageBox.Show("آیا از حذف همه سطوح اطمینان دارید؟"
                    , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.No) return;

                if (MessageBox.Show("با انجام این عمل ، تمام روابط سطوح کاربری و جداول دیگر از بین خواهد رفت"
                    + "\n" + "آیا از حذف تمام سطوح اطمینان دارید؟", "اخطار 2"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                Program.dbOperations.DeleteAllUL_FeaturesAsync();
                Program.dbOperations.DeleteAllUser_Level_UL_FeaturesAsync();
                dgvData.DataSource = GetData();
                ColorDgv();
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Unique_Phrase.Text)
                && string.IsNullOrWhiteSpace(txtST_Description.Text))
                return;

            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.UL_Feature> lstUL_Features = (List<Models.UL_Feature>)dgvData.DataSource;
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstUL_Features = SearchThis(lstUL_Features, c.Name);
                                if ((lstUL_Features == null) || !lstUL_Features.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstUL_Features;

            Application.DoEvents();
            panel1.Enabled = true;
        }

        // جستجوی موردی
        private List<Models.UL_Feature> SearchThis(List<Models.UL_Feature> lstUL_Feature1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Unique_Phrase":
                    switch (cmbST_Unique_Phrase.SelectedIndex)
                    {
                        case 0:
                            return lstUL_Feature1.Where(d => d.Unique_Phrase.ToLower().Contains(txtST_Unique_Phrase.Text.ToLower())).ToList();
                        case 1:
                            return lstUL_Feature1.Where(d => d.Unique_Phrase.ToLower().StartsWith(txtST_Unique_Phrase.Text.ToLower())).ToList();
                        case 2:
                            return lstUL_Feature1.Where(d => d.Unique_Phrase.ToLower().Equals(txtST_Unique_Phrase.Text.ToLower())).ToList();
                        default: return lstUL_Feature1;
                    }
                case "txtST_Description":
                    switch (cmbST_Description.SelectedIndex)
                    {
                        case 0:
                            return lstUL_Feature1.Where(d => d.Description.ToLower().Contains(txtST_Description.Text.ToLower())).ToList();
                        case 1:
                            return lstUL_Feature1.Where(d => d.Description.ToLower().StartsWith(txtST_Description.Text.ToLower())).ToList();
                        case 2:
                            return lstUL_Feature1.Where(d => d.Description.ToLower().Equals(txtST_Description.Text.ToLower())).ToList();
                        default: return lstUL_Feature1;
                    }
            }

            return null;
        }

        private async void tsmiDelete_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            Models.UL_Feature uL_Feature = lstULFs.Find(d => d.Id == id);// Program.dbOperations.GetUL_FeatureAsync(id);

            if (MessageBox.Show("آیا از حذف این قابلیت اطمینان دارید؟"
               , uL_Feature.Unique_Phrase, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
               == DialogResult.No) return;

            if(Stack.Use_Web)
                await HttpClientExtensions.DeleteAsJsonAsync2(Stack.API_Uri_start_read
                    + "/User_Level_UL_Feature/"+ id, Stack.token);
            else
                Program.dbOperations.DeleteUL_FeatureAsync(uL_Feature);

            lstULFs.Remove(uL_Feature);
            dgvData.DataSource = await GetData();
            ColorDgv();

            pictureBox1.Visible = true;
            Application.DoEvents();
            timer1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            timer1.Enabled = false;
        }

        private void J2210_UL_Features_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnReturn.Focus();
        }

        private void RadEnabled_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
            ColorDgv();
        }

        private async void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = await GetData();
            ColorDgv();
        }

    }
}
