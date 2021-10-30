using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication19.Models
{
    // برای تعریف کلمه ها 
    class Word
    {
        // کلمه اصلی
        public string ActualWord { get; set; }   

        // توضیح کلمه
        public string Definition { get; set; }

        // وارد کردن این اطلاعات برای ساختن کلمه اجباری می باشد
        public Word(string actualWord, string definition)
        {
            ActualWord = actualWord;
            Definition = definition;            
        }

        // تابع که به صورت تصادفی حروف درون یک کلمه را به هم رخته و به صورت یک آرایه از کارکتر ها باز می گرداند
        // جا به جایی به این صورت است که یک حرف به صورت تصادفی انتخاب می شود و سپس با حروف ابتدایی جا به جا میشود تا جایگزینی آخرین حرف 
        public static List<char> Shuffle(string word)
        {
            int Count = word.Count();
            List<char> CharList = new List<char>();
            word.ToList().ForEach(x => CharList.Add(x));
            Random rng = new Random();
            for (int i = 0; i < Count; i++)
            {
                int Index = rng.Next(Count);
                char value = CharList[Index];
                CharList[Index] = CharList[i];
                CharList[i] = value;
            }
            return CharList;
        }
    }
}
