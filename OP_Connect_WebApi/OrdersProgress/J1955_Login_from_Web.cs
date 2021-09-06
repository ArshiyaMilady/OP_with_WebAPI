using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdersProgress
{
    public partial class J1955_Login_from_Web : X210_ExampleForm_Normal
    {
        public J1955_Login_from_Web()
        {
            InitializeComponent();
        }

        private void J1955_Login_from_Web_Shown(object sender, EventArgs e)
        {

        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            #region خطایابی
            if (string.IsNullOrWhiteSpace(txtNM.Text))
            {
                MessageBox.Show("نام کاربری / شماره همراه ؟", "خطا");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("رمز ورود ؟", "خطا");
                return;
            }
            #endregion

            pictureBox1.Visible = true;
            panel2.Enabled = false;

            int login_type = radUseName.Checked ? 1 : 2;
            Models.User user = null;
            try {  user = await GetUser_by_Name_or_Mobile(login_type, txtNM.Text, txtPassword.Text); }
            catch { }

            if (user == null)
            {
                panel2.Enabled = true;
                pictureBox1.Visible = false;

                MessageBox.Show(label1.Text.Substring(0, label1.Text.Length - 2) + " یا رمز ورود نادرست است", "خطا");
                return;
            }
            else
            {
                //bool b =;
                if (await Stack_Methods.GetAllUserData_web(user))
                {
                    #region ثبت در تایخچه
                    if (Stack.UserLevel_Type != 1)
                    {
                        Models.LoginHistory loginHistory = new Models.LoginHistory
                        {
                            Company_Id = user.Company_Id,
                            User_Id = user.Id,
                            User_RealName = user.Real_Name,
                            DateTime_mi = DateTime.Now,
                            Date_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now),
                            Time = Stack_Methods.NowTime_HHMMSSFFF(":", false),
                        };

                        await HttpClientExtensions.PostAsJsonAsync<Models.LoginHistory>
                            (Stack.API_Uri_start + "/LoginHistories", loginHistory, Stack.token);
                    }
                    #endregion

                    Stack.bx = true;
                    Close();
                }
                else
                {
                    panel2.Enabled = true;
                    pictureBox1.Visible = false;
                    MessageBox.Show("جهت بررسی مشکل با شرکت تماس بگیرید", "خطا در اطلاعات کاربری");
                }
                return;
            }

        }

        private async Task<Models.User> GetUser_by_Name_or_Mobile(int login_type, string name_mobile, string password)
        {
            Models.User user = null;

            string response = await HttpClientExtensions.GetToken(Stack.API_Uri_start + "/Token"
                , login_type, name_mobile, password);

            if (!string.IsNullOrEmpty(response))
            {
                Stack.token = response;
                return await HttpClientExtensions.GetT<Models.User>(Stack.API_Uri_start_read
                    + "/Users/0?user_name=" + name_mobile,Stack.token);
                //MessageBox.Show(Stack.token);
            }

            return user;
        }

        private void LblExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void J1955_Login_from_Web_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Stack.bx)
            {
                if (MessageBox.Show("آیا از برنامه خارج می شوید؟", ""
                    , MessageBoxButtons.YesNo) != DialogResult.Yes) e.Cancel = true;
            }
        }

        private void LblMaster_Click(object sender, EventArgs e)
        {
            radUseName.Checked = true;
            txtNM.Text = "real_admin";
            txtPassword.Text = "9999";
        }

        private void LblAdmin_Click(object sender, EventArgs e)
        {
            radUseName.Checked = true;
            txtNM.Text = "admin";
            txtPassword.Text = "9999";
        }















        //
    }
}
