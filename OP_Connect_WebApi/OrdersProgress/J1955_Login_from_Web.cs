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
                var b = 
                if (await Stack_Methods.GetAllUserData_web(user).Status)
                {
                    MessageBox.Show(Stack.UserLevel_Type.ToString());
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

                /*
                CryptographyProcessor cryptographyProcessor = new CryptographyProcessor();
                if (!cryptographyProcessor.GenerateHash(txtPassword.Text, Stack.Standard_Salt).Equals(user.Password))
                {
                    MessageBox.Show(label1.Text.Substring(0, label1.Text.Length - 2) + " یا رمز ورود نادرست است", "خطا");
                    return;
                }
                else
                {
                    if (!Stack_Methods.GetAllUserData(user.Name))
                    {
                        //MessageBox.Show(Stack.UserName,"name");
                        //MessageBox.Show(Stack.UserLevel_Type.ToString(),"type");
                        //MessageBox.Show(Stack.UserId.ToString(),"user id");
                        //MessageBox.Show(Stack.UserLevel_Id.ToString(), "UserLevel_Id");
                        MessageBox.Show("جهت بررسی مشکل با شرکت تماس بگیرید", "خطا در اطلاعات کاربری");
                        return;
                    }

                    #region کاربر پیش فرض
                    if (!user.IsDefault)
                    {
                        //MessageBox.Show("200");
                        if (chkDefaultUser.Checked)
                        {
                            //MessageBox.Show("300");
                            //if (!user.Name.Equals("real_admin"))
                            if ((Stack.UserLevel_Type != 1) && (Stack.UserLevel_Type != 2))
                            {
                                //MessageBox.Show("400");
                                Models.User default_user = Program.dbOperations.GetAllUsersAsync
                                    (Stack.Company_Id, 1).FirstOrDefault(d => d.IsDefault);
                                if (default_user != null)
                                {
                                    default_user.IsDefault = false;
                                    Program.dbOperations.UpdateUserAsync(default_user);
                                }
                                //MessageBox.Show(user.Name);
                                user.IsDefault = true;
                                Program.dbOperations.UpdateUserAsync(user);
                            }
                        }
                    }
                    //MessageBox.Show("500",user.IsDefault.ToString());

                    #endregion

                    #region ذخیره در تاریخچه ورود
                    if (Stack.UserLevel_Type != 1)
                        Program.dbOperations.AddLoginHistoryAsync(new Models.LoginHistory
                        {
                            Company_Id = user.Company_Id,
                            User_Id = user.Id,
                            User_RealName = user.Real_Name,
                            DateTime_mi = DateTime.Now,
                            Date_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now),
                            Time = Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 8),
                        });
                    #endregion

                    //MessageBox.Show(Stack.UserLevel_Type.ToString());
                }
                */
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















        //
    }
}
