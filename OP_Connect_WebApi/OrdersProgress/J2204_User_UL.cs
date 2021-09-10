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
    public partial class J2204_User_UL : X210_ExampleForm_Normal
    {
        long user_id;
        List<Models.User_Level> lstUL = new List<Models.User_Level>();

        public J2204_User_UL(long _user_index)
        {
            InitializeComponent();

            user_id = _user_index;
            Text = Text + " - " + Program.dbOperations.GetUserAsync(user_id).Real_Name;
        }

        private async void J2204_User_UL_Shown(object sender, EventArgs e)
        {
            if(Stack.Use_Web) dgvData.DataSource = await GetData_web();
            else dgvData.DataSource = GetData();
            ShowData();
        }

        private List<Models.User_Level> GetData()
        {
            #region این کاربر با توجه به سطح کاربری کاربر وارد شده ، چه سطح کاربری را می تواند داشته باشد
            if (Stack.UserLevel_Type == 0)
            {
                foreach (Models.UL_See_UL ul_see_ul in Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Id,Stack.UserLevel_Id))
                {
                    Models.User_Level ul = Program.dbOperations.GetUser_LevelAsync(ul_see_ul.UL_Id);
                    lstUL.Add(ul);
                }
            }
            else
            {
                if (Stack.UserLevel_Type == 1)
                    lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id).Where(b => b.Enabled).ToList();
                else
                    lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id).Where(b => b.Enabled)
                        .Where(d => d.Type == 0).ToList();

                //if (Stack.UserLevel_Type == 2)
                //    lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id).Where(b => b.Enabled)
                //        .Where(d => (d.Type != 1) && (d.Type != 2)).ToList();
                //else if (Stack.UserLevel_Type == 3)
                //    lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id).Where(b => b.Enabled)
                //        .Where(d => (d.Type != 1) && (d.Type != 2) && (d.Type != 3)).ToList();
            }
            #endregion

            foreach (Models.User_UL user_ul in Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, user_id))
            {
                if(lstUL.Any(d=>d.Id == user_ul.UL_Id))
                    lstUL.First(d => d.Id == user_ul.UL_Id).C_B1 = true;
            }

            return lstUL.OrderByDescending(d => d.C_B1).ToList();
        }

        private async Task<List<Models.User_Level>> GetData_web()
        {
            List<Models.UL_See_UL> lstULSUL = await HttpClientExtensions.GetT<List<Models.UL_See_UL>>
               (Stack.API_Uri_start_read + "/UL_See_UL?all=no&company_id=" + Stack.Company_Id, Stack.token);

            if (!lstUL.Any())
            {
                List<Models.User_Level> lstUL1 = await HttpClientExtensions.GetT<List<Models.User_Level>>
                    (Stack.API_Uri_start_read + "/User_Levels?all=no&company_id=" + Stack.Company_Id, Stack.token);
                lstUL1 = lstUL1.Where(d => d.Enabled).ToList();

                #region این کاربر با توجه به سطح کاربری کاربر وارد شده ، چه سطح کاربری را می تواند داشته باشد
                if (Stack.UserLevel_Type == 0)
                {
                    foreach (Models.UL_See_UL ul_see_ul in lstULSUL.Where(d => d.MainUL_Id == Stack.UserLevel_Id).ToList())
                    {
                        Models.User_Level ul = lstUL1.First(d => d.Id == ul_see_ul.UL_Id);
                        lstUL.Add(ul);
                    }
                }
                else
                {
                    if (Stack.UserLevel_Type == 1)
                        lstUL = lstUL1;
                    else
                        lstUL = lstUL1.Where(d => d.Type == 0).ToList();
                }
                #endregion
            }

            List<Models.User_UL> lstThisUser_UL = await HttpClientExtensions.GetT<List<Models.User_UL>>
              (Stack.API_Uri_start_read + "/User_UL?all=no&company_id=" + Stack.Company_Id
              + "&user_id=" + user_id, Stack.token);

            foreach (Models.User_UL user_ul in lstThisUser_UL)
            {
                if (lstUL.Any(d => d.Id == user_ul.UL_Id))
                    lstUL.First(d => d.Id == user_ul.UL_Id).C_B1 = true;
            }

            return lstUL.OrderByDescending(d => d.C_B1).ToList();
        }

        private void ShowData()
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    case "C_B1":
                        col.HeaderText = "انتخاب";
                        col.Width = 50;
                        break;
                    case "Description":
                        col.HeaderText = "شرح";
                        col.ReadOnly = true;
                        col.Width = 200;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            if (Stack.Use_Web) await SaveData_web();
            else Save_Data();
            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void Save_Data()
        {
            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, user_id).Any())
            {
                foreach (Models.User_UL user_ul
                    in Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, user_id))
                {
                    if (!lstUL.Where(d => d.C_B1).Any(d => d.Id == user_ul.UL_Id))
                    {
                        Program.dbOperations.DeleteUser_ULAsync(user_ul);
                    }
                }
            }
            #endregion

            #region اضافه شود 
            foreach (Models.User_Level ul in lstUL.Where(d => d.C_B1).ToList())
            {
                if (!Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, user_id)
                    .Any(d => d.UL_Id == ul.Id))
                {
                    Program.dbOperations.AddUser_UL(new Models.User_UL
                    {
                        Company_Id = Stack.Company_Id,
                        User_Id = user_id,
                        UL_Id = ul.Id,
                        UL_Description = ul.Description,
                    });
                }
            }
            #endregion

        }

        private async Task<bool> SaveData_web()
        {
            List<Models.User_UL> lstThisUser_UL = await HttpClientExtensions.GetT<List<Models.User_UL>>
            (Stack.API_Uri_start_read + "/User_UL?all=no&company_id=" + Stack.Company_Id
            + "&user_id=" + user_id, Stack.token);

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (lstThisUser_UL.Any())
            {
                foreach (Models.User_UL user_ul in lstThisUser_UL)
                {
                    if (!lstUL.Where(d => d.C_B1).Any(d => d.Id == user_ul.UL_Id))
                    {
                        await HttpClientExtensions.DeleteAsJsonAsync2
                            (Stack.API_Uri_start + "/User_UL/" + user_ul.Id, Stack.token);
                    }
                }
            }
            #endregion

            #region اضافه شود 
            foreach (Models.User_Level ul in lstUL.Where(d => d.C_B1).ToList())
            {
                if (!lstThisUser_UL.Any(d => d.UL_Id == ul.Id))
                {
                    Models.User_UL uul = new Models.User_UL
                    {
                        Company_Id = Stack.Company_Id,
                        User_Id = user_id,
                        UL_Id = ul.Id,
                        UL_Description = ul.Description,
                    };
                    await HttpClientExtensions.PostAsJsonAsync(Stack.API_Uri_start_read
                      + "/User_UL", uul, Stack.token);
                }
            }
            #endregion

            return true;
        }


        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("این امکان فعال نمی باشد");
            return;

            if (MessageBox.Show("آیا از حذف تمام روابط سطوح کاربری اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllUser_ULsAsync();
        }
  
        // فقط یکی از سطح ها انتخاب شود
        private void DgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvData.Columns[e.ColumnIndex].Name.Equals("C_B1"))
            {
                bool C_B1 = Convert.ToBoolean(dgvData[e.ColumnIndex, e.RowIndex].Value);
                if (!C_B1)
                {
                    foreach (DataGridViewRow row in dgvData.Rows)
                        if (row.Index != e.RowIndex)
                            row.Cells["C_B1"].Value = false;

                    //dgvData["C_B1", e.RowIndex].Value = true;
                }

            }
        }
    }
}
