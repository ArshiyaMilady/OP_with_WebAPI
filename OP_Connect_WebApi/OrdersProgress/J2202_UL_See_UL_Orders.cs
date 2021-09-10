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
    public partial class J2202_UL_See_UL_Orders : X210_ExampleForm_Normal
    {
        long main_ul_id;
        List<Models.User_Level> lstUL = new List<Models.User_Level>();

        public J2202_UL_See_UL_Orders(long _main_ul_id)
        {
            InitializeComponent();

            main_ul_id = _main_ul_id;
            Text = "   " + Program.dbOperations.GetUser_LevelAsync(main_ul_id).Description;
        }

        private async void J2202_UL_See_UL_Orders_Shown(object sender, EventArgs e)
        {
            if(Stack.Use_Web) dgvData.DataSource = await GetData_web();
            else dgvData.DataSource = GetData();
            ShowData();
        }

        private List<Models.User_Level> GetData()
        {
            if (!lstUL.Any())
            {
                if (Stack.UserLevel_Type == 0)
                {
                    foreach (Models.UL_See_UL ul_see_ul in Program.dbOperations
                        .GetAllUL_See_ULsAsync(Stack.Company_Id, Stack.UserLevel_Id))
                    {
                        Models.User_Level ul = Program.dbOperations.GetUser_LevelAsync(ul_see_ul.UL_Id);
                        lstUL.Add(ul);
                    }
                }
                else
                {
                    if (Stack.UserLevel_Type == 1)
                        lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id).ToList();
                    else
                        lstUL = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id)
                            .Where(d => d.Type == 0).ToList();
                }
            }

            foreach (Models.User_Level ul in lstUL)
            {
                //if (Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Id, main_ul_index).Any())
                //    ul.C_B1 = true;

                ul.C_B1 = Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Id, main_ul_id)
                    .Any(d=>d.UL_Id == ul.Id);

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

                if (Stack.UserLevel_Type == 0)
                {
                    if ((lstUL != null) && lstUL.Any())
                    {
                        foreach (Models.UL_See_UL ul_see_ul in lstULSUL.Where(d=>d.MainUL_Id == Stack.UserLevel_Id).ToList())
                        {
                            Models.User_Level ul = lstUL1.First(d => d.Id == ul_see_ul.UL_Id);
                            lstUL.Add(ul);
                        }
                    }
                }
                else
                {
                    if (Stack.UserLevel_Type == 1)
                        lstUL = lstUL1;
                    else
                        lstUL = lstUL1.Where(d => d.Type == 0).ToList();
                }
            }

            foreach (Models.User_Level ul in lstUL)
            {
                ul.C_B1 = lstULSUL.Where(d => d.MainUL_Id == main_ul_id).Any(d => d.UL_Id == ul.Id);
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
            else SaveData();

            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void SaveData()
        {
            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Id, main_ul_id).Any())
            {
                foreach (Models.UL_See_UL ul_see_ul
                    in Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Id, main_ul_id))
                {
                    if (!lstUL.Where(d => d.C_B1).Any(d => d.Id == ul_see_ul.UL_Id))
                    {
                        Program.dbOperations.DeleteUL_See_ULAsync(ul_see_ul);
                    }
                }
            }
            #endregion

            #region اضافه شود 
            foreach (Models.User_Level ul in lstUL.Where(d => d.C_B1).ToList())
            {
                if (!Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Id, main_ul_id)
                    .Any(d => d.UL_Id == ul.Id))
                {
                    Program.dbOperations.AddUL_See_UL(new Models.UL_See_UL
                    {
                        Company_Id = Stack.Company_Id,
                        MainUL_Id = main_ul_id,
                        UL_Id = ul.Id,
                    });
                }
            }
            #endregion
        }

        private async Task<bool> SaveData_web()
        {
            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            List<Models.UL_See_UL> lstULSUL = await HttpClientExtensions.GetT<List<Models.UL_See_UL>>
                (Stack.API_Uri_start_read + "/UL_See_UL?all=no&company_id=" + Stack.Company_Id, Stack.token);

            //if (Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Id, main_ul_id).Any())
            if (lstULSUL.Any(d=>d.MainUL_Id == main_ul_id))
            {
                foreach (Models.UL_See_UL ul_see_ul in lstULSUL.Where(d => d.MainUL_Id == main_ul_id).ToList())
                {
                    if (!lstUL.Where(d => d.C_B1).Any(d => d.Id == ul_see_ul.UL_Id))
                    {
                        await HttpClientExtensions.DeleteAsJsonAsync2
                            (Stack.API_Uri_start + "/UL_See_UL/" + ul_see_ul.Id,Stack.token);
                        //Program.dbOperations.DeleteUL_See_ULAsync(ul_see_ul);
                    }
                }
            }
            #endregion

            #region اضافه شود 
            foreach (Models.User_Level ul in lstUL.Where(d => d.C_B1).ToList())
            {
                if (!lstULSUL.Where(d => d.MainUL_Id == main_ul_id).Any(d => d.UL_Id == ul.Id))
                {
                    Models.UL_See_UL ulsul =  new Models.UL_See_UL
                    {
                        Company_Id = Stack.Company_Id,
                        MainUL_Id = main_ul_id,
                        UL_Id = ul.Id,
                    };
                    await HttpClientExtensions.PostAsJsonAsync(Stack.API_Uri_start_read
                        + "/UL_See_UL", ulsul, Stack.token);
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

            Program.dbOperations.DeleteAllUL_See_ULsAsync();
        }

    }
}
