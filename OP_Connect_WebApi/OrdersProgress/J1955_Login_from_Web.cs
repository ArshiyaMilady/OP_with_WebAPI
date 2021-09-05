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

        private void BtnLogin_Click(object sender, EventArgs e)
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

            int login_type = radUseName.Checked ? 1 : 2;
            Models.User user = GetUser_by_Name_or_Mobile(login_type,txtNM.Text,txtPassword.Text);
            return;
            if (user == null)
            {
                MessageBox.Show(label1.Text.Substring(0, label1.Text.Length - 2) + " یا رمز ورود نادرست است", "خطا");
                return;
            }
            else
            {
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
                    Stack.bx = true;
                    Close();
                    return;
                }
            }

        }

        private Models.User GetUser_by_Name_or_Mobile(int login_type, string name_mobile, string password)
        {
            //MessageBox.Show(@"{""LoginType"": """ + login_type + @""", ""UserName_Mobile"": """ + name_mobile + @""", ""Password"": """ + password + @"""}");

            //MessageBox.Show(login_type + "\n" + name_mobile + "\n" + password);

            //return null;
            Models.User user = null;

            //var httpClient = new HttpClient();
            string response = HttpClientExtensions.GetToken(Stack.API_Uri_start + "/Token"
                , login_type, name_mobile, password).Result;
            //MessageBox.Show(response);
            if (!string.IsNullOrEmpty(response))
            {
                Stack.token = response;
                MessageBox.Show(Stack.token);
            }
            else 
            {
                MessageBox.Show("null");
            }


            //CryptographyProcessor cryptographyProcessor = new CryptographyProcessor();
            //if (!cryptographyProcessor.GenerateHash(txtPassword.Text, Stack.Standard_Salt).Equals(user.Password))
            //    return null;

            return user;
        }















        //
    }
}
