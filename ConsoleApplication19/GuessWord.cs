using ConsoleApplication19.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication19
{
    class GuessWord : BaseGame
    {
        // تعداد شانس های کاربر برای حدس کلمه
        private static int Chances = 3;

        // کلمه در نظر گرفته شده
        private Word TargetWord;

        // زمان بازی به صورت پیش فرض یک دقیقه در نظر گرفته شده
        public GameTimer GameTimer = new GameTimer { Minutes = 1, Seconds = 0 };

        // لیست کلمه هایی که از فایل دریافت می شود
        public List<Word> Words { get; set; }

        // در ابتدای ساخنه شدن اطلاعات کلمه ها دریافت می شود
        public GuessWord(User user) : base(user)
        {
            // آدرس فایل
            // /bin/debug/Words
            ReadFile(@"Words");
        }

        // توضیحات بازی
        public static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Guess Word Help:\n" +
                "\tIn this game you guess the scrambled words in the set time! you have 3 chances for each word\n" +
                "Modes:\n" +
                "\tEasy: Characters and a hint are shown to you\n" +
                "\tHard: Only characters are shown!\n" +
                "Score:\n" +
                "\tIf you guess a word in the first try you get 10 points, in the second try 6 points and in the third try 2 points" +
                "\tIn the hard mode points are doubled!"
                );
        }

        // نمایش اطلاعات کاربر به همراه امتیاز
        public override void PrintInfo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            base.PrintInfo();
            Console.WriteLine($"Score: { UserScore}");
        }

        // کلمه ها از فایل خوانده می شوند و در لیست کلمه ها قرار داده می شوند
        public override void ReadFile(string path)
        {
            using (StreamReader x = new StreamReader(path))
            {
                string data = x.ReadToEnd();
                Words = JsonConvert.DeserializeObject<List<Word>>(data);
            }
        }

        // ذخیره امتیاز کاربران
        public override void SaveFile(string path)
        {
            string json = JsonConvert.SerializeObject(Database.GuessWordScores.ToArray());
            File.WriteAllText(path, json);
        }

        // بازی توسط این تابه شورع می شود
        public void Start(bool IsEasyMode)
        {
            // امتیاز کاربر صفر می شود
            UserScore = 0;

            // با توجه به سطح بازی کلمه دریافت می شود
            TargetWord = GetWord(IsEasyMode);

            // شانس حدس کلمه 3 در نظر گرفته می شود
            // برای بازی دوباره نیاز است که این متغیر ها اصلاح شوند 
            Chances = 3;
            GameTimer.GotTime = true;

            //تایمر شروع می شود
            GameTimer.Start();

            // تا زمانی که کاربر زمان دارد می تواند کلمه حدس بزند
            while (GameTimer.GotTime)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("chars:");

                // حروف کلمات به هم ریخته می شود و نمایش داده می شود
                Word.Shuffle(TargetWord.ActualWord).ForEach(c => Console.Write($"{c} "));
                Console.WriteLine();

                // اگر بازی آسان باشد توضیحات نیز نمایش داده میشود
                if (IsEasyMode)
                {
                    Console.WriteLine("Help:");
                    Console.WriteLine(TargetWord.Definition);
                }
                Console.ForegroundColor = ConsoleColor.Cyan;

                //  کاربر حدس میزند
                string UserGuess = Console.ReadLine();
                Console.WriteLine();

                // چک کردن اینکه کاربر درست حدس زده
                if (Guess(UserGuess))
                {
                    // امتیازات سطح آسان
                    if (IsEasyMode)
                    {
                        switch (Chances)
                        {
                            case 1:
                                UserScore += 2;
                                break;
                            case 2:
                                UserScore += 6;
                                break;
                            case 3:
                                UserScore += 10;
                                break;
                            default:
                                break;
                        }
                    }
                    // امتیازات سطح سخت
                    else
                    {
                        switch (Chances)
                        {
                            case 1:
                                UserScore += 2 * 2;
                                break;
                            case 2:
                                UserScore += 6 * 2;
                                break;
                            case 3:
                                UserScore += 10 * 2;
                                break;
                            default:
                                break;
                        }
                    }
                    // کلمه جدید و شانس های کلمه دریافت می شود
                    TargetWord = GetWord(IsEasyMode);
                    Chances = 3;
                }

                // اگر اشتباه گفته باشد شانس های باقی مانده نمایش داده می شود و اگر شانسی باقی نمانده باشد کلمه صحیح نمایش داده می شود و کلمه جدید دریافت می شود
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Remaining Chances : {--Chances}");
                    if (Chances == 0)
                    {
                        Console.WriteLine($"The word was: {TargetWord.ActualWord}");
                        TargetWord = GetWord(IsEasyMode);
                        Chances = 3;
                    }

                }
            }

            // نمایش اطلاعات و امتیاز به کاربر
            PrintInfo();

            // اگر کاربر امتیاز بالای صفر ثبت کرده باشد در لیست امتیازات دیتابیس اضافه شده و در فایل ذخیره می شود
            if (UserScore > 0)
            {
                Database.GuessWordScores.Add(new Score { User = User, GameScore = UserScore });
                SaveFile("Guess Word Scores");
            }
        }

        // چک کردن کلمه ای که کاربر حدس زده
        public bool Guess(string UserWord)
        {
            if (TargetWord.ActualWord.ToLower() == UserWord.ToLower())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct!");
                Console.WriteLine($"The word was: {TargetWord.ActualWord}");
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect!");
                return false;
            }
        }

        // دریافت کلمه با  توجه به سطح بازی انتخاب شده
        public Word GetWord(bool IsEasyMode)
        {
            Random rng = new Random();
            if (IsEasyMode)
            {
                int Index = rng.Next(Words.Count);
                return Words[Index];
            }
            else
            {
                var HardWords = Words.Where(w => w.ActualWord.Count() >= 6).ToList();
                int Index = rng.Next(HardWords.Count());
                return HardWords[Index];

            }

        }
    }
}
