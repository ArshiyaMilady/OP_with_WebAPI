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
    public partial class J2000_Users : X210_ExampleForm_Normal
    {
        List<Models.User> lstUsers = new List<Models.User>();

        public J2000_Users()
        {
            InitializeComponent();
        }

        private async void J2000_Users_Shown(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 2;
            cmbST_Name.SelectedIndex = 0;
            cmbST_Mobile.SelectedIndex = 0;
            cmbST_Phone.SelectedIndex = 0;
            cmbST_Address.SelectedIndex = 0;

            panel2.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jk1000");
            tsmiSetUserLevel.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jk1000");
            tsmiChangePassword.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jk1000");
            btnDeleteAll.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jk1000");
            tsmiSetUserLevel.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jk1000");
            tsmiLoginsHistory.Visible = Stack.UserLevel_Type != 0;

            btnDeleteAll.Visible = (Stack.UserLevel_Type == 1);
            btnAddNew.Visible = (Stack.UserLevel_Type == 1) || (Stack.UserLevel_Type == 2);


            dgvData.DataSource = await GetData();
            ShowData();
            Application.DoEvents();
            panel1.Enabled = true;
        }

        private async Task<List<Models.User>> GetData()
        {
            // اگر لیست خالی است
            if(!lstUsers.Any())
            {
                if (Stack.UserLevel_Type == 0)
                {
                    List<Models.UL_See_UL> lstUL_See_ULs = new List<Models.UL_See_UL>();
                    List<Models.User> lstAllUsers = new List<Models.User>();
                    List<Models.User_UL> lstAllUUL = new List<Models.User_UL>();

                    if(Stack.Use_Web)   // استفاده از اینترنت
                    {
                        lstUL_See_ULs = await HttpClientExtensions.GetT<List<Models.UL_See_UL>>
                            (Stack.API_Uri_start_read + "/UL_See_UL?company_id=" + Stack.Company_Id
                            + "&main_ul_id=" + Stack.UserLevel_Id, Stack.token);
                        lstAllUsers = await HttpClientExtensions.GetT<List<Models.User>>
                            (Stack.API_Uri_start_read + "/Users?all=no&company_id=" + Stack.Company_Id, Stack.token);
                        lstAllUUL = await HttpClientExtensions.GetT<List<Models.User_UL>>
                            (Stack.API_Uri_start_read + "/User_UL?all=no&company_id=" + Stack.Company_Id, Stack.token);
                    }
                    else
                    {
                        lstUL_See_ULs = Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Id, Stack.UserLevel_Id);
                        lstAllUsers = Program.dbOperations.GetAllUsersAsync(Stack.Company_Id, 0);
                        lstAllUUL = Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id);
                    }

                    foreach (Models.User user in lstAllUsers)
                    {
                        List<Models.User_UL> lstUUL = lstAllUUL.Where(d => d.User_Id == user.Id).ToList();

                        foreach (Models.User_UL user_ul in lstUUL)
                        {
                            if (lstUL_See_ULs.Any(d => d.UL_Id == user_ul.UL_Id))
                                lstUsers.Add(user);
                        }
                    }
                }
                else  // برای سطح کاربرانی که نوع سطح آنها غیر صفر است مانند ادمین ها و کاربران ارشد
                {
                    lstUsers = await WhichUsers(Stack.UserLevel_Type);
                }


            }

            switch (comboBox1.SelectedIndex)
            {
                case 0:  // کاربران غیرفعال
                    return lstUsers.Where(d => !d.Active).ToList();
                case 2:  // کاربران فعال
                    return lstUsers.Where(d => d.Active).ToList();
            }

            return lstUsers;
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "Active":
                        col.HeaderText = "فعال؟";
                        col.Width = 50;
                        break;
                    case "Index":
                        if (Stack.UserLevel_Type == 1)
                        {
                            col.HeaderText = "Id";
                            col.Width = 50;
                        }
                        else col.Visible = false;
                        break;
                    case "Name":
                        col.HeaderText = "شناسه";
                        col.Width = 150;
                        break;
                    case "Real_Name":
                        col.HeaderText = "نام";
                        col.Width = 200;
                        break;
                    case "User_Level":
                        col.HeaderText = "سطح دسترسی";
                        col.Width = 100;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "Mobile":
                        col.HeaderText = "همراه";
                        col.Width = 100;
                        break;
                    case "Phone":
                        col.HeaderText = "تلفن ثابت";
                        col.Width = 100;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "EMail":
                        col.HeaderText = "ایمیل";
                        col.Width = 100;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "Address":
                        col.HeaderText = "آدرس";
                        col.Width = 100;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "User_Domain":
                        col.HeaderText = "Domain";
                        col.Width = 60;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "Description":
                        col.HeaderText = "توضیحات";
                        col.Width = 100;
                        break;
                    //case "UserLevel_Id":
                    //    col.HeaderText = "شناسۀ سطح دسترسی";
                    //    col.Width = 100;
                    //    break;
                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        private async void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف همه کاربران اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

            if(Stack.Use_Web)
            {
                await HttpClientExtensions.DeleteAsJsonAsync2(Stack.API_Uri_start
                    + "/Users/0?company_id=" + Stack.Company_Id, Stack.token);
            }
            else
                Program.dbOperations.DeleteAllUsersAsync();


            dgvData.DataSource = GetData();

            pictureBox1.Visible = true;
            Application.DoEvents();
            timer1.Enabled = true;
        }

        private async void TsmiDelete_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            Models.User user = Program.dbOperations.GetUserAsync(id);

            if (MessageBox.Show("آیا از حذف این کاربر اطمینان دارید؟"
               , user.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
               == DialogResult.No) return;

            bool Delete_OK = false;
            if(Stack.Use_Web)
            {
                Delete_OK = await HttpClientExtensions.DeleteAsJsonAsync2(Stack.API_Uri_start
                     + "/Users/" + id, Stack.token) != null;
            }
            else
                Delete_OK = Program.dbOperations.DeleteUser(user) > 0;

            if (Delete_OK)
            {
                user = lstUsers.First(d => d.Id == user.Id);
                lstUsers.Remove(user);

                //MessageBox.Show(lstUsers.Count.ToString());
                dgvData.DataSource = GetData();
            }

            pictureBox1.Visible = true;
            Application.DoEvents();
            timer1.Enabled = true;
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

            InitailValue = dgvData[e.ColumnIndex, e.RowIndex].Value;//.ToString();
        }

        private async void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (Convert.ToString(dgvData[e.ColumnIndex, e.RowIndex].Value).Equals(Convert.ToString(InitailValue))) return;

            panel1.Enabled = !Stack.Use_Web;
            pictureBox3.Visible = Stack.Use_Web;

            bool bSaveChange = true;   // آیا تغییر ذخیره شود؟

            long id = Convert.ToInt64(dgvData["Id", e.RowIndex].Value);

            Models.User user = lstUsers.First(d => d.Id == id); // Program.dbOperations.GetUserAsync(id);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Active":
                    user.Active = Convert.ToBoolean(dgvData["Active", e.RowIndex].Value);
                    break;
                case "Name":
                    user.Name = Convert.ToString(dgvData["Name", e.RowIndex].Value);
                    if (string.IsNullOrWhiteSpace(user.Name)) return;
                    #region اگر کاربر دیگر با این نام تعریف شده باشد
                    else if (Program.dbOperations.GetAllUsersAsync(Stack.Company_Id, 0).Where(d => d.Id != id)
                        .Any(j => j.Name.ToLower().Equals(user.Name.ToLower())))
                    {
                        bSaveChange = false;
                        MessageBox.Show("نام کاربر تکراری است", "خطا");
                    }
                    #endregion
                    break;
                case "Real_Name":
                    user.Real_Name = Convert.ToString(dgvData["Real_Name", e.RowIndex].Value);
                    break;
                case "User_Domain":
                    user.User_Domain = Convert.ToString(dgvData["User_Domain", e.RowIndex].Value);
                    break;
                case "Mobile":
                    //bSaveChange = false;
                    user.Mobile = Convert.ToString(dgvData["Mobile", e.RowIndex].Value);
                    if (Stack.UserLevel_Type != 1)
                    {
                        if (user.Mobile.Trim().Length < 10)
                        {
                            MessageBox.Show("لطفا شماره موبایل را بدرستی وارد نمایید", "خطا");
                            bSaveChange = false;
                        }
                        else if (Program.dbOperations.GetAllUsersAsync(Stack.Company_Id, 0).Where(d => d.Id != id)
                            .Where(j => j.Mobile != null).Where(n => n.Mobile.Length >= 10)
                            .Any(q => q.Mobile.Substring(q.Mobile.Length - 10)
                            .Equals(user.Mobile.Substring(q.Mobile.Length - 10))))
                        {
                            MessageBox.Show("این شماره همراه قبلا وارد شده است", "خطا");
                            bSaveChange = false;
                        }
                    }
                    break;
                case "Phone":
                    user.Phone = Convert.ToString(dgvData["Phone", e.RowIndex].Value);
                    break;
                case "EMail":
                    user.EMail = Convert.ToString(dgvData["EMail", e.RowIndex].Value);
                    break;
                case "Address":
                    user.Address = Convert.ToString(dgvData["Address", e.RowIndex].Value);
                    break;
                case "UserLevel_Id":
                    user.UserLevel_Id = Convert.ToInt64(dgvData["UserLevel_Id", e.RowIndex].Value);
                    break;
            }

            if (bSaveChange)
            {
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
                if(Stack.Use_Web)
                {
                    var res = await HttpClientExtensions.PutAsJsonAsync<Models.User>(Stack.API_Uri_start
                        + "/Users/"+user.Id, user, Stack.token);
                    if(res.IsSuccessStatusCode)
                    {
                        pictureBox3.Visible = false;
                        panel1.Enabled = true;
                    }
                    //MessageBox.Show((await HttpClientExtensions.PutAsJsonAsync<Models.User>(Stack.API_Uri_start
                    //    + "/Users/"+user.Id, user, Stack.token)).StatusCode.ToString());
                }
                else
                    Program.dbOperations.UpdateUserAsync(user);

                lstUsers.Remove(lstUsers.First(d => d.Id == id));
                lstUsers.Add(user);
            }
            else
            {
                if (InitailValue != null)
                dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
            }

            InitailValue = null;
        }


        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TsmiChangePassword_Click_1(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            new J2110_ChangePassword(id).ShowDialog();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            if (!chkCanEdit.Checked)
                chkCanEdit.Checked = true;

            bool b2 = true;
            if (chkShowUpdateMessage.Checked)
                b2 = chkShowUpdateMessage.Checked = false;

            //long index = Program.dbOperations.GetNewIndex_User();
            long ii = 1;
            string name = "شناسه " + ii;
            while(Program.dbOperations.GetUserAsync(name)!=null)
                name = "شناسه-"  + (ii++);

            Models.User user= new Models.User
            {
                Company_Id = Stack.Company_Id,
                //Index = index,
                Name = name,
                Real_Name = "؟",
                DateTime_mi=DateTime.Now,
                DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                Active = true,
                Password = new CryptographyProcessor().GenerateHash("1111",Stack.Standard_Salt),
            };
            
            //if (index > 0)
            {
                Program.dbOperations.AddUser(user);
                lstUsers.Add(user);

                dgvData.DataSource = GetData();
                ShowData();
                int iNewRow = dgvData.Rows.Count - 1;
                //dgvData["Id", iNewRow].Value = index;
                dgvData.CurrentCell = dgvData["Real_Name", iNewRow];
                dgvData.Focus();
            }

            Application.DoEvents();
            if (!b2) chkShowUpdateMessage.Checked = true;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Name.Text)
                && string.IsNullOrWhiteSpace(txtST_Mobile.Text)
                 && string.IsNullOrWhiteSpace(txtST_Phone.Text)
                  && string.IsNullOrWhiteSpace(txtST_Address.Text))
            {
                //ShowData(false);
                return;
            }

            panel1.Enabled = false;
            //dgvData.Visible = false;
            Application.DoEvents();

            //ShowData(false);
            List<Models.User> lstUsers = (List<Models.User>)dgvData.DataSource;
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
                                lstUsers = SearchThis(lstUsers, c.Name);
                                if ((lstUsers == null) || !lstUsers.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstUsers;

            //System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            panel1.Enabled = true;
            //dgvData.Visible = true;

        }

        // جستجوی موردی
        private List<Models.User> SearchThis(List<Models.User> lstUsers1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Name":
                    switch (cmbST_Name.SelectedIndex)
                    {
                        case 0:
                            return lstUsers1.Where(d => d.Real_Name.ToLower().Contains(txtST_Name.Text.ToLower())).ToList();
                        case 1:
                            return lstUsers1.Where(d => d.Real_Name.ToLower().StartsWith(txtST_Name.Text.ToLower())).ToList();
                        case 2:
                            return lstUsers1.Where(d => d.Real_Name.ToLower().Equals(txtST_Name.Text.ToLower())).ToList();
                        default: return lstUsers1;
                    }
                case "txtST_Mobile":
                    switch (cmbST_Mobile.SelectedIndex)
                    {
                        case 0:
                            return lstUsers1.Where(d => d.Mobile.ToLower().Contains(txtST_Mobile.Text.ToLower())).ToList();
                        case 1:
                            return lstUsers1.Where(d => d.Mobile.ToLower().StartsWith(txtST_Mobile.Text.ToLower())).ToList();
                        case 2:
                            return lstUsers1.Where(d => d.Mobile.ToLower().Equals(txtST_Mobile.Text.ToLower())).ToList();
                        default: return lstUsers1;
                    }
                case "txtST_Phone":
                    switch (cmbST_Phone.SelectedIndex)
                    {
                        case 0:
                            return lstUsers1.Where(d => d.Phone.ToLower().Contains(txtST_Phone.Text.ToLower())).ToList();
                        case 1:
                            return lstUsers1.Where(d => d.Phone.ToLower().StartsWith(txtST_Phone.Text.ToLower())).ToList();
                        case 2:
                            return lstUsers1.Where(d => d.Phone.ToLower().Equals(txtST_Phone.Text.ToLower())).ToList();
                        default: return lstUsers1;
                    }
                case "txtST_Address":
                    switch (cmbST_Address.SelectedIndex)
                    {
                        case 0:
                            return lstUsers1.Where(d => d.Address.ToLower().Contains(txtST_Address.Text.ToLower())).ToList();
                        case 1:
                            return lstUsers1.Where(d => d.Address.ToLower().StartsWith(txtST_Address.Text.ToLower())).ToList();
                        case 2:
                            return lstUsers1.Where(d => d.Address.ToLower().Equals(txtST_Address.Text.ToLower())).ToList();
                        default: return lstUsers1;
                    }
                    //break;
            }

            return null;
        }

        private void ChkCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            dgvData.SelectionMode = chkCanEdit.Checked ? DataGridViewSelectionMode.RowHeaderSelect
                : DataGridViewSelectionMode.FullRowSelect;
            dgvData.ReadOnly = !chkCanEdit.Checked;
            chkShowUpdateMessage.Enabled = chkCanEdit.Checked;
            ShowData();
        }

        private void TsmiSetUserLevel_Click(object sender, EventArgs e)
        {
            long index = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            // مراجعه به فرم تعیین سطح کاربری
            new J2204_User_UL(index).ShowDialog();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
        }

        private void TsmiLoginsHistory_Click(object sender, EventArgs e)
        {
            long user_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            new J1960_LoginsHistory(user_index).ShowDialog();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            timer1.Enabled = false;
        }






        public async Task<List<Models.User>> WhichUsers(int ul_type)
        {
            List<Models.User> lstUsers = new List<Models.User>();

            if(Stack.Use_Web)
                lstUsers = await HttpClientExtensions.GetT<List<Models.User>>
                    (Stack.API_Uri_start_read + "/Users?all=no&company_id=" + Stack.Company_Id, Stack.token);
            else  lstUsers = Program.dbOperations.GetAllUsersAsync(Stack.Company_Id, 0);

            if (lstUsers.Any() && (Stack.UserLevel_Type > 1))
            {
                List<Models.User_Level> lstUL_Type_notSeen = new List<Models.User_Level>();
                if (Stack.Use_Web)
                    lstUL_Type_notSeen = (await HttpClientExtensions.GetT<List<Models.User_Level>>
                    (Stack.API_Uri_start_read + "/User_Levels?all=no&company_id=" + Stack.Company_Id, Stack.token))
                    .Where(d=>d.Enabled).ToList();
                else
                    lstUL_Type_notSeen = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id, 1);

                List<long> lstUL_Type_notSeen_Id = lstUL_Type_notSeen
                    .Where(d => (d.Type > 0) && (d.Type <= Stack.UserLevel_Type)).Select(d => d.Id).ToList();
                List<long> lstUsersUL_Type_Seen_Id = Program.dbOperations.GetAllUser_ULsAsync
                    (Stack.Company_Id).Where(d => !lstUL_Type_notSeen_Id.Contains(d.UL_Id)).Select(d => d.User_Id).ToList();
                lstUsers = lstUsers.Where(d => lstUsersUL_Type_Seen_Id.Contains(d.Id)).ToList();
            }
            return lstUsers;
        }


       

    }
}
