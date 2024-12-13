using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AskHub.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.SourceQuestions) // Configure SourceQuestions
                .WithOne(q => q.SourceAppUser)
                .HasForeignKey(q => q.SourceAppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.DestinationQuestions) // Configure DestinationQuestions
                .WithOne(q => q.DestinationAppUser)
                .HasForeignKey(q => q.DestinationAppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasCheckConstraint("CK_Questions_SourceDestinationNotEqual",
                                    "[SourceAppUserId] IS NULL OR [DestinationAppUserId] IS NULL OR [SourceAppUserId] <> [DestinationAppUserId]");

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Answer)
                .WithOne(a => a.Question)
                .HasForeignKey<Answer>(a => a.QuestionId)
                .IsRequired(false); 

            base.OnModelCreating(modelBuilder);
        }


    }
}
