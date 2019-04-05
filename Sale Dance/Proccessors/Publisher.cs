using System;
using System.Collections.Generic;
using System.Linq;
using Sale_Dance.Models;

namespace Sale_Dance.Soldiers
{
    public class Publisher
    {
        private const int MAX_PUBLISHING = 3;
        public static Tuple<bool, string> ValidToPost(List<Post> posts, Post post, Business b)
        {
            if (post.IsPublished)
            {
                return Tuple.Create(false, "פוסט זה כבר פורסם");
                
            }
            var count = posts.Count(p => p.IsPublished == true);
            if(count >= MAX_PUBLISHING)
            {
                return Tuple.Create(false, "אינך יכול לפרסם יותר מ-3 פוסטים. הסר פוסט אחר כדי לאפשר פרסום.");
               
            }

            if (BusinessHasEnoughData(b))
            {
                return Tuple.Create(false, "יש לוודא שכל הפרטים של העסק ממולאים ונכונים. אנא מלא את כל פרטי העסק לפני פרסום הפוסט");

            }
            return Tuple.Create(true,"הפוסט פורסם בהצלחה!");
        }

        private static bool BusinessHasEnoughData(Business b)
        {
            return string.IsNullOrWhiteSpace(b.Name) && string.IsNullOrWhiteSpace(b.Address) &&
                   string.IsNullOrWhiteSpace(b.About)
                   && string.IsNullOrWhiteSpace(b.BusinessPhoneContact);
        }

    }

   
}
