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
    public partial class J2200_Users_Levels : X210_ExampleForm_Normal
    {
        // دسترسی هایی که کاربر بنا به سطح کاربری خود می تواند داشته باشد
        //List<Models.User_Level> lstUL_Result = new List<Models.User_Level>();
        List<Models.User_Level> lstULs = new List<Models.User_Level>();

        public J2200_Users_Levels()
        {
            InitializeComponent();

            Height = 500;
        }

        private async void J2200_Users_Levels_Shown(object sender, EventArgs e)
        {
            cmbST_Description.SelectedIndex = 0;
            panel2.Visible = (Stack.UserLevel_Type == 1) || (Stack.UserLevel_Type == 2)
                || Stack.lstUser_ULF_UniquePhrase.Contains("jk2120");
            btnAddNew.Visible = (Stack.UserLevel_Type == 1) || (Stack.UserLevel_Type == 2)
                || Stack.lstUser_ULF_UniquePhrase.Contains("jk2120");
            tsmiDeleteAllFeatures.Visible = (Stack.UserLevel_Type == 1) || (Stack.UserLevel_Type == 2);

            if (Stack.Use_Web)
                dgvData.DataSource = await GetData_web();
            else
                dgvData.DataSource = GetData();

            ShowData();
        }

        private  List<Models.User_Level> GetData()
        {
            if (!lstULs.Any())
            {
                if (Stack.UserLevel_Type == 0)
                {
                    List<Models.UL_See_UL> lstULSUL = Program.dbOperations
                        .GetAllUL_See_ULsAsync(Stack.Company_Id, Stack.UserLevel_Id);
                    List<Models.User_Level> lstUL = Program.dbOperations
                        .GetAllUser_LevelsAsync(Stack.Company_Id);

                    foreach (Models.UL_See_UL ul_see_ul in lstULSUL)
                    {
                        //Models.User_Level ul = lstUL.First(d => d.Id == ul_see_ul.UL_Id);
                        lstULs.Add(lstUL.First(d => d.Id == ul_see_ul.UL_Id));
                    }
                }
                else if (Stack.UserLevel_Type == 1)
                    lstULs = Program.dbOperations.GetAllUser_LevelsAsync(0);// Stack.Company_Id,0).ToList();
                else if (Stack.UserLevel_Type == 2)
                    lstULs = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id, 0)
                        .Where(d => (d.Type != 1) && (d.Type != 2)).ToList();
            }

            if (radEnabledLevel.Checked) return lstULs.Where(d => d.Enabled).OrderByDescending(d => d.C_B1).ToList();
            else if (radDisabledLevel.Checked) return lstULs.Where(d => !d.Enabled).OrderByDescending(d => d.C_B1).ToList();
            else return lstULs.OrderByDescending(d => d.C_B1).ToList();
        }

        private async Task<List<Models.User_Level>> GetData_web()
        {
            if (!lstULs.Any())
            {
                List<Models.User_Level> lstAllUL = await HttpClientExtensions.GetT<List<Models.User_Level>>
                   (Stack.API_Uri_start_read + "/User_Levels?all=yes", Stack.token);

                if (Stack.UserLevel_Type == 0)
                {
                    List<Models.UL_See_UL> lstULSUL = await HttpClientExtensions.GetT<List<Models.UL_See_UL>>
                        (Stack.API_Uri_start_read + "/UL_See_UL?all=no&company_id=" + Stack.Company_Id
                        + "&main_ul_id=" + Stack.UserLevel_Id, Stack.token);

                    lstAllUL = lstAllUL.Where(d=>d.Company_Id == Stack.Company_Id).Where(d => d.Enabled).ToList();

                    foreach (Models.UL_See_UL ul_see_ul in lstULSUL)
                    {
                        //Models.User_Level ul = lstUL.First(d => d.Id == ul_see_ul.UL_Id);
                        lstULs.Add(lstAllUL.First(d => d.Id == ul_see_ul.UL_Id));
                    }
                }
                else
                {
                    if (Stack.UserLevel_Type == 1)
                        lstULs = lstAllUL;
                    else
                    if (Stack.UserLevel_Type == 2)
                        lstULs = lstAllUL.Where(d => d.Company_Id == Stack.Company_Id)
                            .Where(d => (d.Type != 1) && (d.Type != 2)).ToList();
                }
            }

            if (radEnabledLevel.Checked) return lstULs.Where(d => d.Enabled).OrderByDescending(d => d.C_B1).ToList();
            else if (radDisabledLevel.Checked) return lstULs.Where(d => !d.Enabled).OrderByDescending(d => d.C_B1).ToList();
            else return lstULs.OrderByDescending(d => d.C_B1).ToList();
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "Id":
                        if (Stack.UserLevel_Type == 1)
                        {
                            col.HeaderText = "شناسه";
                            col.ReadOnly = true;
                            col.Width = 50;
                        }
                        else col.Visible = false;
                        break;
                    case "Company_Id":
                        if (Stack.UserLevel_Type == 1)
                        {
                            col.HeaderText = "شناسه شرکت";
                            col.ReadOnly = true;
                            col.Width = 100;
                        }
                        else col.Visible = false;
                        break;
                    case "Description":
                        col.HeaderText = "شرح";
                        col.Width = 200;
                        break;
                    case "Unit_Name":
                        col.HeaderText = "نام واحد";
                        col.Width = 120;
                        break;
                    case "Enabled":
                        col.HeaderText = "فعال؟";
                        col.Width = 50;
                        //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case "Type":
                        if (Stack.UserLevel_Type == 1)
                        {
                            col.HeaderText = "Type";
                            col.Width = 50;
                        }
                        else col.Visible = false;
                        break;
                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        private async void BtnAddNew_Click(object sender, EventArgs e)
        {
            long id = -1;
            Models.User_Level user_level = new Models.User_Level
            {
                Company_Id = Stack.Company_Id,
                Description = "؟",
                Unit_Name="؟",
                Enabled = true,
            };

            if (Stack.Use_Web)
            {
                var response = await HttpClientExtensions.PostAsJsonAsync
                    (Stack.API_Uri_start_read + "/User_Levels", user_level, Stack.token);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("اشکال در ثبت اطلاعات", "خطا");
                    return;
                }
                else
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    Models.User_Level ul1 = JsonConvert.DeserializeObject<Models.User_Level>(responseString);
                    id = ul1.Id;
                }
            }
            else id = Program.dbOperations.AddUser_Level(user_level);

            if (id > 0)
            {
                user_level.Id = id;
                lstULs.Add(user_level);

                if (Stack.Use_Web) dgvData.DataSource = await GetData_web();
                else dgvData.DataSource = GetData();

                ShowData();
                int iNewRow = dgvData.Rows.Count - 1;
                dgvData.CurrentCell = dgvData["Description", iNewRow];
                dgvData.Focus();
            }
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
            dgvData.CurrentCell = dgvData[e.ColumnIndex, e.RowIndex];
            if (e.Button == MouseButtons.Right)
            {
                /////// Do something ///////
                //long ul_id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
                //Models.User_Level user_level = lstULs.First(d => d.Id == ul_id);// Program.dbOperations.GetUser_LevelAsync(ul_id);
                tsmiDelete.Visible = (Stack.UserLevel_Type == 1) || (Stack.UserLevel_Type == 2);// || (user_level.Type == 0);

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
            if (dgvData[e.ColumnIndex, e.RowIndex].Value == InitailValue) return;

            bool bSaveChange = true;   // آیا تغییر ذخیره شود؟
            panel1.Enabled = !Stack.Use_Web;
            pictureBox3.Visible = Stack.Use_Web;

            long id = Convert.ToInt64(dgvData["Id", e.RowIndex].Value);

            Models.User_Level user_level = lstULs.First(d => d.Id == id);//  Program.dbOperations.GetUser_LevelAsync(id);
            switch (dgvData.Columns[e.ColumnIndex].Name)
            {
                case "Enabled":
                    user_level.Enabled = Convert.ToBoolean(dgvData["Enabled", e.RowIndex].Value);
                    break;
                case "Description":
                    user_level.Description = Convert.ToString(dgvData["Description", e.RowIndex].Value);
                    break;
                case "Unit_Name":
                    user_level.Unit_Name = Convert.ToString(dgvData["Unit_Name", e.RowIndex].Value);
                    break;
                case "Type":
                    user_level.Type = Convert.ToInt32(dgvData["Type", e.RowIndex].Value);
                    break;
            }

            if (bSaveChange)
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

            if (bSaveChange)
            {
                if (Stack.Use_Web)
                {
                    var res = await HttpClientExtensions.PutAsJsonAsync<Models.User_Level>
                        (Stack.API_Uri_start_read + "/User_Levels/" + user_level.Id, user_level, Stack.token);
                    if (res.IsSuccessStatusCode)
                    {
                        pictureBox3.Visible = false;
                        panel1.Enabled = true;
                    }
                    //MessageBox.Show((await HttpClientExtensions.PutAsJsonAsync<Models.User>(Stack.API_Uri_start
                    //    + "/Users/"+user.Id, user, Stack.token)).StatusCode.ToString());
                }
                else
                    Program.dbOperations.UpdateUser_LevelAsync(user_level);
            }
            else dgvData[e.ColumnIndex, e.RowIndex].Value = InitailValue;
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف همه سطوح اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

            if (MessageBox.Show("با انجام این عمل ، تمام روابط سطوح کاربری و جداول دیگر از بین خواهد رفت"
                + "\n" + "آیا از حذف تمام سطوح اطمینان دارید؟", "اخطار 2"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            if (Stack.Use_Web)
            {
                MessageBox.Show("This feature is not currently supported on the web!");
            }
            else
            {
                Program.dbOperations.DeleteAllUser_LevelsAsync();
                Program.dbOperations.DeleteAllUser_Level_UL_FeaturesAsync();
                Program.dbOperations.DeleteAllUser_ULsAsync();
                dgvData.DataSource = GetData();
            }
        }

        private async void RadAll_CheckedChanged(object sender, EventArgs e)
        {
            if (Stack.Use_Web) dgvData.DataSource = await GetData_web();
            else dgvData.DataSource = GetData();

        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtST_Description.Text))
                return;

            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.User_Level> lstULs1 = (List<Models.User_Level>)dgvData.DataSource;
            {
                foreach (Control c in groupBox1.Controls)
                {
                    //MessageBox.Show(c.Text);
                    if (c.Name.Length > 4)
                    {
                        if (c.Name.Substring(0, 5).Equals("txtST"))
                            if (!string.IsNullOrWhiteSpace(c.Text))
                            {
                                lstULs1 = SearchThis(lstULs1, c.Name);
                                if ((lstULs1 == null) || !lstULs1.Any()) break;
                            }
                    }
                }
            }

            dgvData.DataSource = lstULs1;

            Application.DoEvents();
            panel1.Enabled = true;
        }

        // جستجوی موردی
        private List<Models.User_Level> SearchThis(List<Models.User_Level> lstULs1, string TextBoxName)
        {
            switch (TextBoxName)
            {
                case "txtST_Description":
                    switch (cmbST_Description.SelectedIndex)
                    {
                        case 0:
                            return lstULs1.Where(d => d.Description.ToLower().Contains(txtST_Description.Text.ToLower())).ToList();
                        case 1:
                            return lstULs1.Where(d => d.Description.ToLower().StartsWith(txtST_Description.Text.ToLower())).ToList();
                        case 2:
                            return lstULs1.Where(d => d.Description.ToLower().Equals(txtST_Description.Text.ToLower())).ToList();
                        default: return lstULs1;
                    }
            }

            return null;
        }

        private async void TsmiDelete_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            Models.User_Level user_level = Program.dbOperations.GetUser_LevelAsync(id);

            if (MessageBox.Show("آیا از حذف این سطح اطمینان دارید؟"
               , user_level.Description, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
               != DialogResult.Yes) return;
            panel1.Enabled = false;

            bool Delete_OK = false;
            if (Stack.Use_Web)
            {
                Delete_OK = await HttpClientExtensions.DeleteAsJsonAsync2(Stack.API_Uri_start
                     + "/User_Levels/" + id, Stack.token) != null;
            }
            else
            {
                // حذف رابطه این سطح کاربری با تمام کاربران دارای این سطح
                foreach (Models.User_UL user_ul in Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, 0, id).ToList())
                    Program.dbOperations.DeleteUser_ULAsync(user_ul);
                // حذف تمام رابطه های این سطح با امکانات آن
                foreach (Models.User_Level_UL_Feature ul_ulf in Program.dbOperations
                    .GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Id, id, 0))
                    Program.dbOperations.DeleteUser_Level_UL_Feature(ul_ulf);
                // حذف سطح کاربری
                Delete_OK = Program.dbOperations.DeleteUser_LevelAsync(user_level)>0;

                dgvData.DataSource = GetData();
            }

            pictureBox1.Visible = true;
            Application.DoEvents();
            timer1.Enabled = true;
            panel1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            timer1.Enabled = false;
        }

        private void TsmiUL_Features_Edit_Click(object sender, EventArgs e)
        {
            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            new J2220_User_Level_UL_Feature(user_level_index).ShowDialog();
        }

        private async void TsmiDeleteAllFeatures_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف همه امکانات این سطح اطمینان دارید؟"
                , "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.No) return;

            long user_level_id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);

            if(Stack.Use_Web)
            {

            }
            else
                Program.dbOperations.DeleteAllUser_Level_UL_Features
                    (Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Id, user_level_id));
            pictureBox1.Visible = true;
            Application.DoEvents();
            timer1.Enabled = true;
        }

        private void tsmiUL_See_ULs_Click(object sender, EventArgs e)
        {
            long user_level_id = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            new J2202_UL_See_UL_Orders(user_level_id).ShowDialog();
        }

        private void TsmiUL_See_OL_Click(object sender, EventArgs e)
        {
            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            new J2230_UL_See_OL(user_level_index).ShowDialog();
        }

        private void TsmiSetOL_UL_Click(object sender, EventArgs e)
        {
            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            new J2240_UL_Confirm_OLs(user_level_index).ShowDialog();
        }

        private void TsmiUL_Request_Categories_Click(object sender, EventArgs e)
        {
            long user_level_index = Convert.ToInt64(dgvData.CurrentRow.Cells["Id"].Value);
            new J2310_UL_Request_Categories(user_level_index).ShowDialog();
        }

        private async void BtnShowAll_Click(object sender, EventArgs e)
        {
            if (Stack.Use_Web) dgvData.DataSource = await GetData_web();
            else dgvData.DataSource = GetData();
        }


    }
}
