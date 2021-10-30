using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;


namespace ConsoleApplication19.Models
{
    // نایمر برای بازی حدس کلمه
    class GameTimer
    {
        //دقیقه و ثانیه در نظر گرفته شده برای بازی
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        //زمان بازی برای شمارش معکوس به این متغیر داده می شود
        private int TimeLeft { get; set; }
        // متغیر برای پایان زمان
        private bool _gotTime = true;

        public bool GotTime
        {
            get { return _gotTime; }
            set { _gotTime = value; }
        }
        // در این تابع هر ثانیه یک واحد از شمارنده معکوس کم می شود تا به صفر برسد سپس متغیر با یک پیام تغییر می کند
        public void Start()
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            TimeLeft = Seconds + (Minutes * 60);
            timer.Elapsed += delegate
            {
                if (TimeLeft <=0)
                {
                    Console.WriteLine("Time's Up!");
                    GotTime = false;
                    timer.Stop();
                }
                else
                {
                    TimeLeft--;
                }
            };
            timer.Start();
        }
    }
}
