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
    public partial class J2240_UL_Confirm_OLs : X210_ExampleForm_Normal
    {
        long user_level_id;
        List<Models.Order_Level> lstOL = new List<Models.Order_Level>();

        public J2240_UL_Confirm_OLs(long _user_level_id)
        {
            InitializeComponent();

            user_level_id = _user_level_id;
            Text = "";
        }

        private async void J2240_UL_Confirm_OLs_Shown(object sender, EventArgs e)
        {
            if (Stack.Use_Web)
            {
                Models.User_Level ul = await HttpClientExtensions.GetT<Models.User_Level>
                    (Stack.API_Uri_start_read + "/User_Levels/" + user_level_id);
                if (ul != null) Text = "   " + ul.Description;
            }
            else
                Text = "   " + Program.dbOperations.GetUser_LevelAsync(user_level_id).Description;

            if (Stack.Use_Web) dgvData.DataSource = await GetData_web();
            else dgvData.DataSource = GetData();
            ShowData();
        }


        private List<Models.Order_Level> GetData()
        {
            if (!lstOL.Any())
            {
                if (Stack.UserLevel_Type == 0)
                {
                    foreach (Models.OL_UL ol_ul in Program.dbOperations
                        .GetAllOL_ULsAsync(Stack.Company_Id,0, Stack.UserLevel_Id))
                    {
                        Models.Order_Level ol = Program.dbOperations.GetOrder_LevelAsync(ol_ul.OL_Id);
                        lstOL.Add(ol);
                    }
                }
                else
                {
                    //if (Stack.UserLevel_Type == 1)
                    lstOL = Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Id).ToList();
                }
            }

            foreach (Models.Order_Level ol in lstOL)
                ol.C_B1 = Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, ol.Id, user_level_id).Any();

            return lstOL.OrderByDescending(d => d.C_B1).ToList();
        }

        private async Task<List<Models.Order_Level>> GetData_web()
        {
            List<Models.OL_UL> lstOLUL = await HttpClientExtensions.GetT<List<Models.OL_UL>>
               (Stack.API_Uri_start_read + "/OL_UL?all=no&company_id=" + Stack.Company_Id
               + "&ol_Id=0&ul_Id=" + Stack.UserLevel_Id, Stack.token);

            if (!lstOL.Any())
            {
                List<Models.Order_Level> lstOL1 = await HttpClientExtensions.GetT<List<Models.Order_Level>>
                   (Stack.API_Uri_start_read + "/Order_Level?all=no&company_id=" + Stack.Company_Id, Stack.token);
                lstOL1 = lstOL1.Where(d => d.Enabled).ToList();

                if (Stack.UserLevel_Type == 0)
                {

                    foreach (Models.OL_UL ol_ul in lstOLUL)
                    {
                        //Models.Order_Level ol = lstOL1.First(d => d.Id == ol_ul.OL_Id);// Program.dbOperations.GetOrder_LevelAsync(ol_ul.OL_Id);
                        lstOL.Add(lstOL1.First(d => d.Id == ol_ul.OL_Id));
                    }
                }
                else
                {
                    //if (Stack.UserLevel_Type == 1)
                    lstOL = lstOL1;
                }
            }

            foreach (Models.Order_Level ol in lstOL)
                ol.C_B1 = lstOLUL.Any(d => d.OL_Id == ol.Id);

            return lstOL.OrderByDescending(d => d.C_B1).ToList();
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
            else SaveData();

            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void SaveData()
        {
            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, 0, user_level_id).Any())
            {

                foreach (Models.OL_UL ol_ul in Program.dbOperations
                    .GetAllOL_ULsAsync(Stack.Company_Id, 0, user_level_id))
                {
                    if (!lstOL.Where(d => d.C_B1).Any(d => d.Id == ol_ul.OL_Id))
                    {
                        Program.dbOperations.DeleteOL_ULAsync(ol_ul);
                    }
                }
            }
            #endregion

            #region موارد جدید اضافه شود 
            foreach (Models.Order_Level ol in lstOL.Where(d => d.C_B1).ToList())
            {
                if (!Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, ol.Id, user_level_id).Any())
                {
                    Program.dbOperations.AddOL_ULAsync(new Models.OL_UL
                    {
                        Company_Id = Stack.Company_Id,
                        UL_Id = user_level_id,
                        OL_Id = ol.Id,
                    });
                }
            }
            #endregion
        }

        private async Task<bool> SaveData_web()
        {
            List<Models.OL_UL> lstOLUL = await HttpClientExtensions.GetT<List<Models.OL_UL>>
               (Stack.API_Uri_start_read + "/OL_UL?all=no&company_id=" + Stack.Company_Id
               + "&ol_Id=0&ul_Id=" + user_level_id, Stack.token);

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (lstOLUL.Any())
            {
                foreach (Models.OL_UL olul in lstOLUL)
                {
                    if (!lstOL.Where(d => d.C_B1).Any(d => d.Id == olul.UL_Id))
                    {
                        await HttpClientExtensions.DeleteAsJsonAsync2
                            (Stack.API_Uri_start + "/OL_UL/" + olul.Id, Stack.token);
                    }
                }
            }
            #endregion

            #region موارد جدید اضافه شود 
            foreach (Models.Order_Level ol in lstOL.Where(d => d.C_B1).ToList())
            {
                if (!lstOLUL.Any(d => d.OL_Id == ol.Id))
                {
                    Models.OL_UL olul = new Models.OL_UL
                    {
                        Company_Id = Stack.Company_Id,
                        UL_Id = user_level_id,
                        OL_Id = ol.Id,
                    };
                    await HttpClientExtensions.PostAsJsonAsync(Stack.API_Uri_start_read
                       + "/OL_UL", olul, Stack.token);
                }
            }
            #endregion

            return true;
        }


        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("این امکان فعال نمی باشد");
            return;

            if (MessageBox.Show("آیا از حذف تمام روابط سطوح کاربری با مراحل سفارشها اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllOL_ULsAsync();
        }

        bool bChooseAll = false;
        private void BtnChooseAll_Click(object sender, EventArgs e)
        {
            bChooseAll = !bChooseAll;

            foreach (DataGridViewRow row in dgvData.Rows.Cast<DataGridViewRow>())
                row.Cells["C_B1"].Value = bChooseAll;
            if (bChooseAll) btnChooseAll.Text = "عدم انتخاب همه";
            else btnChooseAll.Text = "انتخاب همه";
        }

        private void J2240_UL_Confirm_OLs_Load(object sender, EventArgs e)
        {

        }
    }
}
