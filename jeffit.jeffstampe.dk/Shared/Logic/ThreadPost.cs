using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeffit.jeffstampe.dk.Shared.Logic
{
    public class ThreadPost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Likes { get; set; }
        public string SubJeffit { get; set; }
        public User Creator { get; set; }
        public DateTime CreationTime { get; set; }

        public List<Comment> Comments { get; set; }
        public ThreadPost()
        {
        }
            public ThreadPost(string Name, User creator)
        {
            this.Name = Name;
            this.Creator = creator;
            CreationTime = DateTime.Now;
        }
        public void AddLike() => Likes++;
        public void RemoveLike() => Likes--;

        public void CreateComment(Comment comment)
        {
            Comments.Add(comment);
        }
    }
}
