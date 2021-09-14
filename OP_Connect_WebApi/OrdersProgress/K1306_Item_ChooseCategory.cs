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
    public partial class K1306_Item_ChooseCategory : Form
    {
        Models.Item item = null;
        List<Models.Category> lstCategories = new List<Models.Category>();

        public K1306_Item_ChooseCategory(Models.Item _item = null)
        {
            InitializeComponent();

            Stack.ix = -1;
            if (_item != null)
            {
                item = _item;
                label3.Text = label3.Text + " برای" + item.Name_Samll;
            }
        }

        private async void K1306_Item_ChooseCategory_Shown(object sender, EventArgs e)
        {
            if (Stack.Use_Web)
            {
                try
                {
                    lstCategories = await HttpClientExtensions.GetT<List<Models.Category>>
                         (Stack.API_Uri_start_read + "/Categories?all=no&company_Id=" + Stack.Company_Id, Stack.token);
                }
                catch { }
            }
            else
                lstCategories = Program.dbOperations.GetAllCategoriesAsync(Stack.Company_Id);

            if (lstCategories.Any())
            {
                comboBox1.Items.AddRange(lstCategories.Select(d => d.Name).ToArray());
                if (item == null)
                {
                    if (comboBox1.Items.Count > 0)
                        comboBox1.SelectedIndex = 0;
                }
                else
                {
                    if (comboBox1.Items.Count > 0)
                    {
                        if (lstCategories.Any(d=>d.Id == item.Category_Id))
                            comboBox1.Text = lstCategories.First(d => d.Id == item.Category_Id).Name;
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

            Stack.lx =  lstCategories.First(d=>d.Name.Equals(comboBox1.Text)).Id;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Stack.ix = -1;
            Close();
        }
    }
}
