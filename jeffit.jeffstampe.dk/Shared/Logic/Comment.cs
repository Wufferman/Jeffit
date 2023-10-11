using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeffit.jeffstampe.dk.Shared.Logic
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public User Creator { get; set; }
        public DateTime CreationTime { get; set; }

        public Comment() { }  // Parameterless constructor for EF Core

        public Comment(string text, User creator)
        {
            Text = text;
            Creator = creator;
            CreationTime = DateTime.Now;
        }
        public void AddLike() => Likes++;
        public void RemoveLike() => Likes--;
    }
}
