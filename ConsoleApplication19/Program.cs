using ConsoleApplication19.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication19
{
    class Program
    {
        // در این تابع اطلاعات امتیازات از فایل ها دریافت می شود و در دیتابیس ذخیره می شود
        public static List<Score> GetScores(string path)
        {
            List<Score> scores = new List<Score>();
            try
            {
                using (StreamReader x = new StreamReader(path))
                {
                    string data = x.ReadToEnd();
                    scores = JsonConvert.DeserializeObject<List<Score>>(data);
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error in Reading Scores Path: {path}");
            }
            return scores;
        }
        static void Main(string[] args)
        {
            // دریافت امتیازات بازی ها
            Database.GuessNumberScores = GetScores("Guess Number Scores");
            Database.GuessWordScores = GetScores("Guess Word Scores");
            // برای نمایش منو
            while (true)
            {
                // دریافت اطلاعات کاربر
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Username:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string Name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Age:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                int.TryParse(Console.ReadLine(), out int Age);
                User User = new User(Name, Age);
                string MenuOption = default;
                // کاربر وارد منو اصلی می شود
                while (MenuOption != "3")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    // انتخاب بازی
                    // در صورت انتخاب 3 به حلقه قبلی باز می گردد تا کاربر جدید وارد شود
                    Console.WriteLine("1-Guess Number \t 2-Guess Word \t 3-New User \t 4-Exit \t 5-devloperInformation");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    MenuOption = Console.ReadLine();
                    Console.Clear();
                    switch (MenuOption)
                    {
                        // منوی حدس عدد
                        case "1":
                            while (MenuOption != "5")
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("1-Start \t 2-View High Scores \t 3-Search for Username \t 4-Help \t 5-Back to Main Menu");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                MenuOption = Console.ReadLine();
                                switch (MenuOption)
                                {
                                    case "1":
                                        {
                                            // بازی شروع می شود و تا زمانی که کاربر بخواهد تکرار می شود
                                            string PlayAgain = default;
                                            do
                                            {
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("Number of chances:");
                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                int.TryParse(Console.ReadLine(), out int UserChances);
                                                GuessNumber guessNumber = new GuessNumber(User, chances: UserChances);
                                                guessNumber.Start();
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("Play Again?(y/n)");
                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                PlayAgain = Console.ReadLine();
                                                Console.Clear();

                                            } while (PlayAgain.ToLower() == "y");
                                        }
                                        break;
                                    case "2":
                                        {
                                            // نمایش امتیازات بالاتر از امتیاز انتخاب شده
                                            Database.ViewGuessNumberScores();
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        break;
                                    case "3":
                                        {
                                            // نمایش امتیازات بر اساس اسم کاربر
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Enter Username:");
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            string userName = Console.ReadLine();
                                            Database.ViewGuessNumberScoresByName(userName);
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        break;
                                    case "4":
                                        // نمایش توضیحات بازی
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        GuessNumber.PrintHelp();
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    case "5":
                                        // خروج از منوی بازی حدس عدد
                                        break;
                                    default:
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid Input");
                                        break;
                                }
                            }
                            break;
                            //منوی بازی حدس کلمه
                        case "2":
                            GuessWord guessWord = new GuessWord(User);
                            while (MenuOption != "6")
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("1-Easy Mode \t 2-Hard Mode \t 3-View High Scores \t 4-Search for Username \t 5-Help \t 6-Back to Main Menu");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                MenuOption = Console.ReadLine();
                                Console.Clear();
                                switch (MenuOption)
                                {
                                    case "1":
                                        {
                                            // بازی شروع می شود و تا زمانی که کاربر بخواهد تکرار می شود
                                            string PlayAgain = default;
                                            
                                            do
                                            {
                                                // سطح آسان
                                                guessWord.Start(true);
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("Play Again?(y/n)");
                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                PlayAgain = Console.ReadLine();
                                                Console.Clear();

                                            } while (PlayAgain.ToLower() == "y");
                                        }
                                        break;
                                    case "2":
                                        {
                                            // بازی شروع می شود و تا زمانی که کاربر بخواهد تکرار می شود
                                            string PlayAgain = default;
                                            do
                                            {
                                                // سطح سخت
                                                guessWord.Start(false);
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("Play Again?(y/n)");
                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                PlayAgain = Console.ReadLine();
                                                Console.Clear();
                                            } while (PlayAgain.ToLower() == "y");
                                        }
                                        break;
                                    case "3":
                                        {
                                            // نمایش امتیازات بازی حدس کلمه
                                            Database.ViewGuessWordScores();
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        break;
                                    case "4":
                                        {
                                            // نمایش امتیازات بازی حدس کلمه بر اسا نام کاربر
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Enter Username:");
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            string userName = Console.ReadLine();
                                            Database.ViewGuessWordScoresByName(userName);
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        break;
                                    case "5":
                                        // نمایش اطلاعات بازی
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        GuessWord.PrintHelp();
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    case "6":
                                        // خروج از منوی بازی حدس کلمه
                                        break;
                                    default:
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid Input");
                                        break;
                                }
                            }
                            Console.ReadKey();
                            break;
                        case "3":
                            // ورود کاربر جدید
                            Name = default;
                            Age = default;
                            User = default;
                            break;
                        case "4":
                            // خروج از برنامه
                            Environment.Exit(0);
                            break;

                        case "5":
                            developerInformation();
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Input");
                            break;
                    }

                    void developerInformation()
                    {
                        Console.Clear();
                        Console.WriteLine("       :::::               Name : Erfan Gharche Beydokhti                  :::::");
                        Console.WriteLine("   ....:::::           Job : Co_developer and Graphic designer             :::::....");
                        Console.WriteLine(".......:::::   Specialized programming language: Dart Flutter frame work   :::::.......");
                        Console.WriteLine("\n [1].Back to main menu");
                        if (Console.ReadKey().KeyChar == '1') ;
                    }
                }
            }
        }
    }
}
