using jeffit.jeffstampe.dk.Shared.Logic;
using Microsoft.EntityFrameworkCore;

namespace jeffit.jeffstampe.dk.Server.Model
{
    public class ThreadContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ThreadPost> Threads { get; set; }
        public DbSet<User> Users { get; set; }
        public string DbPath { get; }
        public ThreadContext(DbContextOptions<ThreadContext> options) : base(options)
        {
            DbPath = "bin/TodoTask.db";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Creator)
                .WithMany()
                .HasForeignKey("CreatorId");

            modelBuilder.Entity<ThreadPost>()
                .HasMany(tp => tp.Comments)
                .WithOne()
                .HasForeignKey("ThreadPostId");

            modelBuilder.Entity<ThreadPost>()
                .HasOne(tp => tp.Creator)
                .WithMany()
                .HasForeignKey("CreatorId");
        }

    }
}

