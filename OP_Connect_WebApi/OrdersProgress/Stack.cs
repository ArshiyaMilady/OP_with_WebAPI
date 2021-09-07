﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OrdersProgress
{
    static class Stack
    {
        public static bool Use_Web = true;

        public static string Standard_Salt = "d;lkjWeoj'l;ksDfok';lsdkovjmwEfl;kwikvxc.m,/Zviejkjds;flKjoremwa;lm";  // کاملا رندوم

        //public static string API_Uri_start = "http://www.opwa.somee.com/api";
        //public static string API_Uri_start_read = "http://www.opwa.somee.com/api";
        public static string API_Uri_start = "http://localhost:6238/api";
        public static string API_Uri_start_read = "https://localhost:44380/api";
        public static string token;

        public static bool bx;
        public static int ix;
        public static long lx;
        public static string sx;

        // فعلا شناسه کاربر 1201 باشد
        public static long UserId = -1; // شناسه کاربری که با نرم افزار کار می کند
        public static string UserName = null;
        public static string User_RealName = null;
        public static int UserLevel_Type = -1;   // سطح کاربری عادی  = تمام سطوح به غیر از ادمین و ادمین واقعی
        public static long UserLevel_Id = -1;
        public static long Company_Id = 1;

        // false : رزرو کالاها از انبار به صورت دستی
        // true : رزرو کالاها از انبار به صورت اتوماتیک
        public static bool bWarehouse_Booking_MaxHours = false;


        // امکانات کاربر وارد شده را در خود نگه می دارد
        public static List<string> lstUser_ULF_UniquePhrase = new List<string>();

        //public static string Standard_Salt = ";hchfghk;h";  // کلمه "کازابلانکا" وقتی کیبورد انگلیسی است


        public const int OrderLevel_Removed = -10001;// { get { return -10001; } }
        public const int OrderLevel_Canceled = -200;// { get { return -200; } }
        public const int OrderLevel_Returned = -100; // { return -100; } }
        public const int OrderLevel_Ordering1_ConfirmItems = 100; // { return 100; } }        // در حال ثبت سفارش - کالاهای سفارش تأیید شده اند
        public const int OrderLevel_OrderCompleted = 200; // { return 200; } }  // ثبت سفارش کامل شده است
        public const int OrderLevel_SendToCompany = 300; // { return 300; } }   // به کارخانه ارسال شده است
        public const int OrderLevel_SaleConfirmed = 400; // { return 400; } }   // تأیید شده توسط واحد فروش
        public const int OrderLevel_FinancialConfirmed = 500; // { return 500; } }   // تأیید شده توسط واحد مالی
        public const int OrderLevel_Producting = 700; // { return 700; } }   // در حال تولید
        public const int OrderLevel_Producted = 800; // { return 800; } }   // تولید تکمیل شده است

        public const int OrderLevel_SentOut = 10000; // { return 10000; } }   // سفارش ارسال شده است

        public const int UserLevel_Admin = 10; // { return 10; } }
        public const int UserLevel_Supervisor1 = 101; // { return 101; } }
        public const int UserLevel_Supervisor2 = 102; // { return 102; } }
        public const int UserLevel_Supervisor3 = 103; // { return 103; } }        
        public const int UserLevel_RegOrderUnit = 1002; // واحد ثبت سفارش
        public const int UserLevel_SaleManager = 1011; //  سرپرست فروش
        public const int UserLevel_SaleUnit = 1012; // واحد فروش
        public const int UserLevel_FinancialUnit = 1022; // واحد مالی
        public const int UserLevel_PlanningUnit = 1032; // واحد برنامه ریزی
        public const int UserLevel_Agent = 2001; // { return 2001; } }  

        //public static Models.Order order_actions { get; set; }
    }

    static class Stack_Methods
    {
        public static DateTime DateTime_Miladi(DateTime dateTime)
        {
            return dateTime;

            //DateTime d = dateTime;
            //// 484,375ms
            //d = new DateTime((d.Ticks / TimeSpan.TicksPerSecond) * TimeSpan.TicksPerSecond);
            //// 1296,875ms
            //d = d.AddMilliseconds(-d.Millisecond);
            //return d;

            //return dateTime.AddMilliseconds(-dateTime.Millisecond);

            //DateTime dateTime = date_time;
            //return new DateTime(
            //    dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond),
            //    dateTime.Kind
            //    );
        }

        public static string Miladi_to_Shamsi_YYYYMMDD(DateTime dtMiladi, string Between = "/")
        {
            var pc = new System.Globalization.PersianCalendar();
            int iMonth = pc.GetMonth(dtMiladi);
            string sMonth = (iMonth < 10) ? ("0" + iMonth) : iMonth.ToString();
            int iDay = pc.GetDayOfMonth(dtMiladi);
            string sDay = (iDay < 10) ? ("0" + iDay) : iDay.ToString();
            return (pc.GetYear(dtMiladi).ToString() + Between + sMonth + Between + sDay);
        }

        public static string NowTime_HHMMSSFFF(string between = ":", bool HasMillisecond = true)
        {
            string sDT = DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss");
            if(HasMillisecond)
                sDT = sDT + ":" + DateTime.Now.Millisecond;
            if (!between.Equals(":")) sDT = sDT.Replace(":", between);
            return sDT;
        }

        // با توجه به دو تابع تاریخ و زمان، برای زمان امروز، رشته ای با فرمت زیر را بر میگرداند
        // YYYY/MM/DD-HH:MM:SS
        public static string DateTimeNow_Shamsi(string BetweenDate = "/", string BetweenTime = ":",bool HasMillisecond = false)
        {
            string sMiladi_to_Shamsi_YYYYMMDD = Miladi_to_Shamsi_YYYYMMDD(DateTime.Now, BetweenDate)
                + "-" + NowTime_HHMMSSFFF(BetweenTime);
            if (HasMillisecond) return sMiladi_to_Shamsi_YYYYMMDD;
            else
            {
                // تعداد کاراکتر زمان بدون میلی ثانیه
                int nTimeLength = 15 + 2 * BetweenDate.Length+ 2 * BetweenTime.Length;
                return sMiladi_to_Shamsi_YYYYMMDD.Substring(0, nTimeLength);
            }
        }

        // شناسه، سطح دسترسی و ... را برای یک کاربر با معلوم بودن نام بر میگرداند
        public static bool GetAllUserData(string user_name)
        {
            Stack.UserName = user_name;
            Models.User user = Program.dbOperations.GetUserAsync(user_name);
            Stack.User_RealName = user.Real_Name;
            Stack.UserId = user.Id;
            Stack.Company_Id = user.Company_Id;
            if (Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, Stack.UserId).Any())
            {
                Stack.UserLevel_Id = Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, Stack.UserId).First().UL_Id;
                Models.User_Level user_level = Program.dbOperations.GetUser_LevelAsync(Stack.UserLevel_Id);
                Stack.UserLevel_Type = user_level.Type;
                // نام و سطح دسترسی کاربر
                Stack.sx = user.Real_Name + " / " + user_level.Description;
            }


            Stack.bWarehouse_Booking_MaxHours = Program.dbOperations.GetCompanyAsync(user.Company_Id).Warehouse_AutomaticBooking;

            #region تعیین دسترسی های کاربر با توجه به سطح کاربری
            // ادمین واقعی
            if (Stack.UserLevel_Type == 1)
            //if (Stack.UserName.Equals("real_admin"))
            {
                Stack.lstUser_ULF_UniquePhrase = Program.dbOperations
                    .GetAllUL_FeaturesAsync(Stack.Company_Id, 0).Select(d => d.Unique_Phrase).ToList();

                //MessageBox.Show(Stack.UserLevel_Type.ToString());
            }
            else if (Stack.UserLevel_Type == 2)
            {
                // تمام امکانات به غیر از امکانات ادمین واقعی
                Stack.lstUser_ULF_UniquePhrase = Program.dbOperations.GetAllUL_FeaturesAsync(Stack.Company_Id)
                    .Where(d => !d.Unique_Phrase.Substring(0, 1).Equals("d"))
                    .Select(d => d.Unique_Phrase).ToList();
            }
            else
            {
                Stack.lstUser_ULF_UniquePhrase = Program.dbOperations
                   .GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Id, Stack.UserLevel_Id)
                   .Select(d => d.UL_Feature_Unique_Phrase).ToList();
            }
            #endregion
            return ((Stack.UserId > 0) && (Stack.UserLevel_Id > 0) && (Stack.UserLevel_Type >= 0));
        }

        // شناسه، سطح دسترسی و ... را برای یک کاربر با معلوم بودن نام بر میگرداند
        public static async Task<bool> GetAllUserData_web(Models.User user)
        {
            Stack.UserName = user.Name;
            Stack.User_RealName = user.Real_Name;
            Stack.UserId = user.Id;
            Stack.Company_Id = user.Company_Id;   
            //MessageBox.Show(Stack.UserId.ToString(),"1");
            //List<Models.User_UL> lstUUL = await HttpClientExtensions.GetT<List<Models.User_UL>>
            //    (Stack.API_Uri_start_read + "/User_UL?all=no&company_id="+Stack.Company_Id+"&user_id=" + user.Id, Stack.token);
            //if ((lstUUL!=null) && lstUUL.Any())
            {
                Models.User_Level user_level = await HttpClientExtensions.GetT<Models.User_Level>
                    (Stack.API_Uri_start_read + "/User_Levels/0?user_id=" + Stack.UserId, Stack.token);
                Stack.UserLevel_Id = user_level.Id;
                Stack.UserLevel_Type = user_level.Type;
                // نام و سطح دسترسی کاربر
                Stack.sx = user.Real_Name + " / " + user_level.Description;
            }
            //MessageBox.Show(user.Company_Id.ToString());
            Models.Company company = await HttpClientExtensions.GetT<Models.Company>
                (Stack.API_Uri_start_read + "/Companies/" + user.Company_Id, Stack.token);
            Stack.bWarehouse_Booking_MaxHours = company.Warehouse_AutomaticBooking;

            //MessageBox.Show(Stack.UserLevel_Type.ToString());
            #region تعیین دسترسی های کاربر با توجه به سطح کاربری
            // ادمین واقعی
            var res1 = await HttpClientExtensions.GetT<List<Models.UL_Feature>>
                (Stack.API_Uri_start_read + "/UL_Feature?company_Id=" + Stack.Company_Id
                + "&EnableType=" + 1 + "&ul_Id=" + Stack.UserLevel_Id, Stack.token);
            Stack.lstUser_ULF_UniquePhrase = res1.Select(d => d.Unique_Phrase).ToList();
            //MessageBox.Show(Stack.lstUser_ULF_UniquePhrase.Count.ToString());
            #endregion

            return ((Stack.UserId > 0) && (Stack.UserLevel_Id > 0) && (Stack.UserLevel_Type >= 0));
        }

        // شناسه تمام کاربران استاندارد مانند ادمین اصلی و ادمین و کاربر ارشد را بر میگرداند
        public static List<long> GetStandardUsersIndex(long user_level_type=100)
        {
            List<long> lstUL = new List<long>();
            if(user_level_type == 10)
            {
                foreach(Models.User_Level ul in Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id)
                    .Where(d=>d.Type>0).ToList())
                        lstUL.Add(ul.Id);
            }
            else
            {
                foreach (Models.User_Level ul in Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id)
                    .Where(d => d.Type ==user_level_type).ToList())
                        lstUL.Add(ul.Id);
            }
            List<long> lstResult = new List<long>();
            foreach (long ul_index in lstUL)
                lstResult.AddRange(Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Id, 0, ul_index)
                    .Select(d=>d.User_Id).ToArray());
            return lstResult;
        }
    }

    public class CryptographyProcessor
    {
        // طول رشته خرجی مضربی از 8 می باشد
        // حتی اگر عدد ورودی تابع مضربی از 8 نباشد
        public string CreateRandomSalt(int size = 32)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // RealHashedValue : رشته هش شده با سالت که در دیتابیس ذخیره شده است
        // InputValue : مقدار ورودی بدون هش که باید بررسی شود
        // salt : سالتی که باید بر روی مقدار ورودی اعمال شود و با رشته هش شده مقایسه شود
        public bool AreEqual(string RealHashedValue, string InputValue, string salt)
        {
            string InputHashedPin = GenerateHash(InputValue, salt);
            InputHashedPin = GenerateHash(InputHashedPin, ";hchfghk;h");  // کلمه "کازابلانکا" وقتی کیبورد انگلیسی است
            //string RealHashedPin = GenerateHash(RealHashPassword, ";hchfghk;h");  // کلمه "کازابلانکا" وقتی کیبورد انگلیسی است
            //return InputHashedPin.Equals(RealHashedPin);
            return InputHashedPin.Equals(RealHashedValue);
        }

        public string GenerateHash_2Times(string input, string salt1, string salt2)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(salt1) || string.IsNullOrEmpty(salt2))
                return null;

            string s = GenerateHash(input, salt1);
            return GenerateHash(s, salt2);
        }

        public string XOR_2Strings(string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
                result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));

            return result.ToString();
        }
    }

    public static class HttpClientExtensions
    {
        public static async Task<string> GetAsJsonAsync_String(string requestUri)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage res = await httpClient.GetAsync(requestUri);
            res.EnsureSuccessStatusCode();
            if (res.IsSuccessStatusCode)
                return await res.Content.ReadAsStringAsync();
            else return null;
        }

        public static async Task<T> GetT<T>(string requestUri, string BeererAuthorizedToken = null)
        {
            HttpClient httpClient = new HttpClient();
            //if (httpClient.GetStringAsync(requestUri).Status == TaskStatus.RanToCompletion)
            {
                if (!string.IsNullOrEmpty(BeererAuthorizedToken))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BeererAuthorizedToken);
                var response = await httpClient.GetStringAsync(requestUri);
                return JsonConvert.DeserializeObject<T>(response);
            }
            //return Nullable<T>();
        }

        public static async Task<HttpResponseMessage> DeleteAsJsonAsync<T>
            (string requestUri, T data, string BeererAuthorizedToken = null)
        {
            HttpClient httpClient = new HttpClient();
            if (!string.IsNullOrEmpty(BeererAuthorizedToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BeererAuthorizedToken);
            //HttpResponseMessage res = await httpClient.GetAsync(requestUri);
            //res.EnsureSuccessStatusCode();
            //if (res.IsSuccessStatusCode)
            var res1 = await httpClient.SendAsync(new HttpRequestMessage
                    (HttpMethod.Delete, requestUri)
            { Content = Serialize(data) });
            if (res1.IsSuccessStatusCode) return res1;
            else return null;
        }

        public static async Task<HttpResponseMessage> DeleteAsJsonAsync2<T>
            (string requestUri, string BeererAuthorizedToken = null)
        {
            HttpClient httpClient = new HttpClient();
            if (!string.IsNullOrEmpty(BeererAuthorizedToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BeererAuthorizedToken);
            //HttpResponseMessage res = await httpClient.GetAsync(requestUri);
            //res.EnsureSuccessStatusCode();
            //if (res.IsSuccessStatusCode)
            var res1 = await httpClient.SendAsync(new HttpRequestMessage
                    (HttpMethod.Delete, requestUri));
            if (res1.IsSuccessStatusCode) return res1;
            else return null;
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T data, string BeererAuthorizedToken = null)
        {
            HttpClient httpClient = new HttpClient();
            if (!string.IsNullOrEmpty(BeererAuthorizedToken))
                httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", BeererAuthorizedToken);
            return await httpClient.SendAsync(new HttpRequestMessage
                    (HttpMethod.Put, requestUri)
            { Content = Serialize(data) });
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync_byCheck<T>(string requestUri, T data, string BeererAuthorizedToken = null)
        {
            HttpClient httpClient = new HttpClient();
            if (!string.IsNullOrEmpty(BeererAuthorizedToken))
                httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", BeererAuthorizedToken);
            HttpResponseMessage res = await httpClient.GetAsync(requestUri);
            //res.EnsureSuccessStatusCode();
            if (res.IsSuccessStatusCode)
                return await httpClient.SendAsync(new HttpRequestMessage
                (HttpMethod.Put, requestUri)
                { Content = Serialize(data) });
            else return null;
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T data, string BeererAuthorizedToken = null)
        {
            var httpClient = new HttpClient();
            var Json = JsonConvert.SerializeObject(data);
            HttpContent httpContent = new StringContent(Json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            if (!string.IsNullOrEmpty(BeererAuthorizedToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BeererAuthorizedToken);
            return await httpClient.PostAsync(requestUri, httpContent);
        }

        public static HttpContent Serialize(object data) => new StringContent
            (JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        // WebAPI دریافت توکن از سرور 
        public static async Task<string> GetToken(string requestUri,int login_type, string user_name_moile, string password)
        {
            //var bodyString = @"{""LoginType"": """ + login_type + @""", ""UserName_Mobile"": """ + user_name_moile + @""", ""Password"": """ + password + @"""}";
            var bodyString = @"{LoginType: """ + login_type + @""", UserName_Mobile: """ 
                + user_name_moile + @""", Password: """ + password + @"""}";

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(requestUri
                , new StringContent(bodyString, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var responseJson = JObject.Parse(responseString);
                // توکن در متغیری به نام توکن (به انگیسی) از طرف وب اِی پی آی دریافت می شود
                return (string)responseJson["token"];
            }
            else return null;
        }

        // بررسی صحت یا فعال بودن توکن - توسط آدرس دریافت نام کاربری
        // برای این پروژه آدرس نام کاربری به صورت زیر است
        // userUri :      Stack.API_Uri_start_read + "/lei_users/0?iType=1&Other=" + Stack.sUserIndex
        public static async Task<bool> CheckTokenValidation(string userUri, string token)
        {
            //Stack.token="s";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);//"ss");//, 
            using (var _response = await httpClient.GetAsync(userUri))
            {
                if (_response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return false;// new JsonResult("-1");
                else return true;
            }
        }



    }



}
