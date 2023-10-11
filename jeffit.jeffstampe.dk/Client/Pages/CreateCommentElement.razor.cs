using jeffit.jeffstampe.dk.Client.cs;
using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.AspNetCore.Components;

namespace jeffit.jeffstampe.dk.Client.Pages
{
    public partial class CreateCommentElement
    {
        [Parameter]
        public ThreadPost Thread { get; set; }

        [Parameter]
        public User LoginUser { get; set; }

        Comment newcomment;

        protected override void OnInitialized()
        {
            newcomment = new Comment("", LoginUser);
            base.OnInitialized();
        }

        public async Task AddCommentToThread()
        {
            newcomment.Creator = LoginUser;
            newcomment.CreationTime = DateTime.Now;
            if (newcomment.Text != "" && newcomment.Text != string.Empty)
            {
                Thread.CreateComment(newcomment);
                await APICalls.AddCommentToThread(Thread.Id, newcomment);
                newcomment = new Comment("", LoginUser);
            }
        }
    }
}
