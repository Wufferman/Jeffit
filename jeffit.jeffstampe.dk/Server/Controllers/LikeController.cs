using jeffit.jeffstampe.dk.Server.Services.Interfaces;
using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.AspNetCore.Mvc;

namespace jeffit.jeffstampe.dk.Server.Controllers
{
    [ApiController]
    [Route("api/like")]
    public class LikeController : ControllerBase
    {
        IThreadService threadService;
        public LikeController(IThreadService service)
        {
            threadService = service;
        }

        [HttpPost("thread/add")]
        public async Task AddLike(ThreadPost thread)
        {
            await threadService.AddLike(thread);
        }
        [HttpPost("comment/add")]
        public async Task AddLike(Comment comment)
        {
            await threadService.AddLike(comment);
        }
        [HttpPost("thread/remove")]
        public async Task RemoveLike(ThreadPost thread)
        {
            await threadService.RemoveLike(thread);
        }
        [HttpPost("comment/remove")]
        public async Task RemoveLike(Comment comment)
        {
            await threadService.RemoveLike(comment);
        }
    }
}
