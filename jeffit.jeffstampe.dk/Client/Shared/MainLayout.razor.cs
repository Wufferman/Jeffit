using jeffit.jeffstampe.dk.Client.cs;
using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.JSInterop;
using System.Net.WebSockets;

namespace jeffit.jeffstampe.dk.Client.Shared
{
    public partial class MainLayout
    {
        protected override async Task OnInitializedAsync()
        {
            await JSRuntime.InvokeVoidAsync("blazorAppLoaded");
            await base.OnInitializedAsync();
        }
    }
}
