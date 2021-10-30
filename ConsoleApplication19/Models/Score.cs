using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication19.Models
{
    // کلاس برای امتیاز کاربران که علاوه بر امتیاز، اطلاعات کاربران را نیز دارد
    class Score
    {
        public User User { get; set; }
        public int GameScore { get; set; }        
    }
}
