using jeffit.jeffstampe.dk.Client.cs;
using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace jeffit.jeffstampe.dk.Client.Pages
{
    public partial class CreateThreadElement
    {

        [Parameter]
        public User LoginUser { get; set; }


        ThreadPost newThread;

        protected override void OnInitialized()
        {
            newThread = new ThreadPost("", LoginUser)
            {
                Comments = new List<Comment>()
            };
            base.OnInitialized();
        }

        public async Task AddThread()
        {
            newThread.Creator = LoginUser;
            newThread.CreationTime = DateTime.Now;
            if (newThread.Name != "" && newThread.Name != string.Empty)
            {
                await APICalls.AddThread(newThread);
                newThread = new ThreadPost("", LoginUser)
                {
                    Comments = new List<Comment>()
                };
            }
        }
    }
}
