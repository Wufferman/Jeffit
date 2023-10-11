using jeffit.jeffstampe.dk.Client.cs;
using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.AspNetCore.Components;
using System.Net.WebSockets;

namespace jeffit.jeffstampe.dk.Client.Pages
{
    public partial class Index
    {
        private ClientWebSocket webSocket = new();
        private string baseUrl = APICalls.baseUrl;
        private string username = "";
        private User loginUser;
        public List<ThreadPost> threads;
        private string GetWSUrl
        {
            get
            {
                return baseUrl.Replace("https://", "");
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await Refresh();
            await base.OnInitializedAsync();
            await ConnectWebSocket();
        }

        private void UpdateUsername(ChangeEventArgs e)
        {
            username = e.Value.ToString();
        }

        private async Task Login()
        {
            loginUser = await APICalls.Login(username);
            await Refresh();
        }

        private async Task ConnectWebSocket()
        {
            webSocket = new();
            var uri = new Uri($"wss://{GetWSUrl}jeffitws");
            await webSocket.ConnectAsync(uri, CancellationToken.None);
            ArraySegment<byte> buffer = new byte[1] { 0x1 };
            WebSocketReceiveResult result;

            while (webSocket.State == WebSocketState.Open)
            {
                result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                await Refresh();
            }
        }

        private async Task Refresh()
        {
            threads = await APICalls.GetThreadPosts();
            await InvokeAsync(() => StateHasChanged());
        }
    }
}
