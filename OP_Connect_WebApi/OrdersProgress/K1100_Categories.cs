﻿using System;
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
    public partial class K1100_Categories : X210_ExampleForm_Normal
    {
        List<Models.Category> categories = new List<Models.Category>();

        public K1100_Categories()
        {
            InitializeComponent();

            panel2.Visible = (Stack.UserLevel_Type==1) || Stack.lstUser_ULF_UniquePhrase.Contains("jn3110");
            btnAddNew.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jn3120");
            tsmiDelete.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jn3130");
        }

        private async void K1100_Categories_Shown(object sender, EventArgs e)
        {
            cmbST_Name.SelectedIndex = 0;

            dgvData.DataSource = await GetData();
            ShowData();

            Application.DoEvents();
            panel1.Enabled = true;
        }

        private async Task<List<Models.Category>> GetData(bool bForceReset = false)
        {
            if (!categories.Any() || bForceReset)
            {
                if (Stack.Use_Web)
                    categories= await HttpClientExtensions.GetT<List<Models.Category>>
                        (Stack.API_Uri_start_read + "/Categories?all=no&company_Id=" + Stack.Company_Id, Stack.token);
                else
                    categories= Program.dbOperations.GetAllCategoriesAsync(Stack.Company_Id);
            }

            return categories;
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                { 
                    case "Name":
                        col.HeaderText = "نام دسته";
                        col.Width = 150;
                        break;
                    case "Description":
                        col.HeaderText = "توضیحات";
                        col.Width = 200;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Name.Text))
                return;

            panel1.Enabled = false;
            //dgvData.Visible = false;
            Application.DoEvents();

            //ShowData(false);
            List<Models.Category> lstCats = (List<Models.Category>)dgvData.DataSource;
            //MessageBox.Show(lstItems.Count.ToString());

            foreach (Control c in groupBox1.Controls)
            {
                //MessageBox.Show(c.Text);
                if (c.Name.Length > 4)
                {
                    if (c.Name.Substring(0, 5).Equals("txtST"))
                        if (!string.IsNullOrWhiteSpace(c.Text))
                        {
                            lstCats = SearchThis(lstCats, c.Name);
                            if ((lstCats == null) || !lstCats.Any()) break;
                        }
                }
            }

            dgvData.DataSource = lstCats;

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.Category> SearchThis(List<Models.Category> lstCats1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstCats1.Where(d => d.Name.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstCats1.Where(d => d.Name.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstCats1.Where(d => d.Name.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstCats1;
                    }
            }

            return null;
        }

        private async void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = await GetData();
        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.ReadOnly = !chkCanEdit.Checked;
            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
            ShowData();
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
            pictureBox3.Visible = Stack.Use_Web;
            panel1.Enabled = !Stack.Use_Web;

            int id = Convert.ToInt32(dgvData["Id", e.RowIndex].Value);

            Models.Category cat =  categories.First(d=>d.Id == id);

            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Name":
                    cat.Name = Convert.ToString(dgvData["Name", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(cat.Name))
                        return;
                    #region اگر دسته دیگری با این نام تعریف شده باشد
                    else if (categories.Where(d => d.Id != id).Any(j => j.Name.ToLower().Equals(cat.Name.ToLower())))
                    {
                        bSaveChange = false;
                        MessageBox.Show("نام دسته تکراری بوده و قبلا استفاده شده است", "خطا");
                        #endregion
                    }
                    break;
                case "Description":
                    cat.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
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
                        (Stack.API_Uri_start_read + "/Categories/"+id, cat, Stack.token);
                    if (res.IsSuccessStatusCode)
                    {
                        pictureBox3.Visible = false;
                        panel1.Enabled = true;
                    }

                }
                else Program.dbOperations.UpdateCategoryAsync(cat);

                dgvData.DataSource = await GetData(true);
                dgvData.CurrentCell = dgvData["Name", e.RowIndex];
                //categories.Remove(categories.First(d => d.Id == cat.Id));
                //categories.Add(cat);
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        int iX = 0, iY = 0;
        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            //if (!btnDeleteAll.Visible) return;

            iX = e.X;
            iY = e.Y;
        }

        private void dgvData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (!btnDeleteAll.Visible) return;

            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                // انتخاب سلولی که روی آن کلیک راست شده است
                dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(dgvData, new Point(iX, iY));
            }
        }

        private async void TsmiDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvData.CurrentRow.Cells["Id"].Value);
            //dgvData.CurrentCell = null;
            //MessageBox.Show(id.ToString());
            if(Program.dbOperations.GetAllItemsAsync(Stack.Company_Id,0,100).Any(d=>d.Category_Id==id))
            {
                MessageBox.Show("کالا(ها)یی در این دسته تعریف شده اند. امکان حذف این دسته وجود ندارد","خطا");
                return;
            }

            Models.Category cat = categories.First(d=>d.Id == id);

            if (MessageBox.Show("آیا از حذف دسته اطمینان دارید؟", cat.Name,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            if (MessageBox.Show("با انجام این عمل ، تمام کالاهایی که در این دسته تعریف شده اند، بلاتکلیف خواهند شد.آیا از حذف دسته اطمینان دارید؟"
                , cat.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            if (Stack.Use_Web)
            {
                await HttpClientExtensions.DeleteAsJsonAsync2(Stack.API_Uri_start_read
                     + "/Categories/" + id, Stack.token);
            }
            else
                Program.dbOperations.DeleteCategoryAsync(cat);

            //categories.Remove(cat);
            dgvData.DataSource = await GetData(true);
        }

        private async void BtnAddNew_Click(object sender, EventArgs e)
        {
            //long index = Program.dbOperations.GetNewIndex_Category();
            Models.Category category = new Models.Category
            {
                Company_Id = Stack.Company_Id,
                Name = "دسته x",// + index,
                //Description = "؟",
                //Active = true,
            };

            bool Add_OK = false;
            if (Stack.Use_Web)
            {
                var response = await HttpClientExtensions.PostAsJsonAsync
                                  (Stack.API_Uri_start_read + "/Categories", category, Stack.token);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("اشکال در ثبت اطلاعات", "خطا");
                    return;
                }
                else
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    Models.Category cat1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Category>(responseString);
                    categories.Add(cat1);
                    Add_OK = true;
                }
            }
            else
            {
                int id = Program.dbOperations.AddCategory(category);
                category.Id = id;
                categories.Add(category);
                Add_OK = true;
            }

            if (Add_OK)
            {
                chkCanEdit.Checked = true;
                dgvData.DataSource = await GetData(true);
                Application.DoEvents();
                //ShowData();
                int iNewRow = dgvData.Rows.Count - 1;
                dgvData.CurrentCell = dgvData["Name", iNewRow];
                dgvData.Focus();
            }
        }

        private void TxtST_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSearch;
        }

        private void TxtST_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }






        //

    }
}
