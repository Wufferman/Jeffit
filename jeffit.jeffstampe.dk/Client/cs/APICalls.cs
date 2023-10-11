using jeffit.jeffstampe.dk.Shared.Logic;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;

namespace jeffit.jeffstampe.dk.Client.cs
{
    public static class APICalls
    {
        private static string localhostUrl = "https://localhost:7088/";
        private static string jeffstampelocalhostUlr = "https://localhost:7146/";
        private static string jeffitjeffstampeUrl = "https://jeddit.jeffstampe.dk/";
        private static string jeffstampeUrl = "https://jeffstampe.dk/";
        public static string baseUrl = jeffstampeUrl    ;
        private static HttpClient client = new HttpClient();

        public static async Task<List<ThreadPost>> GetThreadPosts()
        {
            return await client.GetFromJsonAsync<List<ThreadPost>>(baseUrl + "api/thread");
        }

        public static async Task AddCommentToThread(int threadid, Comment comment)
        {
            await client.PostAsJsonAsync(baseUrl + $"api/thread/{threadid}/comment", comment);
        }

        public static async Task AddThread(ThreadPost thread)
        {
            await client.PostAsJsonAsync(baseUrl + $"api/thread", thread);

        }

        public static async Task AddLike(ThreadPost thread)
        {
            await client.PostAsJsonAsync(baseUrl + "api/like/thread/add", thread);
        }
        public static async Task RemoveLike(ThreadPost thread)
        {
            await client.PostAsJsonAsync(baseUrl + "api/like/thread/remove", thread);
        }
        public static async Task AddLike(Comment comment)
        {
            await client.PostAsJsonAsync(baseUrl + "api/like/comment/add", comment);
        }
        public static async Task RemoveLike(Comment comment)
        {
            await client.PostAsJsonAsync(baseUrl + "api/like/comment/remove", comment);
        }
        public static async Task<User> Login(string username)
        {
            return await client.GetFromJsonAsync<User>(baseUrl + $"api/thread/login/{username}");
        }

    }
}
