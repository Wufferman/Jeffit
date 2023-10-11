using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace jeffit.jeffstampe.dk.Server.Hubs
{
    public class WebsocketHub
    {
        private static readonly ConcurrentDictionary<string, WebSocket> ConnectedClients =
                new ConcurrentDictionary<string, WebSocket>();
        public async Task HandleConnection(HttpContext context)
        {

            string clientId = context.Connection.Id;

            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                // Add the new WebSocket connection to the dictionary, if it doesn't already exist
                ConnectedClients.TryAdd(clientId, webSocket);

                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result = null;

                try
                {
                    do
                    {
                        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        // Additional logic to handle received messages can go here
                    }
                    while (!result.CloseStatus.HasValue);
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                }
                finally
                {
                    WebSocket removedSocket;
                    ConnectedClients.TryRemove(clientId, out removedSocket);

                    if (result?.CloseStatus.HasValue == true)
                    {
                        await removedSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                    }
                    else
                    {
                        await removedSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    }
                }

            }
            else
            {
                context.Response.StatusCode = 400;
            }

        }
        public static async Task TriggerRefresh()
        {
            byte[] refreshSignal = { 0x01 };

            foreach (var client in ConnectedClients.Values)
            {
                if (client.State == WebSocketState.Open)
                {
                    await client.SendAsync(refreshSignal, WebSocketMessageType.Binary, true, CancellationToken.None);
                }
            }
        }
    }
}
