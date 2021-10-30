using ConsoleApplication19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication19
{
    abstract class BaseGame
    {
        
        protected User User = new User();
        // امتیاز کاربر
        protected int UserScore =0;
        // این اطلاعات در کلاس کابر دریافت می شود 
        //protected int totalCoins;
        //protected string userName;

        //public BaseGame() {
        //    totalCoins = 100;
        //    totalScore = 0;
        //    userName = "geust";
        //}

        public BaseGame( User user)
        {
            User = user;
            
            //totalScore = score;            
            //totalCoins = coin;
            //if (uname != "")
            //    userName = uname;
            //else
            //    userName = "geust";
        }
        // اطلاعات کاربر در این قسمت نمایش داده می شود و چون امتیاز هر دور بازی جداگانه محاسبه می شود
        // نمایش امتیازات به عهده کلاس ارث برنده تابع است
        public virtual void PrintInfo() {
            Console.WriteLine("\nUser Information:");
            Console.WriteLine($"User Name:   { User.UserName} \tCoins:  {User.Coins}");
        }       

        public abstract void SaveFile(string path);

        public abstract void ReadFile(string path);

    }
}
