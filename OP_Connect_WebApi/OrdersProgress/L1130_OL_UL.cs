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
    public partial class L1130_OL_UL : X210_ExampleForm_Normal
    {
        long order_level_index;

        public L1130_OL_UL(long _order_level_index)
        {
            InitializeComponent();

            order_level_index = _order_level_index;
            Text = "   " + Program.dbOperations.GetOrder_LevelAsync(order_level_index).Description;
        }

        private void L1130_OL_UL_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
            ShowData();
        }

        private List<Models.User_Level> GetData()//List<Models.UL_Feature> lstULF)
        {
            List<Models.User_Level> lstULs = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id, 1);
            //MessageBox.Show(Stack.UserLevel_Type.ToString());
            //if (Stack.UserLevel_Type != 1)
            //lstULs = lstULs.Where(d => (d.Type == 0)|| (d.Type == 3)).ToList();
            lstULs = lstULs.Where(d => d.Type == 0).ToList();

            if (Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, order_level_index).Any())
            {
                foreach (Models.OL_UL ol_ul in
                    Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, order_level_index))
                {
                    if (lstULs.Any(d => d.Id == ol_ul.UL_Id))
                    {
                        Models.User_Level ulf = lstULs.First(d => d.Id == ol_ul.UL_Id);
                        ulf.C_B1 = true;
                    }
                }
            }
            //if (!Stack.lstUser_ULF_UniquePhrase.Contains("ulf-k1120"))
            //    lstULF = lstULF.Where(d => !d.Unique_Phrase.Equals("ulf-k1120")).ToList();
            return lstULs.OrderByDescending(d => d.C_B1).ToList();
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            List<Models.User_Level> lstULs = (List<Models.User_Level>)dgvData.DataSource;

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, order_level_index).Any())
            {
                foreach (Models.OL_UL ol_ul in Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, order_level_index))
                {
                    if (!lstULs.Where(d => d.C_B1).Any(d => d.Id == ol_ul.UL_Id))
                        Program.dbOperations.DeleteOL_UL(ol_ul);
                }
            }
            #endregion

            #region اضافه شود 
            foreach (Models.User_Level ul in lstULs.Where(d => d.C_B1).ToList())
                {
                    if (!Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id, order_level_index)
                        .Any(d => d.UL_Id == ul.Id))
                    {
                        Program.dbOperations.AddOL_ULAsync(new Models.OL_UL
                        {
                            Company_Id = Stack.Company_Id,
                            OL_Id = order_level_index,
                            UL_Id = ul.Id,
                        });
                    }
                }
                #endregion

            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از حذف تمام روابط مراحل سفارش و سطوح کاربری اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            Program.dbOperations.DeleteAllOL_ULsAsync();
        }
    }
}
