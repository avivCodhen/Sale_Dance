using System;
using Sale_Dance.Models;

namespace Sale_Dance.Soldiers
{
    public class Bumper
    {
        private const int MIN_BUMP = 12;
        public static Tuple<bool, string> ValidToBump(PublishedPost post)
        {
            var now = DateTime.Now;
            var span = now.Subtract(post.PublishTime);
            if (span.Hours >= MIN_BUMP)
            {
                return Tuple.Create(true, "הפוסט הוקפץ בהצלחה");
            }
            return Tuple.Create(false, "הקפצת פוסט יכולה להתבצע כל 12 שעות");
        }
    }
}
