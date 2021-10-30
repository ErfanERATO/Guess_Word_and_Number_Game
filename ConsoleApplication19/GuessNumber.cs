using ConsoleApplication19.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication19
{
    class GuessNumber : BaseGame
    {
        //عدد در نظر گرفته شده
        private int TargetNumber;
        //  شانس های کاربر
        private int Chances;
        //public GuessNumber() {
        //    getRandomNumber();
        //    chances = 5;
        //}
        // به صورت پیش فرض 5 شانس برای کاربر در نظر گرفته شده
        // در صورتی که کاربر تعداد شانس اعداد مثبت انتخاب نکند 5 در نظر گرفته می شود
        public GuessNumber(User user, int chances = 5) : base(user)
        {
            Chances = chances > 0 ? chances : 5;
        }

        // دریافت یک عدد رندوم بین 0 تا 1000
        private int GetRandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1000);
        }

        // چک کردن عدد حدس زده شده توسط کاربر با عدد در نظر گرفته شده
        public bool Check(int userChoice)
        {
            if (userChoice == TargetNumber)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You won!");
                UserScore += (Chances - 1) * 10;
                User.Coins += (Chances - 1) * 100;
                return true;
            }
            else
            {
                if (TargetNumber > userChoice)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Target is Bigger");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Target is Smaller");
                }
                Chances--;
            }
            return false;
        }

        // در صورتی که شانس های کاربر تمام شده باشد بازی پایان می یابد و عدد در نظر گرفته شده به کاربر نمایش داده می شود
        public bool CheckGameOver()
        {
            if (Chances > 0)
            {
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" Game Over: target Number was {TargetNumber}");
                return false;
            }
        }

        // بازی توسط این تابع شروع می شود
        public void Start()
        {
            //در ابتدای بازی امتیاز کاربر صفر می شود
            UserScore = 0;

            // عدد تصادفی دریافت می شود
            TargetNumber = GetRandomNumber();
            int UserGuess = 0;

            // تا زمانی که کاربر عدد صحیح را پیدا نکرده باشد و شانس باقب مانده داشته باشد می تواند عدد حدس بزند
            do
            {
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Chances Left: {Chances}");

                Console.WriteLine("please enter your guess:");
                int.TryParse(Console.ReadLine(), out UserGuess);

                // تا زمانی که کاربر بین بازه مشخص شده عدد وارد نکند از او درخواست عدد می شود
                while (UserGuess <=0 || UserGuess >=1000)
                {
                    Console.WriteLine("Enter your guess:");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    int.TryParse(Console.ReadLine(), out UserGuess);
                    Console.WriteLine(UserGuess);
                    //Console.WriteLine();
                }

            } while (!Check(UserGuess) && CheckGameOver());

            // پس از پایان دور اطلاعات کاربر و امتیاز نمایش داده می شود
            PrintInfo();

            // در صورتی که امتیاز کاربر بالای صفر باشد امتیاز در دیتابیس ذخیره می شود و در فایل ثبت می شود
            if (UserScore > 0)
            {
                Database.GuessNumberScores.Add(new Score { User = User, GameScore = UserScore });
                SaveFile("Guess Number Scores");
            }
        }

        // نمایش اطلاعات کاربر به همراه امتیاز بازی
        public override void PrintInfo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            base.PrintInfo();
            Console.WriteLine($"Score: {UserScore}");
        }

        // توضیحات بازی
        public static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Guess Number Help:\n" +
                "\tIn this game a random number from 0 to 1000 is chosen and you have to guess what that is!\n" +
                "\tFirst you enter number of chances you need (default is 5)\n" +
                "\tThen simply start guessing!");
        }

        // اطلاعات لیست امتیازات حدس عدد ذخیره می شود
        public override void SaveFile(string path)
        {
            string json = JsonConvert.SerializeObject(Database.GuessNumberScores.ToArray());
            File.WriteAllText(path, json);
        }

        // این بازی احتیاج به خواندن اطلاعاتی ندارد
        public override void ReadFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
