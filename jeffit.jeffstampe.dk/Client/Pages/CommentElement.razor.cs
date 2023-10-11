using jeffit.jeffstampe.dk.Client.cs;
using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.AspNetCore.Components;

namespace jeffit.jeffstampe.dk.Client.Pages
{
    public partial class CommentElement
    {
        [Parameter]
        public Comment Comment { get; set; }

        public async Task AddLike()
        {
            Comment.AddLike();
            await APICalls.AddLike(Comment);
        }
        public async Task RemoveLike()
        {
            Comment.RemoveLike();
            await APICalls.RemoveLike(Comment);
        }
    }
}
