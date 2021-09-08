using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OP_WebApi
{
    public static class Stack
    {
        public static string ConstantKey = "d;lkjWeoj'l;ksDfok';lsdkovjmwEfl;kwikvxc.m,/Zviejkjds;flKjoremwa;lm";
        public static string HashKey = "S@m@nt@7615546"; // 7615546 xor 4814

        public static long Company_Id = -1;


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






}
