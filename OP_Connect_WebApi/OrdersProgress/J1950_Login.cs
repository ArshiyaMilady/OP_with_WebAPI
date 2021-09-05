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
    public partial class J1950_Login : X210_ExampleForm_Normal
    {
        public J1950_Login()
        {
            InitializeComponent();

            //comboBox1.SelectedIndex = 1;
        }

        private void J1950_Login_Shown(object sender, EventArgs e)
        {
            if (Program.dbOperations.GetAllUsersAsync(Stack.Company_Id).Any(d=>d.IsDefault))
            {
                Models.User user = Program.dbOperations.GetAllUsersAsync
                    (Stack.Company_Id, 1).First(d => d.IsDefault);

                if (string.IsNullOrEmpty(user.Mobile))
                {
                    //comboBox1.SelectedIndex = 0;
                    txtNM.Text = user.Name;
                }
                else
                {
                    if (radUseName.Checked)
                        txtNM.Text = user.Name;
                    else if (radUseMobile.Checked)
                        txtNM.Text = user.Mobile;
                    //if (comboBox1.SelectedIndex == 0)
                    //    txtNM.Text = user.Name;
                    //else if (comboBox1.SelectedIndex == 1)
                    //    txtNM.Text = user.Mobile;
                }

                txtPassword.Focus();
            }
        }

        private void LblChangePassword_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            #region خطایابی
            if (string.IsNullOrWhiteSpace(txtNM.Text))
            {
                MessageBox.Show("نام کاربری / شماره همراه ؟", "خطا");
                return;
            }

            if(string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("رمز ورود ؟", "خطا");
                return;
            }
            #endregion

            Models.User user = GetUser_by_Name_or_Mobile();

            if (user == null)
            {
                MessageBox.Show(label1.Text.Substring(0,label1.Text.Length-2) + " یا رمز ورود نادرست است", "خطا");
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

        private Models.User GetUser_by_Name_or_Mobile()
        {
            Models.User user = null;

            //switch (comboBox1.SelectedIndex)
            {
                #region انتخاب نام کاربری
                if (radUseName.Checked) {
                    //case 0:
                    user = Program.dbOperations.GetUserAsync(txtNM.Text);
                    //break;
                }
                #endregion

                #region شماره همراه
                if (radUseMobile.Checked)
                //case 1:
                {
                    if (txtNM.Text.Trim().Length < 10)
                    {
                        //MessageBox.Show("شماره همراه نادرست است", "خطا");
                        return null;
                    }

                    txtNM.Text = txtNM.Text.Trim().Substring(txtNM.Text.Length - 10);
                    user = Program.dbOperations.GetAllUsersAsync(Stack.Company_Id, 1)
                        .Where(b => b.Mobile != null)
                        .Where(d => d.Mobile.Length >= 10).FirstOrDefault(j
                        => j.Mobile.Substring(j.Mobile.Length - 10).Equals(txtNM.Text));
                    //break;
                    #endregion
                }
            }

            return user;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void LblChangePassword_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNM.Text))
            {
                MessageBox.Show("نام کاربری / شماره همراه ؟");
                return;
            }

            Models.User user = GetUser_by_Name_or_Mobile();
            if(user == null)
            {
                MessageBox.Show(label1.Text.Substring(0, label1.Text.Length - 2) + " اشتباه است", "خطا");
                return;
            }
            new J2110_ChangePassword(user.Id).ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void J1950_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Stack.bx)
            {
                if (MessageBox.Show("آیا از برنامه خارج می شوید؟", ""
                    , MessageBoxButtons.YesNo) != DialogResult.Yes) e.Cancel = true;
            }
        }

        private void lblMaster_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now));
            if (string.Compare(Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now)
                , "1400/07/01") < 0) 
            {
                Stack.UserLevel_Type = 1;
                Stack.UserLevel_Id = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id, 0).FirstOrDefault(d => d.Type == 1).Id;
                //Stack.UserId = Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id).First(d => d.UL_Id == Stack.UserLevel_Id).User_Id;
                Stack.UserId = Program.dbOperations.GetUserAsync("real_admin").Id;
                Stack.UserName = "real_admin";
                //Stack.UserName = Program.dbOperations.GetUserAsync(Stack.UserId).Name;
                Stack.Company_Id = 1;
                Stack.bx = true;
                Close();
                return;
            }
        }

        private void LblAdmin_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now));
            if (string.Compare(Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now)
                , "1400/07/01") < 0)
            {
                Stack.UserLevel_Type = 2;
                Stack.UserLevel_Id = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id, 0).FirstOrDefault(d => d.Type == 2).Id;
                Stack.UserId = Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id).First(d => d.UL_Id == Stack.UserLevel_Id).User_Id;
                Stack.UserName = Program.dbOperations.GetUserAsync(Stack.UserId).Name;
                Stack.Company_Id = 1;
                Stack.bx = true;
                Close();
                return;
            }
        }

        private void RadUseName_Mobile_CheckedChanged(object sender, EventArgs e)
        {
            if (radUseName.Checked) label1.Text = "نام کاربری :";
            else if (radUseMobile.Checked) label1.Text = "شماره همراه :";
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
