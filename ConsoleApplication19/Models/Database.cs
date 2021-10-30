using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace ConsoleApplication19.Models
{
    // امتیاز های ذخیره شده در ابتدای برنامه در این قسمت ذخیره می شوند
    static class Database
    {
        // امتیاز بازی حدس عدد
        public static List<Score> GuessNumberScores = new List<Score>();

        //امتیاز بازی حدس کلمه
        public static List<Score> GuessWordScores = new List<Score>();

        //نمایش امتیاز بازی حدس عدد بر اساس امتیاز
        public static void ViewGuessNumberScores()
        {
            // اطلاعات امتیاز های بالا تر از امتیاز وارد شده نمایش داده می شود
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter target score:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            int.TryParse(Console.ReadLine(), out int TargetScore);

            // امتیاز های بالا تر از امتیاز انتخاب شده به صورت نزولی نشان داده  می شوند
            var HighScores = GuessNumberScores.Where(s => s.GameScore > TargetScore).OrderByDescending(x=>x.GameScore).ToList();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{"UserName",-10} \t {"Age",-3} \t {"Game Score",-10}");
            Console.ForegroundColor = ConsoleColor.Green;
            HighScores.ForEach(x => Console.WriteLine($"{x.User.UserName,-10} \t {x.User.Age,-3} \t {x.GameScore,-10}"));
        }

        //نمایش اطلاعات بازی حدس عدد برا اساس اسم
        public static void ViewGuessNumberScoresByName(string Name)
        {
            // اطلاعات امتیاز های نام کاربر وارد شده نمایش داده می شود
            // کلمه وارد شده جزیی از نام کاربر باشد            
            // امتیاز های کاربران انتخاب شده به صورت نزولی نشان داده  می شوند
            var HighScores = GuessNumberScores.Where(s => s.User.UserName.ToLower().Contains(Name.ToLower())).OrderByDescending(x => x.GameScore).ToList();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{"UserName",-10} \t {"Age",-3} \t {"Game Score",-10}");
            Console.ForegroundColor = ConsoleColor.Green;
            HighScores.ForEach(x => Console.WriteLine($"{x.User.UserName,-10} \t {x.User.Age,-3} \t {x.GameScore,-10}"));
        }

        //نمایش امتیاز بازی حدس کلمه بر اساس امتیاز
        public static void ViewGuessWordScores()
        {
            // اطلاعات امتیاز های بالا تر از امتیاز وارد شده نمایش داده می شود
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter target score:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            int.TryParse(Console.ReadLine(), out int TargetScore);

            // امتیاز های بالا تر از امتیاز انتخاب شده به صورت نزولی نشان داده  می شوند
            var HighScores = GuessWordScores.Where(s => s.GameScore > TargetScore).OrderByDescending(x => x.GameScore).ToList();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{"UserName",-10} \t {"Age",-3} \t {"Game Score",-10}");
            Console.ForegroundColor = ConsoleColor.Green;
            HighScores.ForEach(x => Console.WriteLine($"{x.User.UserName,-10} \t {x.User.Age,-3} \t {x.GameScore,-10}"));

        }

        //نمایش امتیاز کاربران بازی حدس کلمه برا اساس نام
        public static void ViewGuessWordScoresByName(string Name)
        {
            // اطلاعات امتیاز های نام کاربر وارد شده نمایش داده می شود
            // کلمه وارد شده جزیی از نام کاربر باشد            
            // امتیاز های کاربران انتخاب شده به صورت نزولی نشان داده  می شوند
            var HighScores = GuessWordScores.Where(s => s.User.UserName.ToLower().Contains(Name.ToLower())).OrderByDescending(x => x.GameScore).ToList();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{"UserName",-10} \t {"Age",-3} \t {"Game Score",-10}");
            Console.ForegroundColor = ConsoleColor.Green;
            HighScores.ForEach(x => Console.WriteLine($"{x.User.UserName,-10} \t {x.User.Age,-3} \t {x.GameScore,-10}"));
        }
    }
}
