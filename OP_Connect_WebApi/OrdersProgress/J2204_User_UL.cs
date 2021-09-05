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
        long user_index;
        List<Models.User_Level> lstUL = new List<Models.User_Level>();

        public J2204_User_UL(long _user_index)
        {
            InitializeComponent();

            user_index = _user_index;
            Text = Text + " - " + Program.dbOperations.GetUserAsync(user_index).Real_Name;
        }

        private void J2204_User_UL_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
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

            foreach (Models.User_UL user_ul in Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, user_index))
            {
                if(lstUL.Any(d=>d.Id == user_ul.UL_Id))
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            List<Models.User_Level> lstUL = (List<Models.User_Level>)dgvData.DataSource;

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, user_index).Any())
            {
                foreach (Models.User_UL user_ul
                    in Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, user_index))
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
                    if (!Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, user_index)
                        .Any(d => d.UL_Id == ul.Id))
                    {
                        Program.dbOperations.AddUser_UL(new Models.User_UL
                        {
                            Company_Id = Stack.Company_Id,
                            User_Id = user_index,
                            UL_Id = ul.Id,
                            UL_Description = ul.Description,
                        });
                    }
                }
                #endregion

            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
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
