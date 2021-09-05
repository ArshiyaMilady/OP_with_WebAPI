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
    public partial class L1120_OL_Prerequisites : X210_ExampleForm_Normal
    {
        long order_level_index;

        public L1120_OL_Prerequisites(long _order_level_index)
        {
            InitializeComponent();

            order_level_index = _order_level_index;
            Text = "   " + Program.dbOperations.GetOrder_LevelAsync(order_level_index).Description;
        }

        private void L1120_OL_Prerequisites_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = GetData();
            ShowData();
        }


        private List<Models.Order_Level> GetData()//List<Models.UL_Feature> lstULF)
        {
            List<Models.Order_Level> lstOLs = Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Id, 1)
                .Where(d => d.Id != order_level_index).ToList();

            if (Program.dbOperations.GetAllOL_PrerequisitesAsync(Stack.Company_Id, order_level_index).Any())
            {
                foreach (Models.OL_Prerequisite olp in
                    Program.dbOperations.GetAllOL_PrerequisitesAsync(Stack.Company_Id, order_level_index))
                {
                    if (lstOLs.Any(d => d.Id == olp.Prerequisite_Id))
                    {
                        Models.Order_Level ol = lstOLs.First(d => d.Id == olp.Prerequisite_Id);
                        ol.C_B1 = true;
                    }
                }
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
                        col.Width = 300;
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
            if (Program.dbOperations.GetAllOL_PrerequisitesAsync(Stack.Company_Id, order_level_index).Any())
            {

                foreach (Models.OL_Prerequisite olp
                    in Program.dbOperations.GetAllOL_PrerequisitesAsync(Stack.Company_Id, order_level_index))
                {
                    if (!lstOL.Where(d => d.C_B1).Any(d => d.Id == olp.OL_Id))
                        Program.dbOperations.DeleteOL_PrerequisiteAsync(olp);
                }
            }
            #endregion

            #region اضافه شود 
            foreach (Models.Order_Level ol in lstOL.Where(d => d.C_B1).ToList())
                {
                    if (!Program.dbOperations.GetAllOL_PrerequisitesAsync(Stack.Company_Id, order_level_index)
                        .Any(d => d.Prerequisite_Id == ol.Id))
                    {
                        Program.dbOperations.AddOL_PrerequisiteAsync(new Models.OL_Prerequisite
                        {
                            Company_Id = Stack.Company_Id,
                            OL_Id = order_level_index,
                            Prerequisite_Id = ol.Id,
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

            Program.dbOperations.DeleteAllOL_PrerequisitesAsync();
        }

    }
}
