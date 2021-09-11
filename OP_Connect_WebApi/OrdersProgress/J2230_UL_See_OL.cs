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
    public partial class J2230_UL_See_OL : X210_ExampleForm_Normal
    {
        long ul_id;
        List<Models.Order_Level> lstOLs = new List<Models.Order_Level>();

        public J2230_UL_See_OL(long _ul_id)
        {
            InitializeComponent();

            ul_id = _ul_id;
            Text = "";
        }

        private async void J2230_UL_See_OL_Shown(object sender, EventArgs e)
        {
            if (Stack.Use_Web)
            {
                Models.User_Level ul = await HttpClientExtensions.GetT<Models.User_Level>
                   (Stack.API_Uri_start_read + "/User_Levels/" + ul_id);
                if (ul != null) Text = "   " + ul.Description;
                dgvData.DataSource = await GetData_web();
            }
            else
            {
                Text = "    " + Program.dbOperations.GetUser_LevelAsync(ul_id).Description;
                dgvData.DataSource = GetData();
            }
            ShowData();
        }

        private List<Models.Order_Level> GetData()
        {
            if (!lstOLs.Any())
            {
                if (Stack.UserLevel_Type == 0)
                {
                    foreach (Models.UL_See_OL ul_see_ol in Program.dbOperations
                        .GetAllUL_See_OLsAsync(Stack.Company_Id, Stack.UserLevel_Id))
                    {
                        Models.Order_Level ol = Program.dbOperations.GetOrder_LevelAsync(ul_see_ol.OL_Id);
                        lstOLs.Add(ol);
                    }
                }
                else
                {
                    //if (Stack.UserLevel_Type == 1)
                    lstOLs = Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Id).ToList();
                }
            }

            foreach (Models.Order_Level ol in lstOLs)
            {
                ol.C_B1 = Program.dbOperations.GetAllUL_See_OLsAsync(Stack.Company_Id, ul_id)
                    .Any(d => d.OL_Id == ol.Id);
            }

            return lstOLs.OrderByDescending(d => d.C_B1).ToList();
        }

        private async Task<List<Models.Order_Level>> GetData_web()
        {
            List<Models.UL_See_OL> lstULSOL = await HttpClientExtensions.GetT<List<Models.UL_See_OL>>
                 (Stack.API_Uri_start_read + "/UL_See_OL?all=no&company_id=" + Stack.Company_Id, Stack.token);

            if (!lstOLs.Any())
            {
                List<Models.Order_Level> lstOL1 = await HttpClientExtensions.GetT<List<Models.Order_Level>>
                  (Stack.API_Uri_start_read + "/Order_Level?all=no&company_id=" + Stack.Company_Id, Stack.token);
                lstOL1 = lstOL1.Where(d => d.Enabled).ToList();

                if (Stack.UserLevel_Type == 0)
                {
                    foreach (Models.UL_See_OL ul_see_ol in lstULSOL.Where(d=>d.UL_Id == Stack.UserLevel_Id).ToList())
                    {
                        //Models.Order_Level ol = lstOL1.First(d => d.Id == ul_see_ol.OL_Id);
                        lstOLs.Add(lstOL1.First(d => d.Id == ul_see_ol.OL_Id));
                    }
                }
                else
                {
                    //if (Stack.UserLevel_Type == 1)
                    lstOLs = lstOL1;
                }
            }

            foreach (Models.Order_Level ol in lstOLs)
            {
                ol.C_B1 = lstULSOL.Where(d => d.UL_Id == ul_id).Any(d => d.OL_Id == ol.Id);
            }

            return lstOLs.OrderByDescending(d => d.C_B1).ToList();
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

            panel1.Enabled = false;
            Application.DoEvents();

            if (Stack.Use_Web) await SaveData_web();
            else SaveData();

            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void SaveData()
        {
            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllUL_See_OLsAsync(Stack.Company_Id, ul_id).Any())
            {
                foreach (Models.UL_See_OL ul_see_ol
                    in Program.dbOperations.GetAllUL_See_OLsAsync(Stack.Company_Id, ul_id))
                {
                    if (!lstOLs.Where(d => d.C_B1).Any(d => d.Id == ul_see_ol.UL_Id))
                    {
                        Program.dbOperations.DeleteUL_See_OLAsync(ul_see_ol);
                    }
                }
            }
            #endregion

            #region موارد جدید اضافه شود 
            foreach (Models.Order_Level ol in lstOLs.Where(d => d.C_B1).ToList())
            {
                if (!Program.dbOperations.GetAllUL_See_OLsAsync(Stack.Company_Id, ul_id)
                    .Any(d => d.OL_Id == ol.Id))
                {
                    Program.dbOperations.AddUL_See_OL(new Models.UL_See_OL
                    {
                        Company_Id = Stack.Company_Id,
                        UL_Id = ul_id,
                        OL_Id = ol.Id,
                    });
                }
            }
            #endregion

        }

        private async Task<bool> SaveData_web()
        {
            List<Models.UL_See_OL> lstULSOL = await HttpClientExtensions.GetT<List<Models.UL_See_OL>>
              (Stack.API_Uri_start_read + "/UL_See_OL?all=no&company_id=" + Stack.Company_Id
              + "&ul_Id=" + ul_id, Stack.token);

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (lstULSOL.Any())
            {
                foreach (Models.UL_See_OL ul_see_ol in lstULSOL)
                {
                    if (!lstOLs.Where(d => d.C_B1).Any(d => d.Id == ul_see_ol.UL_Id))
                    {
                        await HttpClientExtensions.DeleteAsJsonAsync2
                            (Stack.API_Uri_start + "/UL_See_OL/" + ul_see_ol.Id, Stack.token);
                    }
                }
            }
            #endregion

            #region موارد جدید اضافه شود 
            foreach (Models.Order_Level ol in lstOLs.Where(d => d.C_B1).ToList())
            {
                if (!lstULSOL.Any(d => d.OL_Id == ol.Id))
                {
                    Models.UL_See_OL ulsol = new Models.UL_See_OL
                    {
                        Company_Id = Stack.Company_Id,
                        UL_Id = ul_id,
                        OL_Id = ol.Id,
                    };
                    await HttpClientExtensions.PostAsJsonAsync(Stack.API_Uri_start_read
                       + "/UL_See_OL", ulsol, Stack.token);
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

            Program.dbOperations.DeleteAllUL_See_OLsAsync();
        }

        bool bChooseAll = false;
        private void BtnChooseAll_Click(object sender, EventArgs e)
        {
            bChooseAll = !bChooseAll;

            foreach (DataGridViewRow row in dgvData.Rows.Cast<DataGridViewRow>())
                row.Cells["C_B1"].Value = bChooseAll;
            if(bChooseAll) btnChooseAll.Text = "عدم انتخاب همه";
            else btnChooseAll.Text = "انتخاب همه";
        }
    }
}
