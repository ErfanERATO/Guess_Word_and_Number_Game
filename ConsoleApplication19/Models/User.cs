using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication19.Models
{
    //کلاس برای دریافت اطلاعات کاربران
    class User
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public int Coins { get; set; }
        // ورود اطلاعات کاربران
        // اگر نام وارد نشود به صورت مهمان وارد می شود
          public User(string username = "", int age = 0, int coins =100)
          {
              UserName = username == "" ? "Guest" : username;
              Age = age;
              Coins = coins;
          }
    }
}
