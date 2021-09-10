using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            try
            {  user = await GetUser_by_Name_or_Mobile(login_type, txtNM.Text, txtPassword.Text); }
            catch { }

            if (user == null)
            {
                panel2.Enabled = true;
                pictureBox1.Visible = false;
                if(Stack.bx)
                    MessageBox.Show(label1.Text.Substring(0, label1.Text.Length - 2) + " یا رمز ورود نادرست است", "خطا");
                else
                    MessageBox.Show("اشکال در ارتباط با سرور. لطفا مجددا امتحان نمایید", "خطا");
                return;
            }
            else
            {
                //bool b =;
                if (await GetAllUserData_web(user))
                {
                    #region ثبت در تایخچه
                    if (Stack.UserLevel_Type != 1)
                    {
                        Models.LoginHistory loginHistory = new Models.LoginHistory
                        {
                            Company_Id = user.Company_Id,
                            User_Id = user.Id,
                            User_RealName = user.Real_Name,
                            DateTime_mi = DateTime.Now.ToString(),
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
            Stack.bx = true;
            Models.User user = null;

            string response = await HttpClientExtensions.GetToken(Stack.API_Uri_start + "/Token"
                , login_type, name_mobile, password);
            //MessageBox.Show(response);
            if (!string.IsNullOrEmpty(response))
            {
                Stack.token = response;
                //MessageBox.Show(Stack.token);
                try
                {
                    user = await HttpClientExtensions.GetT<Models.User>(Stack.API_Uri_start_read
                        + "/Users/0?name_mobile=" + name_mobile + "&login_type=" + login_type, Stack.token);
                }
                catch { Stack.bx = false; }
                //MessageBox.Show(user.Real_Name);
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

        // شناسه، سطح دسترسی و ... را برای یک کاربر با معلوم بودن نام بر میگرداند
        public async Task<bool> GetAllUserData_web(Models.User user)
        {
            Stack.UserName = user.Name;
            Stack.User_RealName = user.Real_Name;
            Stack.UserId = user.Id;
            Stack.Company_Id = user.Company_Id;
            //MessageBox.Show(Stack.token);
            //MessageBox.Show(Stack.UserId.ToString());
            //List<Models.User_UL> lstUUL = await HttpClientExtensions.GetT<List<Models.User_UL>>
            //    (Stack.API_Uri_start_read + "/User_UL?all=no&company_id="+Stack.Company_Id+"&user_id=" + user.Id, Stack.token);
            //if ((lstUUL!=null) && lstUUL.Any())
            {
                Models.User_Level user_level = await HttpClientExtensions.GetT<Models.User_Level>
                    (Stack.API_Uri_start_read + "/User_Levels/0?user_id=" + Stack.UserId, Stack.token);
                Stack.UserLevel_Id = user_level.Id;
                Stack.UserLevel_Type = user_level.Type;
                //MessageBox.Show(Stack.UserLevel_Type.ToString(), "1");
                // نام و سطح دسترسی کاربر
                Stack.sx = user.Real_Name + " / " + user_level.Description;
            }
            //MessageBox.Show(user.Company_Id.ToString());
            Models.Company company = await HttpClientExtensions.GetT<Models.Company>
                (Stack.API_Uri_start_read + "/Companies/" + user.Company_Id, Stack.token);
            Stack.bWarehouse_Booking_MaxHours = company.Warehouse_AutomaticBooking;

            //MessageBox.Show(Stack.UserLevel_Type.ToString());
            #region تعیین دسترسی های کاربر با توجه به سطح کاربری
            var res1 = await HttpClientExtensions.GetT<List<Models.UL_Feature>>
                (Stack.API_Uri_start_read + "/UL_Feature?company_Id=" + Stack.Company_Id
                + "&EnableType=" + 1 + "&ul_Id=" + Stack.UserLevel_Id, Stack.token);
            Stack.lstUser_ULF_UniquePhrase = res1.Select(d => d.Unique_Phrase).ToList();
            //MessageBox.Show(Stack.lstUser_ULF_UniquePhrase.Count.ToString());
            #endregion

            return (Stack.UserId > 0) && (Stack.UserLevel_Id > 0) && (Stack.UserLevel_Type >= 0);
        }

        // --- با توجه به نبود توکن تا این مرحله ، امکان تغییر رمز در اینجا وجود ندارد
        private void LblChangePassword_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtNM.Text))
            //{
            //    MessageBox.Show("نام کاربری / شماره همراه ؟");
            //    return;
            //}

            //int login_type = radUseName.Checked ? 1 : 2;
            //Models.User user = null;
            ////try
            //{
            //    user = await HttpClientExtensions.GetT<Models.User>
            //        (Stack.API_Uri_start_read + "/Users/0?user_name=" + txtNM.Text + "&login_type=" + login_type, Stack.token);
            //}
            ////catch { }

            //if (user == null)
            //{
            //    MessageBox.Show(label1.Text.Substring(0, label1.Text.Length - 2) + " اشتباه است", "خطا");
            //    return;
            //}
            //new J2110_ChangePassword(user.Id).ShowDialog();
        }















        //
    }
}
