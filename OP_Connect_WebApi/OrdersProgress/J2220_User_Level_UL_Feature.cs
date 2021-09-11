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
    public partial class J2220_User_Level_UL_Feature : X210_ExampleForm_Normal
    {
        long user_level_id;
        //List<Models.UL_Feature> lstULF_Result = new List<Models.UL_Feature>();

        public J2220_User_Level_UL_Feature(long _user_level_id)
        {
            InitializeComponent();

            user_level_id = _user_level_id;

            Text = "   " + Program.dbOperations.GetUser_LevelAsync(user_level_id).Description;
        }

        private async void J2220_User_Level_UL_Feature_Shown(object sender, EventArgs e)
        {
            if (Stack.Use_Web) await GetData_web();
            else GetData();
            ShowData();
            ColorDgv();
        }

        // رنگ بندی امکانات
        private void ColorDgv()
        {
            foreach (DataGridViewRow row in dgvData.Rows.Cast<DataGridViewRow>()
                .Where(d => d.Cells["Unique_Phrase"].Value.ToString().Substring(2).Equals("0000")).ToList())
                row.DefaultCellStyle.BackColor = Color.Yellow;
        }

         private void GetData()//List<Models.UL_Feature> lstULF)
        {
            List<Models.UL_Feature> lstUL_Feature = Program.dbOperations.GetAllUL_FeaturesAsync(Stack.Company_Id);
            //MessageBox.Show(Stack.UserLevel_Type.ToString());
            if(Stack.UserLevel_Type != 1)
                lstUL_Feature = lstUL_Feature.Where(d=>!d.Unique_Phrase.Substring(0,1).Equals("d")).ToList();

            if (Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Id,user_level_id).Any())
            {
                foreach (Models.User_Level_UL_Feature user_Level_ul_feature in 
                    Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Id,user_level_id))
                {
                    if (lstUL_Feature.Any(d => d.Id == user_Level_ul_feature.UL_Feature_Id))
                    {
                        Models.UL_Feature ulf = lstUL_Feature.First(d => d.Id == user_Level_ul_feature.UL_Feature_Id);
                        ulf.C_B1 = true;
                    }
                }
            }
            //if (!Stack.lstUser_ULF_UniquePhrase.Contains("ulf-k1120"))
            //    lstULF = lstULF.Where(d => !d.Unique_Phrase.Equals("ulf-k1120")).ToList();
            dgvData.DataSource = lstUL_Feature.OrderByDescending(d => d.C_B1).ThenBy(j => j.Unique_Phrase).ToList();
        }

         private async Task<bool> GetData_web()//List<Models.UL_Feature> lstULF)
        {
            List<Models.UL_Feature> lstUL_Feature = await HttpClientExtensions.GetT<List<Models.UL_Feature>>
                (Stack.API_Uri_start_read + "/UL_Feature?all=no&company_Id=" + Stack.Company_Id + "&EnableType=1", Stack.token);

            List<Models.User_Level_UL_Feature> lstUL_UL_Feature = await HttpClientExtensions.GetT<List<Models.User_Level_UL_Feature>>
                (Stack.API_Uri_start_read + "/User_Level_UL_Feature?all=no&company_Id=" + Stack.Company_Id 
                + "&EnableType=1&ul_Id="+ user_level_id, Stack.token);

            //Program.dbOperations.GetAllUL_FeaturesAsync(Stack.Company_Id);
            //MessageBox.Show(Stack.UserLevel_Type.ToString());
            if (Stack.UserLevel_Type != 1)
                lstUL_Feature = lstUL_Feature.Where(d=>!d.Unique_Phrase.Substring(0,1).Equals("d")).ToList();

            if (lstUL_UL_Feature.Any())
            {
                foreach (Models.User_Level_UL_Feature user_Level_ul_feature in lstUL_UL_Feature)
                {
                    if (lstUL_Feature.Any(d => d.Id == user_Level_ul_feature.UL_Feature_Id))
                    {
                        Models.UL_Feature ulf = lstUL_Feature.First(d => d.Id == user_Level_ul_feature.UL_Feature_Id);
                        ulf.C_B1 = true;
                    }
                }
            }
            //if (!Stack.lstUser_ULF_UniquePhrase.Contains("ulf-k1120"))
            //    lstULF = lstULF.Where(d => !d.Unique_Phrase.Equals("ulf-k1120")).ToList();
            dgvData.DataSource = lstUL_Feature.OrderByDescending(d => d.C_B1).ThenBy(j => j.Unique_Phrase).ToList();
            return true;
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
                    case "Unique_Phrase":
                        if (Stack.lstUser_ULF_UniquePhrase.Contains("dk1130"))
                        {
                            col.HeaderText = "عبارت شاخص";
                            col.ReadOnly = true;
                            col.Width = 120;
                        }
                        else col.Visible = false;
                        break;
                    case "Description":
                        col.HeaderText = "شرح";
                        col.ReadOnly = true;
                        col.Width = 500;
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
            List<Models.UL_Feature> lstUL_Feature = (List<Models.UL_Feature>)dgvData.DataSource;

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Id, user_level_id).Any())
            {
                foreach (Models.User_Level_UL_Feature ul_ulf
                    in Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Id, user_level_id, 0))
                {
                    if (!lstUL_Feature.Where(d => d.C_B1).Any(d => d.Id == ul_ulf.UL_Feature_Id))
                    {
                        Program.dbOperations.DeleteUser_Level_UL_Feature(ul_ulf);
                    }
                }
            }
            #endregion

            #region اضافه شود 
            foreach (Models.UL_Feature ulf in lstUL_Feature.Where(d => d.C_B1).ToList())
            {
                if (!Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Id, user_level_id)
                    .Any(d => d.UL_Feature_Id == ulf.Id))
                {
                    Program.dbOperations.AddUser_Level_UL_FeatureAsync(new Models.User_Level_UL_Feature
                    {
                        Company_Id = Stack.Company_Id,
                        UL_Feature_Unique_Phrase = ulf.Unique_Phrase,
                        User_Level_Id = user_level_id,
                        UL_Feature_Id = ulf.Id,
                        UL_Feature_Enabled = true,
                    });
                }
            }
            #endregion
        }

        private async Task<bool> SaveData_web()
        {
            List<Models.UL_Feature> lstUL_Feature = (List<Models.UL_Feature>)dgvData.DataSource;

            List<Models.User_Level_UL_Feature> lstUL_UL_Feature = await HttpClientExtensions.GetT<List<Models.User_Level_UL_Feature>>
             (Stack.API_Uri_start_read + "/User_Level_UL_Feature?all=no&company_Id=" + Stack.Company_Id
             + "&EnableType=1&ul_Id=" + user_level_id, Stack.token);


            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            //if (Program.dbOperations.GetAllUL_See_ULsAsync(Stack.Company_Id, main_ul_id).Any())
            if (lstUL_UL_Feature.Any())
            {
                foreach (Models.User_Level_UL_Feature ul_ulf in lstUL_UL_Feature)
                {
                    if (!lstUL_Feature.Where(d => d.C_B1).Any(d => d.Id == ul_ulf.UL_Feature_Id))
                    {
                        await HttpClientExtensions.DeleteAsJsonAsync2
                            (Stack.API_Uri_start + "/User_Level_UL_Feature/" + ul_ulf.Id, Stack.token);
                        //Program.dbOperations.DeleteUL_See_ULAsync(ul_see_ul);
                    }
                }
            }
            #endregion

            #region اضافه شود 
            foreach (Models.UL_Feature ulf in lstUL_Feature.Where(d => d.C_B1).ToList())
            {
                if (!lstUL_UL_Feature.Any(d => d.UL_Feature_Id == ulf.Id))
                {
                    Models.User_Level_UL_Feature ulf1 = new Models.User_Level_UL_Feature
                    {
                        Company_Id = Stack.Company_Id,
                        UL_Feature_Unique_Phrase = ulf.Unique_Phrase,
                        User_Level_Id = user_level_id,
                        UL_Feature_Id = ulf.Id,
                        UL_Feature_Enabled = true,
                    };

                    await HttpClientExtensions.PostAsJsonAsync(Stack.API_Uri_start_read
                        + "/User_Level_UL_Feature", ulf1, Stack.token);
                }
            }
            #endregion

            return true;
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("این امکان فعال نمی باشد");
            return;

            if (MessageBox.Show("آیا از حذف تمام روابط سطوح کاربری و امکانات اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllUser_Level_UL_FeaturesAsync();
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
    }
}
