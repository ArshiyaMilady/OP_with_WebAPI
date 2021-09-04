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
        long user_level_index;
        List<Models.Order_Level> lstOL = new List<Models.Order_Level>();

        public J2240_UL_Confirm_OLs(long _user_level_index)
        {
            InitializeComponent();

            user_level_index = _user_level_index;
            Text ="   " + Program.dbOperations.GetUser_LevelAsync(user_level_index).Description;
        }

        private void J2240_UL_Confirm_OLs_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
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
                ol.C_B1 = Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, ol.Id, user_level_index).Any();

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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            List<Models.Order_Level> lstOL = (List<Models.Order_Level>)dgvData.DataSource;

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, 0, user_level_index).Any())
            {

                foreach (Models.OL_UL ol_ul in Program.dbOperations
                    .GetAllOL_ULsAsync(Stack.Company_Id, 0, user_level_index))
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
                if (!Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, ol.Id, user_level_index).Any())
                {
                    Program.dbOperations.AddOL_ULAsync(new Models.OL_UL
                    {
                        Company_Id = Stack.Company_Id,
                        UL_Id = user_level_index,
                        OL_Id = ol.Id,
                    });
                }
            }
            #endregion

            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
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
    }
}
