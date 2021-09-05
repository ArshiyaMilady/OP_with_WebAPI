using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersProgress.ViewModels
{

    //{"LoginType":"1","UserName_Mobile":"admin","Password":"9999"}
    public class LoginModel
    {
        // LoginType = 1 : ورود با شناسه کاربری
        // LoginType = 2 : ورود با تلفن همراه
        public byte LoginType { get; set; }
        public string UserName_Mobile { get; set; }
        public string Password { get; set; }
    }

}
