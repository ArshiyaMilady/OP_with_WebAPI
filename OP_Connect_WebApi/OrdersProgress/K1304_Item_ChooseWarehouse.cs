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
    public partial class K1304_Item_ChooseWarehouse : X210_ExampleForm_Normal
    {
        Models.Item item = null;
        List<Models.Warehouse> lstWarehouses = new List<Models.Warehouse>();

        public K1304_Item_ChooseWarehouse(Models.Item _item = null)
        {
            InitializeComponent();

            Stack.ix = -1;
            if (_item != null)
            {
                item = _item;
                label3.Text = label3.Text + " برای" + item.Name_Samll;
            }
        }

        private async void K1304_Item_ChooseWarehouse_Shown(object sender, EventArgs e)
        {
            if (Stack.Use_Web)
            {
                try
                {
                    lstWarehouses = await HttpClientExtensions.GetT<List<Models.Warehouse>>
                         (Stack.API_Uri_start_read + "/Warehouses?all=no&company_Id=" + Stack.Company_Id, Stack.token);
                }
                catch { }
            }
            else
                lstWarehouses = Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Id);

            if (lstWarehouses.Any())
            {
                comboBox1.Items.AddRange(lstWarehouses.Select(d => d.Name).ToArray());
                if (item == null)
                {
                    if (comboBox1.Items.Count > 0)
                        comboBox1.SelectedIndex = 0;
                }
                else
                {
                    if (comboBox1.Items.Count > 0)
                    {
                        if (lstWarehouses.Any(d => d.Id == item.Warehouse_Id))
                            comboBox1.Text = lstWarehouses.First(d => d.Id == item.Warehouse_Id).Name;
                        else
                            comboBox1.SelectedIndex = 0;
                    }
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از انتخاب خود اطمینان دارید?", ""
                , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            Stack.lx = lstWarehouses.First(d => d.Name.Equals(comboBox1.Text)).Id;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Stack.ix = -1;
            Close();
        }
    }
}
