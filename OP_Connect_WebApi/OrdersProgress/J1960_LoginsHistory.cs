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
    public partial class J1960_LoginsHistory : X210_ExampleForm_Normal
    {
        long user_id = 0;

        public J1960_LoginsHistory(long _user_id=0)
        {
            InitializeComponent();

            if (_user_id > 0)
            {
                user_id = _user_id;
                Text = Stack.User_RealName;
            }
        }

        private async void J1960_LoginsHistory_Shown(object sender, EventArgs e)
        {
            dgvData.DataSource = await GetData();
            ShowData();
        }

        private async Task<List<Models.LoginHistory>> GetData()//bool NeedToCorrect_C_B1 = true)
        {
            if (Stack.Use_Web)
                return (await HttpClientExtensions.GetT<List<Models.LoginHistory>>
                    (Stack.API_Uri_start_read+ "/LoginHistories",Stack.token))
                    .OrderByDescending(d => d.Date_sh).ThenByDescending(j=>j.Time).ToList();
            else
                return Program.dbOperations.GetAllLoginHistorysAsync(Stack.Company_Id, user_id)
                    .OrderByDescending(d=>d.DateTime_mi).ToList();
        }

        private void ShowData()//bool ChangeHeaderTexts = true)
        {
            #region ترجمه سر ستونها و مخفی کردن بعضی ستونها
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                switch (col.Name)
                {
                    //case "User_Id":
                    //    col.HeaderText = "شناسه کاربر";
                    //    col.Width = 150;
                    //    break;
                    case "User_RealName":
                        col.HeaderText = "نام کاربر";
                        col.Width = 200;
                        break;
                    case "Date_sh":
                        col.HeaderText = "تاریخ";
                        col.Width = 150;
                        break;
                    case "Time":
                        col.HeaderText = "زمان";
                        col.Width = 150;
                        break;
                    default: col.Visible = false; break;
                }
            }
            #endregion
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
