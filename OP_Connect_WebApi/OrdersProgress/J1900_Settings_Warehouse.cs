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
    public partial class J1900_Settings_Warehouse : X210_ExampleForm_Normal
    {
        Models.Company company = null;

        public J1900_Settings_Warehouse()
        {
            InitializeComponent();
        }

        private async void J1900_Settings_Warehouse_Load(object sender, EventArgs e)
        {
            if(Stack.Use_Web)
                company = await HttpClientExtensions.GetT<Models.Company>
                    (Stack.API_Uri_start_read + "/Companies/" + Stack.Company_Id, Stack.token);
            else
                company = Program.dbOperations.GetCompanyAsync(Stack.Company_Id);

            if (company == null)
            {
                Close();
                return;
            }
            else
            {
                chkAutomaticWarehouseBooking.Checked = company.Warehouse_AutomaticBooking;
                numericUpDown1.Value = company.Warehouse_Booking_MaxHours;
                if (company.Warehouse_AutomaticBooking)
                    panel2.Enabled = true;
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟", ""
                , MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            if(Stack.bWarehouse_Booking_MaxHours != chkAutomaticWarehouseBooking.Checked)
            {
                //Models.Company company = Program.dbOperations.GetCompanyAsync(Stack.Company_Id);
                company.Warehouse_AutomaticBooking = chkAutomaticWarehouseBooking.Checked;
                company.Warehouse_Booking_MaxHours = (long)numericUpDown1.Value;
                Stack.bWarehouse_Booking_MaxHours = chkAutomaticWarehouseBooking.Checked;

                if(Stack.Use_Web)
                {
                    var res = await HttpClientExtensions.PutAsJsonAsync_byCheck<Models.Company>
                        (Stack.API_Uri_start + "/Companies/" + company.Id, company, Stack.token);
                    if (res == null)
                        MessageBox.Show("خطا در ثبت تغییرات");
                }
                else
                {
                    Program.dbOperations.UpdateCompanyAsync(company);
                }
            }
        }

        private void ChkAutomaticWarehouseBooking_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Enabled = chkAutomaticWarehouseBooking.Checked;
        }
    }
}
