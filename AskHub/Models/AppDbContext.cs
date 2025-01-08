using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AskHub.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<FollowUser> FollowUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                 .HasMany(u => u.SourceQuestions) // Configure SourceQuestions
                 .WithOne(q => q.SourceAppUser)
                 .HasForeignKey(q => q.SourceAppUserId)
                 .OnDelete(DeleteBehavior.Restrict); // Restrict delete to avoid cascade cycle

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.DestinationQuestions) // Configure DestinationQuestions
                .WithOne(q => q.DestinationAppUser)
                .HasForeignKey(q => q.DestinationAppUserId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete for destination user
            
            modelBuilder.Entity<Question>()
                .HasCheckConstraint("CK_Questions_SourceDestinationNotEqual",
                                    "[SourceAppUserId] IS NULL OR [DestinationAppUserId] IS NULL OR [SourceAppUserId] <> [DestinationAppUserId]");

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Answer)
                .WithOne(a => a.Question)
                .HasForeignKey<Answer>(a => a.QuestionId)
                .IsRequired(false);

            //modelBuilder.Entity<UserFollower>()
            //.HasKey(uf => new { uf.FollowerUsername, uf.FollowingUsername });

            //modelBuilder.Entity<UserFollower>()
            //    .HasOne(uf => uf.Follower)
            //    .WithMany(u => u.Following)
            //    .HasForeignKey(uf => uf.FollowerUsername)
            //        //.HasPrincipalKey(u => u.UserName) // Specify that UserName is the principal key

            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<UserFollower>()
            //    .HasOne(uf => uf.Following)
            //    .WithMany(u => u.Followers)
            //    .HasForeignKey(uf => uf.FollowingUsername)
            //       // .HasPrincipalKey(u => u.UserName) // Specify that UserName is the principal key

            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }


    }
}
