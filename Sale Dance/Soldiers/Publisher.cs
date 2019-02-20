using System;
using System.Collections.Generic;
using System.Linq;
using Sale_Dance.Models;

namespace Sale_Dance.Soldiers
{
    public class Publisher
    {
        private const int MAX_PUBLISHING = 3;
        public static Tuple<bool, string> ValidToPost(List<Post> posts, Post post)
        {
            if (post.IsPublished)
            {
                return Tuple.Create(false, "פוסט זה כבר פורסם");
                
            }
            var count = posts.Where(p => p.IsPublished == true).Count();
            if(count >= MAX_PUBLISHING)
            {
                return Tuple.Create(false, "אינך יכול לפרסם יותר מ-3 פוסטים. הסר פוסט אחר כדי לאפשר פרסום.");
               
            }
            return Tuple.Create(true,"הפוסט פורסם בהצלחה!");
        }

       

    }

   
}
