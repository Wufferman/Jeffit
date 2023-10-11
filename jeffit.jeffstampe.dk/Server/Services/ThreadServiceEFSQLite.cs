using jeffit.jeffstampe.dk.Server.Hubs;
using jeffit.jeffstampe.dk.Server.Model;
using jeffit.jeffstampe.dk.Server.Services.Interfaces;
using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace jeffit.jeffstampe.dk.Server.Services
{
    public class ThreadServiceEFSQLite : IThreadService
    {
        private ThreadContext db { get; }

        public ThreadServiceEFSQLite(ThreadContext db)
        {
            this.db = db;
        }

        public async Task CreateThread(ThreadPost thread)
        {
            db.Threads.Add(thread);
            await SaveChanges();
        }

        public Task DeleteThread()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetCommentsByThreadId(int id)
        {
            ThreadPost thread = db.Threads.First(x => x.Id == id);
            return thread.Comments.ToList();
        }

        public async Task<List<ThreadPost>> GetThreads()
        {
            return db.Threads
                .Include(c => c.Creator)
                .Include(com => com.Comments)
                    .ThenInclude(p => p.Creator).ToList();
        }

        public async Task SeedData()
        {
            ThreadPost thread = db.Threads.FirstOrDefault();
            if(thread == null)
            {
                User user = new User("Jeff", "Kode123");
                thread = new ThreadPost("First Thread", user);
                thread.Comments = new List<Comment>();
                thread.Comments.Add(new Comment("First!", user));
                thread.Comments.Add(new Comment("Second Comment", user));
                thread.Comments.Add(new Comment("Third Comment", user));
                db.Threads.Add(thread);
                User user2 = new User("Welat", "Kode123");
                ThreadPost thread2 = new ThreadPost("My First Thread", user);
                thread2.Comments = new List<Comment>();
                thread2.Comments.Add(new Comment("My First!", user));
                thread2.Comments.Add(new Comment("My Second Comment", user));
                thread2.Comments.Add(new Comment("My Third Comment", user));
                db.Threads.Add(thread2);
            }
            db.SaveChanges();

        }

        public async Task AddLike(ThreadPost thread)
        {
            ThreadPost oldThread = db.Threads.First(t => t.Id == thread.Id);
            oldThread.Likes++;
            await SaveChanges();
        }
        public async Task RemoveLike(ThreadPost thread)
        {
            ThreadPost oldThread = db.Threads.First(t => t.Id == thread.Id);
            oldThread.Likes--;
            await SaveChanges();
        }

        public async Task AddLike(Comment comment)
        {
            Comment oldComment = db.Comments.First(c => c.Id == comment.Id);
            oldComment.Likes++;
            await SaveChanges();
        }
        public async Task RemoveLike(Comment comment)
        {
            Comment oldComment = db.Comments.First(c => c.Id == comment.Id);
            oldComment.Likes--;
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            db.SaveChanges();
            await WebsocketHub.TriggerRefresh();
        }

        public Task UpdateThreadPost(ThreadPost thread)
        {
            throw new NotImplementedException();
        }

        public Task UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task CreateComment(Comment comment, int threadId)
        {
            ThreadPost thread = db.Threads.Include(c => c.Comments).First(x => x.Id == threadId);
            thread.Comments.Add(comment);
            await SaveChanges();
        }

        public async Task<User> GetUser(string username)
        {
            User? user = null;

            try
            {
                user = db.Users.First(u => u.Name.ToLower() == username.ToLower());
            }catch { }

            if(user == null)
            {
                user = new User(username, "Kode123");
            }
            return user;
        }
    }
}
