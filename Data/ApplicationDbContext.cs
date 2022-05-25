namespace FastAndFuriousApi.Data
{
    using FastAndFuriousApi.Domain.Quote;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    namespace IWantApp.Data
    {
        public class ApplicationDbContext : IdentityDbContext<IdentityUser>
        {

            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

            public DbSet<Author> Authors { get; set; }
            public DbSet<Phrase> Phrases { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Author>().Property(p => p.Name).IsRequired();
                modelBuilder.Entity<Author>().Property(p => p.Movie).IsRequired();
                modelBuilder.Entity<Phrase>().Property(p => p.Text).IsRequired();
            }

            protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
            {
                configuration.Properties<string>().HaveMaxLength(200);
            }

            // protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseMySql("Server=localhost;port=3306;Database=entityFrameworkStudy;Uid=root;Pwd=password;", ServerVersion.Create(new Version(5, 7, 9), ServerType.MySql));
        }
    }
}