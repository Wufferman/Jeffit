using jeffit.jeffstampe.dk.Client.cs;
using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Reflection;

namespace jeffit.jeffstampe.dk.Client.Pages
{
    public partial class Index
    {
        #region Initialization
        private ClientWebSocket webSocket = new();
        private string baseUrl = APICalls.baseUrl;
        private string username = "";
        private string subjeffit = string.Empty;
        private string savedSubJeffit = string.Empty;
        private User loginUser;
        public List<ThreadPost> threads;
        private bool locked = false;
        private bool descending = false;
        private PropertyInfo propInfo1 = typeof(ThreadPost).GetProperty("CreationTime"); // For first level properties like ThreadPost.Creator
        private PropertyInfo propInfo2 = null; // For nested properties like Creator.Name
        private string specialProp = null;  // Reset special property
        private string lastSortedProp = "CreationTime";  // To hold the last sorted property name
        private string GetWSUrl
        {
            get
            {
                return baseUrl.Replace("https://", "");
            }
        }
        #endregion
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

        private void UpdateSubJeffit(ChangeEventArgs e)
        {
            subjeffit = e.Value.ToString();
        }
        private async Task Login()
        {
            loginUser = await APICalls.Login(username);
            await Refresh();
        }

        private async Task SubJeffitInput()
        {
            await Refresh();
            if(subjeffit != "" && subjeffit != string.Empty && subjeffit != "all")
                savedSubJeffit = subjeffit;
            else
                savedSubJeffit = string.Empty;
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
            if (APICalls.baseUrl == "https://jeffstampe.dk/")
            {
                locked = await new HttpClient().GetFromJsonAsync<bool>($"{APICalls.baseUrl}api/thread/locked");
            }
            threads = await APICalls.GetThreadPosts();
            if (specialProp == "CommentCount")  // Sorting by comment count
            {
                if (descending)
                {
                    threads = threads.OrderByDescending(t => t.Comments.Count).ToList();
                }
                else
                {
                    threads = threads.OrderBy(t => t.Comments.Count).ToList();
                }
            }
            else if (propInfo2 != null)  // For nested properties
            {
                if (descending)
                {
                    threads = threads.OrderByDescending(t => propInfo2.GetValue(propInfo1.GetValue(t))).ToList();
                }
                else
                {
                    threads = threads.OrderBy(t => propInfo2.GetValue(propInfo1.GetValue(t))).ToList();
                }
            }
            else  // For first level properties
            {
                if (descending)
                {
                    threads = threads.OrderByDescending(t => propInfo1.GetValue(t)).ToList();
                }
                else
                {
                    threads = threads.OrderBy(t => propInfo1.GetValue(t)).ToList();
                }
            }

            if (subjeffit != "all" && subjeffit != "" && subjeffit != string.Empty) threads = threads.Where(x => x.SubJeffit == subjeffit).ToList();

            await InvokeAsync(() => StateHasChanged());

        }

        private async Task ChangePropInfo(string newProp)
        {
            if (newProp == lastSortedProp)
            {
                // Toggle the sorting direction
                descending = !descending;
            }
            else
            {
                // Reset the sorting direction to default (true) for new property
                descending = true;
            }

            lastSortedProp = newProp;
            specialProp = null;
            if (newProp == "Creator")
            {
                propInfo1 = typeof(ThreadPost).GetProperty("Creator");
                propInfo2 = typeof(User).GetProperty("Name");
            }
            else if (newProp == "CommentCount")
            {
                specialProp = "CommentCount";  // Set special property
            }
            else
            {
                propInfo1 = typeof(ThreadPost).GetProperty(newProp);
                propInfo2 = null;
            }
            await Refresh();
        }

        private string GetArrow(string input)
        {
            if(lastSortedProp == input)
            {
                if (descending)
                {
                    return "▼";
                }
                else
                {
                    return "▲";
                }
            }
            return "";
        }
    }
}
