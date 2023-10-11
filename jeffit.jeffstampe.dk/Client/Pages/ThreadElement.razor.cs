using jeffit.jeffstampe.dk.Client.cs;
using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace jeffit.jeffstampe.dk.Client.Pages
{
    public partial class ThreadElement
    {
        [Parameter]
        public ThreadPost Thread { get; set; }

        [Parameter]
        public User LoginUser { get; set; }
        private bool CommentVisibilityBool { get; set; } = true;

        public void ChangeCommentVisibilityBool()
        {
            CommentVisibilityBool = !CommentVisibilityBool;
        }

        public async Task AddLike()
        {
            ChangeCommentVisibilityBool();
            Thread.AddLike();
            await APICalls.AddLike(Thread);
        }
        public async Task RemoveLike()
        {
            ChangeCommentVisibilityBool();
            Thread.RemoveLike();
            await APICalls.RemoveLike(Thread);
        }
    }
}
