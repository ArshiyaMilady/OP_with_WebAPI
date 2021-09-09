using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OP_WebApi
{
    public static class Stack
    {
        public static string Standard_Salt = "d;lkjWeoj'l;ksDfok';lsdkovjmwEfl;kwikvxc.m,/Zviejkjds;flKjoremwa;lm";
        public static string ConstantKey = "d;lkjWeoj'l;ksDfok';lsdkovjmwEfl;kwikvxc.m,/Zviejkjds;flKjoremwa;lm";
        public static string HashKey = "S@m@nt@7615546"; // 7615546 xor 4814

        public static long Company_Id = 1;


    }

    //public static class HttpClientExtensions
    //{
    //    public static async Task<string> GetAsJsonAsync_String(string requestUri)
    //    {
    //        HttpClient httpClient = new HttpClient();
    //        HttpResponseMessage res = await httpClient.GetAsync(requestUri);
    //        res.EnsureSuccessStatusCode();
    //        if (res.IsSuccessStatusCode)
    //            return await res.Content.ReadAsStringAsync();
    //        else return null;
    //    }

    //    public static async Task<T> GetT<T>(string requestUri)
    //    {
    //        HttpClient httpClient = new HttpClient();
    //        //if (httpClient.GetStringAsync(requestUri).Status == TaskStatus.RanToCompletion)
    //        {
    //            var response = await httpClient.GetStringAsync(requestUri);
    //            return JsonConvert.DeserializeObject<T>(response);
    //        }
    //    }

    //    public static async Task<HttpResponseMessage> DeleteAsJsonAsync<T>
    //        (string requestUri, T data)
    //    {
    //        HttpClient httpClient = new HttpClient();
    //        //HttpResponseMessage res = await httpClient.GetAsync(requestUri);
    //        //res.EnsureSuccessStatusCode();
    //        //if (res.IsSuccessStatusCode)
    //        var res1 = await httpClient.SendAsync(new HttpRequestMessage
    //            (HttpMethod.Delete, requestUri)
    //        { Content = Serialize(data) });
    //        if (res1.IsSuccessStatusCode) return res1;
    //        else return null;
    //    }

    //    public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T data)
    //    {
    //        HttpClient httpClient = new HttpClient();
    //        HttpResponseMessage res = await httpClient.GetAsync(requestUri);
    //        //res.EnsureSuccessStatusCode();
    //        if (res.IsSuccessStatusCode)
    //            return await httpClient.SendAsync(new HttpRequestMessage
    //                (HttpMethod.Put, requestUri)
    //            { Content = Serialize(data) });
    //        else return null;
    //    }

    //    public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T data, string BeererAuthorizedToken = null)
    //    {
    //        var httpClient = new HttpClient();
    //        var Json = JsonConvert.SerializeObject(data);
    //        HttpContent httpContent = new StringContent(Json);
    //        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
    //        if (!string.IsNullOrEmpty(BeererAuthorizedToken))
    //            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BeererAuthorizedToken);
    //        return await httpClient.PostAsync(requestUri, httpContent);
    //    }

    //    public static HttpContent Serialize(object data) => new StringContent
    //        (JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
    //}

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
            if (HasMillisecond)
                sDT = sDT + ":" + DateTime.Now.Millisecond;
            if (!between.Equals(":")) sDT = sDT.Replace(":", between);
            return sDT;
        }

        // با توجه به دو تابع تاریخ و زمان، برای زمان امروز، رشته ای با فرمت زیر را بر میگرداند
        // YYYY/MM/DD-HH:MM:SS
        public static string DateTimeNow_Shamsi(string BetweenDate = "/", string BetweenTime = ":", bool HasMillisecond = false)
        {
            string sMiladi_to_Shamsi_YYYYMMDD = Miladi_to_Shamsi_YYYYMMDD(DateTime.Now, BetweenDate)
                + "-" + NowTime_HHMMSSFFF(BetweenTime);
            if (HasMillisecond) return sMiladi_to_Shamsi_YYYYMMDD;
            else
            {
                // تعداد کاراکتر زمان بدون میلی ثانیه
                int nTimeLength = 15 + 2 * BetweenDate.Length + 2 * BetweenTime.Length;
                return sMiladi_to_Shamsi_YYYYMMDD.Substring(0, nTimeLength);
            }
        }

        public static bool CheckForInternetConnection(int timeoutMs = 10000)
        {
            try
            {
                string n = CultureInfo.InstalledUICulture.Name;
                //MessageBox.Show(n);
                if (n.Length > 1)
                {
                    string url = null;
                    switch (n.Substring(0, 2))
                    {
                        case "fa":
                            url = "https://www.aparat.com";
                            break;
                        case "zh":
                            url = "http://www.baidu.com";
                            break;
                        default:
                            url = "https://www.google.com";
                            break;
                    }
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.KeepAlive = false;
                    request.Timeout = timeoutMs;
                    using (var response = (HttpWebResponse)request.GetResponse())
                        return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }
    }





}
