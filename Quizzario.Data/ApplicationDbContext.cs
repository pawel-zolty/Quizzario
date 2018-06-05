using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quizzario.Data.Entities;

namespace Quizzario.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AssignedUser> AssignedUsers { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<QuizSettings> QuizSettingss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<Quiz>().ToTable("Quiz");
            modelBuilder.Entity<AssignedUser>().ToTable("AssignedUser");
            modelBuilder.Entity<Score>().ToTable("Score");
            modelBuilder.Entity<QuizSettings>().ToTable("QuizSettings");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();

    }
}
