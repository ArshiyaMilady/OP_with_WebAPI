﻿using System;
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
        long ul_index;
        List<Models.Order_Level> lstOLs = new List<Models.Order_Level>();

        public J2230_UL_See_OL(long _ul_index)
        {
            InitializeComponent();

            ul_index = _ul_index;
            Text = "    " + Program.dbOperations.GetUser_LevelAsync(ul_index).Description;
        }

        private void J2230_UL_See_OL_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
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
                ol.C_B1 = Program.dbOperations.GetAllUL_See_OLsAsync(Stack.Company_Id, ul_index)
                    .Any(d => d.OL_Id == ol.Id);
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            panel1.Enabled = false;
            Application.DoEvents();

            List<Models.Order_Level> lstOL = (List<Models.Order_Level>)dgvData.DataSource;

            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllUL_See_OLsAsync(Stack.Company_Id, ul_index).Any())
            {
                foreach (Models.UL_See_OL ul_see_ol
                    in Program.dbOperations.GetAllUL_See_OLsAsync(Stack.Company_Id, ul_index))
                {
                    if (!lstOL.Where(d => d.C_B1).Any(d => d.Id == ul_see_ol.UL_Id))
                    {
                        Program.dbOperations.DeleteUL_See_OLAsync(ul_see_ol);
                    }
                }
            }
            #endregion

            #region موارد جدید اضافه شود 
            foreach (Models.Order_Level ol in lstOL.Where(d => d.C_B1).ToList())
            {
                if (!Program.dbOperations.GetAllUL_See_OLsAsync(Stack.Company_Id, ul_index)
                    .Any(d => d.OL_Id == ol.Id))
                {
                    Program.dbOperations.AddUL_See_OL(new Models.UL_See_OL
                    {
                        Company_Id = Stack.Company_Id,
                        UL_Id = ul_index,
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
