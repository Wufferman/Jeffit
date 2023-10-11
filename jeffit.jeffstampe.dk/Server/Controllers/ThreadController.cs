using jeffit.jeffstampe.dk.Server.Services.Interfaces;
using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.AspNetCore.Mvc;

namespace jeffit.jeffstampe.dk.Server.Controllers
{
    [ApiController]
    [Route("api/thread")]
    public class ThreadController : ControllerBase
    {
        IThreadService threadService;
        public ThreadController(IThreadService service)
        {
            threadService = service;
            threadService.SeedData();
        }

        [HttpGet]
        public async Task<List<ThreadPost>> GetThreadPosts()
        {
            return await threadService.GetThreads();
        }
        [HttpPost("{id}/comment")]
        public async Task PostCommentToThread(int id, Comment comment)
        {
            await threadService.CreateComment(comment, id);
        }
        [HttpPost]
        public async Task CreateThread(ThreadPost thread)
        {
            await threadService.CreateThread(thread);
        }
        [HttpGet("login/{username}")]
        public async Task<User> GetUser(string username)
        {
            return await threadService.GetUser(username);
        }

    }
}
