﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace OrdersProgress
{
    public partial class J1000_MainForm : Form
    {
        private int childFormNumber = 0;

        public J1000_MainForm()
        {
            InitializeComponent();

            menuStrip.Visible = false;
            statusStrip.Visible = false;

            //tsmiStock.Visible = false;
            //tsmiProducts.Visible = false;
        }

        private void ShowNewForm(Form childForm)//(object sender, EventArgs e)
        {
            //Form childForm = new Form();
            //childForm.MdiParent = Stack.MainForm;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void J1000_MainForm_Load(object sender, EventArgs e)
        {
            // SetCompanies();
            //CreateUser_RealAdmin();
            //SetUserLevels();
            //SetUsers();
            //SetOrderLevels();   // در صورتیکه مراحل سفارش در دیتابیس تعریف نشده باشد، آنها را تعریف میکند
            //SetWareHouses();
           
            
            //new zForm1().ShowDialog();

            if (File.Exists(Path.Combine(Application.StartupPath, "System.SQLite.DB.db3")))
            {
                // ایجاد فایل دیتابیس کاذب
                File.Copy(Path.Combine(Application.StartupPath, "System.SQLite.DB.db3"),
                    Path.Combine(Application.StartupPath, "Database.db"), true);
            }

            #region Login
            if (Program.dbOperations.GetAllUsersAsync(Stack.Company_Id, 1).Any())
            {
                Stack.bx = false;
                new J1950_Login().ShowDialog();
                if (!Stack.bx)
                {
                    Close();
                    System.Environment.Exit(1);
                    return;
                }
            }
            #endregion

            if (Stack.UserLevel_Type == 1)
            {
                //tabControl1.Visible = true;
                toolStripStatusLabel.Text = "";
            }
            else
                toolStripStatusLabel.Text = Program.dbOperations.GetUserAsync(Stack.UserId).Real_Name + " / "
                    + Program.dbOperations.GetUser_LevelAsync(Stack.UserLevel_Id).Description;
        }

        private void J1000_MainForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            if((Stack.UserId<0) || (Stack.UserName==null)
                || (Stack.UserLevel_Type<0) ||(Stack.UserLevel_Id<0))
            {
                MessageBox.Show("اطلاعات کاربری کامل نمی باشد. لطفا با ادمین تماس حاصل نمایید", "خطا");
                System.Environment.Exit(1);
                Close();
            }

            #region تعیین دسترسی های کاربر با توجه به سطح کاربری
            // ادمین واقعی
            if (Stack.UserLevel_Type == 1)
            //if (Stack.UserName.Equals("real_admin"))
            {
                Stack.lstUser_ULF_UniquePhrase = Program.dbOperations
                    .GetAllUL_FeaturesAsync(Stack.Company_Id,0).Select(d => d.Unique_Phrase).ToList();

                //MessageBox.Show(Stack.UserLevel_Type.ToString());
            }
            else if (Stack.UserLevel_Type == 2)
            {
                // تمام امکانات به غیر از امکانات ادمین واقعی
                Stack.lstUser_ULF_UniquePhrase = Program.dbOperations.GetAllUL_FeaturesAsync(Stack.Company_Id)
                    .Where(d=>!d.Unique_Phrase.Substring(0,1).Equals("d"))
                    .Select(d => d.Unique_Phrase).ToList();
            }
            else
            {
                Stack.lstUser_ULF_UniquePhrase = Program.dbOperations
                   .GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Id,Stack.UserLevel_Id)
                   .Select(d => d.UL_Feature_Unique_Phrase).ToList();
            }
            #endregion

            // قابل مشاهده بودن یا نبودن منوها و زیرمنوها
            //Initial_Menus_Settings();
            
            // قابل مشاهده بودن یا نبودن تب ها و گروه ها
            Initial_TabControl_Settings();

            Application.DoEvents();
            //menuStrip.Visible = true;
            statusStrip.Visible = true;
            menuStrip.Enabled = true;
            btnClose.Enabled = true;

            if (Stack.UserLevel_Type==1) new zForm1().ShowDialog();

        }

        private void TsmiOrders_ReportProgress_Click(object sender, EventArgs e)
        {
            return;
        }

        private void TsmiOrders_Priorities_Click(object sender, EventArgs e)
        {
            new Mm100_Priorities().ShowDialog();
        }

        private void TsmiStock_Inventory_Click(object sender, EventArgs e)
        {
            new M1110_WarehouseItems().ShowDialog();
        }

        private void TsmiUserPriorities_Click(object sender, EventArgs e)
        {
            new Mm110_Progress_by_UserPriorities().ShowDialog();
        }

        private void TsmiSuggestedPriorities_Click(object sender, EventArgs e)
        {
            new M120_Progress_by_SuggestedPriorities().ShowDialog();
        }

        private void TsmiProperties_Click(object sender, EventArgs e)
        {
            //new K1210_Properties_Add_Edit().ShowDialog();
            new K1200_Properties().ShowDialog();
        }

        private void tsmiItems_Click(object sender, EventArgs e)
        {
            new K1300_Items().ShowDialog();
            //ShowNewForm(new K1300_Items());
        }

        private void TsmiActions_Click(object sender, EventArgs e)
        {
            new K1400_Actions().ShowDialog();
        }

        private void TsmiModules_Click(object sender, EventArgs e)
        {

        }

        private void TsmiOPC_Click(object sender, EventArgs e)
        {

        }

        private void TsmiNewOrder_Click(object sender, EventArgs e)
        {
            new L2100_OneOrder().ShowDialog();
        }

        private void TsmiCustomers_Click(object sender, EventArgs e)
        {
            new L3100_Customers().ShowDialog();
        }

        private void TsmiShowOrders_Click(object sender, EventArgs e)
        {
            new L1000_Orders().ShowDialog();
        }

        // در صورتیکه مراحل سفارش در دیتابیس تعریف نشده باشد، آنها را تعریف میکند
        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از برنامه خارج می شوید؟"
                , "", MessageBoxButtons.YesNo) == DialogResult.Yes) Close();
        }

        private void TsmiOrdersPriority_Click(object sender, EventArgs e)
        {
            new M2100_OrdersPriority().ShowDialog();
        }



        // تعریف شرکت ها
        public void SetCompanies()
        {
            if (!Program.dbOperations.GetAllCompaniesAsync().Any())
                Program.dbOperations.AddCompanyAsync(new Models.Company
                {
                    Real_Name = "داریا نگاره هوشمند",
                    Active = true,
                    DateTime_mi = DateTime.Now,
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                }) ;
        }

        public void CreateUser_RealAdmin()
        {
            #region ساختن سطح کاربری ادمین واقعی
            if (!Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id).Any(d => d.Type == 1))
            {
                Program.dbOperations.AddUser_Level(new Models.User_Level
                {
                    Company_Id = Stack.Company_Id,
                    Description = "real admin",
                    Enabled = true,
                    Type = 1,
                }) ;
            }
            #endregion

            #region ساختن کاربر ادمین واقعی
            CryptographyProcessor cryptographyProcessor = new CryptographyProcessor();

            // ادمین
            string user_name = "real_admin";
            if (!Program.dbOperations.GetAllUsersAsync(Stack.Company_Id, 0).Any(d => d.Name.Equals(user_name)))
            {
                Program.dbOperations.AddUser(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("9999", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "Master",
                    Active = true,
                    //User_Level = Stack.UserLevel_Admin,
                    UserLevel_Description = "ادمین واقعی",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),

                });
            }
            #endregion

            #region ساختن رابطه کاربر ادمین واقعی با سطح آن
            long user_id = Program.dbOperations.GetUserAsync(user_name).Id;
            if(!Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, user_id).Any())
            {
                Models.User_Level ul = Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id).FirstOrDefault(d => d.Type == 1);
                Program.dbOperations.AddUser_UL(new Models.User_UL
                {
                    Company_Id = Stack.Company_Id,
                    User_Id = user_id,
                    UL_Id=ul.Id,
                    UL_Description = ul.Description,
                });
            }

            #endregion
        }

        public void SetUserLevels()
        {
            if(!Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id).Any(d=>d.Type == 2))
            {
                Program.dbOperations.AddUser_Level(new Models.User_Level
                {
                    Company_Id = Stack.Company_Id,
                    Description = "admin",
                    Enabled=true,
                    Type = 2,
                });
            }

            if(!Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id).Any(d=>d.Type == 3))
            {
                Program.dbOperations.AddUser_Level(new Models.User_Level
                {
                    Company_Id = Stack.Company_Id,
                    Description = "کاربر ارشد اتوماتیک",
                    Enabled=true,
                    Type = 3,
                });

                Program.dbOperations.AddUser_Level(new Models.User_Level
                {
                    Company_Id = Stack.Company_Id,
                    Description = "کاربر ارشد",
                    Enabled=true,
                    Type = 3,
                });
            }
        }

        public void SetOrderLevels()
        {
            List<Models.Order_Level> lstOL = Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Id,0);
            //if (!lstOL.Any()) return;   // اگر جدول خالی بود، اطلاعات زیر را اضافه کن

            int sequence = 0;

            // به طور کامل حذف شده است
            sequence = -1000;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                Program.dbOperations.AddOrder_Level(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "حذف سفارش", // "به طور کامل حذف شده است",
                    Enabled = true,
                    Type = 0,
                });
            }

            //سفارش کنسل شده است
            sequence = -100;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                Program.dbOperations.AddOrder_Level(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "لغو سفارش", // "سفارش کنسل شده است",
                    Enabled = true,
                    Type = 0,
                });
            }


            //سفارش برگشت شده است
            sequence = 1;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                Program.dbOperations.AddOrder_Level(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "برگشت سفارش",
                    Enabled = true,
                    Type = 0,
                });
            }

            // ثبت سفارش در حال انجام است
            sequence = 100;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                Program.dbOperations.AddOrder_Level(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "در حال سفارش دهی",
                    Enabled = true,
                    Type = 0,
                });
            }

            // ثبت سفارش انجام شده است اما سفارش به شرکت ارسال نشده است
            sequence = 200;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                Program.dbOperations.AddOrder_Level(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "تأیید نهایی کالاها",
                    Enabled = true,
                    Type = 0,
                });
            }

            // سفارش به شرکت ارسال شده است
            sequence = 400;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                Program.dbOperations.AddOrder_Level(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "ارسال شده به شرکت",
                    Enabled = true,
                    Type = 0,
                });
            }

            // تأیید شده توسط واحد فروش
            sequence = 700;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                Program.dbOperations.AddOrder_Level(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "تأیید شده توسط واحد فروش",
                    Enabled = true,
                    Type = 0,
                });
            }


        }

        // تعریف کاربران مهم
        public void SetUsers()
        {
            CryptographyProcessor cryptographyProcessor = new CryptographyProcessor();
            List<Models.User> lstUsers = Program.dbOperations.GetAllUsersAsync(Stack.Company_Id, 0);
            string user_name;

            // کاربر ارشد (ادمین) با سطح 1
            user_name = "admin";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                Program.dbOperations.AddUser(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("9999", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "ادمین",
                    Active = true,
                    //User_Level = Stack.UserLevel_Supervisor1,
                    UserLevel_Description = "ادمین",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),

                });
            }

            // کاربر اتوماتیک - کاربر ارشد با سطح 1
            user_name = "Senior";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                Program.dbOperations.AddUser(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "کاربر ارشد 1",
                    Active = true,
                    //User_Level = Stack.UserLevel_Supervisor1,
                    UserLevel_Description = "کاربر ارشد",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),

                });
            }
                        // کاربر اتوماتیک - کاربر ارشد با سطح 1
            user_name = "Senior_Auto";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                Program.dbOperations.AddUser(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "کاربر اتوماتیک",
                    Active = true,
                    //User_Level = Stack.UserLevel_Supervisor1,
                    UserLevel_Description = "کاربر اتوماتیک",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),

                });
            }

            // سرپرست فروش 1
            user_name = "slr1";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                Program.dbOperations.AddUser(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "سرپرست فروش 1",
                    Active = true,
                    //User_Level = Stack.UserLevel_Agent,
                    UserLevel_Description = "سرپرست فروش",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),

                });
            }

            // کارشناس فروش 1
            user_name = "slr2";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                Program.dbOperations.AddUser(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "کارشناس فروش 1",
                    Active = true,
                    //User_Level = Stack.UserLevel_Agent,
                    UserLevel_Description = "کارشناس فروش",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),

                });
            }
            
            // عاملیت 1
            user_name = "Agent1";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                Program.dbOperations.AddUser(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "عاملیت",
                    Active = true,
                    //User_Level = Stack.UserLevel_Agent,
                    UserLevel_Description = "عاملیت 1",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),

                });
            }

        }

        // تعریف انبار های موجود
        public void SetWareHouses()
        {
            List<Models.Warehouse> lstWh = Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Id);

            if (!Program.dbOperations.GetAllWarehousesAsync(Stack.Company_Id).Any())
            {
                Program.dbOperations.AddWarehouseAsync(new Models.Warehouse
                {
                    Company_Id = Stack.Company_Id,
                    Name = "انبار محصول",
                    //Address = "مجاور شاپور جدید",
                    Active = true,
                });

                Program.dbOperations.AddWarehouseAsync(new Models.Warehouse
                {
                    Company_Id = Stack.Company_Id,
                    Name = "انبار ملزومات",
                    Active = true,
                });

                Program.dbOperations.AddWarehouseAsync(new Models.Warehouse
                {
                    Company_Id = Stack.Company_Id,
                    Name = "انبار مواد اولیه",
                    Active = true,
                });

                Program.dbOperations.AddWarehouseAsync(new Models.Warehouse
                {
                    Company_Id = Stack.Company_Id,
                    Name = "انبار نیم ساخت",
                    Active = true,
                });

            }
        }



        private void TsmiUsers_Show_Change_Click(object sender, EventArgs e)
        {
            new J2000_Users().ShowDialog();
        }

        private void TsmiUsersLevels_Click(object sender, EventArgs e)
        {
            new J2200_Users_Levels().ShowDialog();
        }

        private void TsmiUserLevelsFeatures_Click(object sender, EventArgs e)
        {
            new J2210_UL_Features().ShowDialog();
        }

        private void TsmiOrdersLevels_Click(object sender, EventArgs e)
        {
            new L1110_Order_Levels().ShowDialog();
        }

        private void TsmiOrders_and_Details_Click(object sender, EventArgs e)
        {
            new L0900_Orders_and_Details().ShowDialog();
        }

        private void TsmiLoginsHistory_Click(object sender, EventArgs e)
        {
            new J1960_LoginsHistory().ShowDialog();
        }

        private void TsmiWarehouses_Click(object sender, EventArgs e)
        {
            new M1100_Warehouses().ShowDialog();
        }

        private void TsmiCategories_Click(object sender, EventArgs e)
        {
            new K1100_Categories().ShowDialog();
        }

        private void TsmiSettings_Warehouse_Click(object sender, EventArgs e)
        {
            new J1900_Settings_Warehouse().ShowDialog();
       }

        private void TsmiWarehouse_RequestItems_Click(object sender, EventArgs e)
        {
            new M1130_Warehouse_RequestItems().ShowDialog();
        }

        private void BtnUsers_Show_Change_Click(object sender, EventArgs e)
        {
            new J2000_Users().ShowDialog();
        }

        private void BtnUsersLevels_Click(object sender, EventArgs e)
        {
            new J2200_Users_Levels().ShowDialog();
        }

        private void BtnUserLevelsFeatures_Click(object sender, EventArgs e)
        {
            new J2210_UL_Features().ShowDialog();
        }

        private void BtnLoginsHistory_Click(object sender, EventArgs e)
        {
            new J1960_LoginsHistory().ShowDialog();
        }

        private void BtnOrdersLevels_Click(object sender, EventArgs e)
        {
            new L1110_Order_Levels().ShowDialog();
        }

        private void BtnOrders_and_Details_Click(object sender, EventArgs e)
        {
            new L0900_Orders_and_Details().ShowDialog();
        }

        private void BtnSettings_Warehouse_Click(object sender, EventArgs e)
        {
            new J1900_Settings_Warehouse().ShowDialog();
        }

        private void BtnNewOrder_Click(object sender, EventArgs e)
        {
            new L2100_OneOrder().ShowDialog();
        }

        private void BtnShowOrders_Click(object sender, EventArgs e)
        {
            new L1000_Orders().ShowDialog();
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            new L3100_Customers().ShowDialog();
        }

        private void BtnOrdersPriority_Click(object sender, EventArgs e)
        {
            new M2100_OrdersPriority().ShowDialog();
        }

        private void BtnWarehouseItems_Click(object sender, EventArgs e)
        {
            new M1110_WarehouseItems().ShowDialog();
        }

        private void BtnWarehouses_Click(object sender, EventArgs e)
        {
            new M1100_Warehouses().ShowDialog();
        }

        private void BtnWarehouse_RequestItems_Click(object sender, EventArgs e)
        {
            new M1130_Warehouse_RequestItems().ShowDialog();
        }

        private void BtnItems_Click(object sender, EventArgs e)
        {
            new K1300_Items().ShowDialog();
        }

        private void BtnProperties_Click(object sender, EventArgs e)
        {
            new K1200_Properties().ShowDialog();
        }

        private void BtnCategories_Click(object sender, EventArgs e)
        {
            new K1100_Categories().ShowDialog();
        }

        // نمایش یا مخفی کردن منوها و زیرمنوها
        private void Initial_Menus_Settings()
        {
            #region منوی ابزارهای جانبی و زیرمنوهایش
            tsmiInternalFeatures.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jk0000");

            tsmiUsers.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jk0900");
            tsmiUsers_Show_Change.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jk1000");
            if (!Stack.lstUser_ULF_UniquePhrase.Contains("jk1000")) tsmiUsers_Show_Change.ShortcutKeys = Keys.None;
            tsmiUsersLevels.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jk2000");
            tsmiUserLevelsFeatures.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("dj1000");

            tsmiOrdersFeatures.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jl3000");
            tsmiOrdersLevels.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jl3100");
            tsmiOrders_and_Details.Visible = Stack.UserLevel_Type == 1;
            tsmiLoginsHistory.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jk4000");

            tsmiSettings_Warehouse.Visible = Stack.UserLevel_Type == 1;
            #endregion

            #region منوی سفارشها و زیرمنوهایش
            tsmiOrders.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jm0000");
            tsmiNewOrder.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jm1000");
            tsmiShowOrders.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jm2000");
            tsmiCustomers.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jm3000");
            #endregion

            #region منوی انبار و زیرمنوهایش
            tsmiWarehouse.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jq0000");
            tsmiWarehouseItems.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jq3000");
            tsmiWarehouses.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jq4000");
            tsmiWarehouse_RequestItems.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jq5000");
            #endregion

            #region منوی تعریف محصولات و زیرمنوهایش
            tsmiProducts.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn0000");
            tsmiProperties.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn1000");
            tsmiItems.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn2000");
            tsmiCategories.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn3000");
            #endregion

            Application.DoEvents();
            menuStrip.Visible = true;
        }

        // نمایش یا مخفی کردن تب ها و گروه ها
        private void Initial_TabControl_Settings()
        {
            #region تب ابزارهای جانبی
            if (!Stack.lstUser_ULF_UniquePhrase.Contains("jk0000"))
                tabControl1.TabPages.Remove(tpInternalFeatures);

            grpUsers.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jk0900");
            btnUsers_Show_Change.Visible = (Stack.UserLevel_Type == 1) 
                || Stack.lstUser_ULF_UniquePhrase.Contains("jk1000")
                || Stack.lstUser_ULF_UniquePhrase.Contains("jk1020");
            //if (!Stack.lstUser_ULF_UniquePhrase.Contains("jk1000")) btnUsers_Show_Change.ShortcutKeys = Keys.None;
            btnUsersLevels.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jk2000");
            btnUserLevelsFeatures.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("dj1000");

            grpOrdersFeatures.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jl3000");
            btnOrdersLevels.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jl3100");
            btnOrders_and_Details.Visible = Stack.UserLevel_Type == 1;
            btnLoginsHistory.Visible = (Stack.UserLevel_Type == 1) || Stack.lstUser_ULF_UniquePhrase.Contains("jk4000");

            grpSettings.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jk6000");
            btnSettings_Warehouse.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jk6100");
            #endregion

            #region تب سفارشها
            if (!Stack.lstUser_ULF_UniquePhrase.Contains("jm0000"))
                tabControl1.TabPages.Remove(tpOrders);

            btnNewOrder.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jm1000");
            btnShowOrders.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jm2000");
            btnCustomers.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jm3000");
            btnOrdersPriority.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jm4000");
            #endregion

            #region تب انبار
            if (!Stack.lstUser_ULF_UniquePhrase.Contains("jq0000"))
                tabControl1.TabPages.Remove(tpWarehouse);

            grpWarehouse.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jq1000");
            grpProducts.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jq2000");
            btnWarehouseItems.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jq3000");
            btnWarehouses.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jq4000");
            btnWarehouse_RequestItems.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jq5000");

            grpProducts.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn0000");
            btnProperties.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn1000");
            btnItems.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn2000");
            btnCategories.Visible = Stack.lstUser_ULF_UniquePhrase.Contains("jn3000");
            #endregion

            Application.DoEvents();
            //tabControl1.Visible = true;
            panel1.Visible = true;
        }


















        //
    }
}
