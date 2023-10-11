using jeffit.jeffstampe.dk.Shared.Logic;

namespace jeffit.jeffstampe.dk.Server.Services.Interfaces
{
    public interface IThreadService
    {
        public Task CreateThread(ThreadPost thread);
        public Task CreateComment(Comment comment, int threadId);
        public Task DeleteThread();
        public Task<List<Comment>> GetCommentsByThreadId(int id);
        public Task<List<ThreadPost>> GetThreads();
        public Task UpdateThreadPost(ThreadPost thread);
        public Task UpdateComment(Comment comment);
        public Task SeedData();
        public  Task AddLike(ThreadPost thread);
        public  Task RemoveLike(ThreadPost thread);
        public  Task AddLike(Comment comment);
        public  Task RemoveLike(Comment comment);
        public  Task<User> GetUser(string username);

    }
}
