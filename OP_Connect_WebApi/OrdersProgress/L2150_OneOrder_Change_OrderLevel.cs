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
    public partial class L2150_OneOrder_Change_OrderLevel : X210_ExampleForm_Normal
    {
        string order_index;
        List<Models.Order_Level> lstOLs = new List<Models.Order_Level>();

        public L2150_OneOrder_Change_OrderLevel(string _order_index)
        {
            InitializeComponent();

            order_index = _order_index;
            Text = "    شماره سفارش : " + Program.dbOperations.GetOrderAsync(order_index).Id.ToString();
            Stack.bx = false;
        }

        private void L2150_OneOrder_Change_OrderLevel_Shown(object sender, EventArgs e)
        {

            dgvData.DataSource = GetData();
            
            ShowData();
        }

        private List<Models.Order_Level> GetData()
        {
            if (!lstOLs.Any())
            {
                // شناسه مراحل سفارش که کاربر (با سطح خودش) می تواند تأیید کند
                List<long> lstOL_indexes = new List<long>();
                if (Stack.UserLevel_Type != 0)
                    lstOL_indexes.AddRange(Program.dbOperations.GetAllOrder_LevelsAsync
                        (Stack.Company_Id).Select(d => d.Id).ToList());
                else
                    lstOL_indexes = Program.dbOperations.GetAllOL_ULsAsync(Stack.Company_Id
                        , 0, Stack.UserLevel_Id).Select(d => d.OL_Id).ToList();
        
                // شناسه (های) مراحل بعدی سفارش  
                List<long> lstNext_OLs = new ThisProject().Next_OrderLevel_Ids(order_index);

                foreach (long ol_index in lstOL_indexes)
                {
                    Models.Order_Level ol = Program.dbOperations.GetOrder_LevelAsync(ol_index);
                    if (ol.ReturningLevel || ol.CancelingLevel || lstNext_OLs.Contains(ol_index)) lstOLs.Add(ol);
                }
            }

            #region اگر برای کاربر جاری ، امکان تغییر مرحله سفارش وجود نداشت
            if (lstOLs.Count == 1)
                if(Program.dbOperations.GetOrder_LevelAsync(lstOLs.First().Id).ReturningLevel)
                {
                    MessageBox.Show("امکان تغییر مرحله سفارش وجود ندارد");
                    Close();
                    return null;
                }
            #endregion

            return lstOLs.OrderBy(d=>d.Sequence).ToList();
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

        private void DgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvData.Rows.Cast<DataGridViewRow>().Where(d=>Convert.ToBoolean(d.Cells["C_B1"].Value)).ToList().Count>1)
            {
                foreach (DataGridViewRow row in dgvData.Rows.Cast<DataGridViewRow>()
                    .Where(b=>b.Index != e.RowIndex)
                    .Where(d => Convert.ToBoolean(d.Cells["C_B1"].Value)).ToList())
                {
                    row.Cells["C_B1"].Value = false;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!dgvData.Rows.Cast<DataGridViewRow>().Any(d => Convert.ToBoolean(d.Cells["C_B1"].Value)))
            {
                MessageBox.Show("لطفا یک مرحله را انتخاب نمایید", "خطا");
            }
            else
            {
                ThisProject this_project = new ThisProject();

                DataGridViewRow row = dgvData.Rows.Cast<DataGridViewRow>().First(d => Convert.ToBoolean(d.Cells["C_B1"].Value));
                long ol_index = Convert.ToInt64(row.Cells["Id"].Value);
                Models.Order order = Program.dbOperations.GetOrderAsync(order_index);
                Models.Order_Level order_level = Program.dbOperations.GetOrder_LevelAsync(ol_index);
                // اگر سفارش برگشت داده شود
                if (order_level.ReturningLevel)
                {
                    #region اگر در جدول مراحل برگشتی ، مرحله ای برای مرحله جاری سفارش تعریف نشده باشد
                    if (!Program.dbOperations.GetAllOrder_Level_on_ReturningsAsync
                        (Stack.Company_Id, order.CurrentLevel_Id).Any())
                    {
                        MessageBox.Show("امکان برگشت سفارش وجود ندارد. لطفا با ادمین هماهنگ نمایید");
                        return;
                    }
                    #endregion

                    if (MessageBox.Show("آیا از برگشت سفارش اطمینان دارید؟", "مهم"
                        , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

                    new L1143_OL_on_Returning_Comment(order_index).ShowDialog();
                    if (!string.IsNullOrEmpty(Stack.sx))
                    {
                        this_project.ReturnOrder(order, Stack.lx, Stack.sx);
                        Stack.bx = true;
                    }
                }
                // اگر سفارش کنسل شود
                else if (order_level.CancelingLevel)
                {
                    if (MessageBox.Show("آیا از لغو سفارش اطمینان دارید؟", "مهم"
                       , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

                    new X220_InputBox("علت لغو").ShowDialog();
                    if (!string.IsNullOrEmpty(Stack.sx))
                    {
                        this_project.CancelOrder(order, "سفارش لغو شد. علت لغو : " + Stack.sx);
                        Stack.bx = true;
                    }
                }
                else
                {
                    if (MessageBox.Show("آیا از تغییر مرحله سفارش اطمینان دارید؟", "مهم"
                       , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

                    this_project.Change_Order_Level(order, ol_index);
                    Stack.bx = true;
                }

                if (Stack.bx)
                {
                    MessageBox.Show("تغییر مرحله سفارش انجام شد");
                    Close();
                }
            }
        }

 
    }
}
