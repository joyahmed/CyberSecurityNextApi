
namespace CyberSecurityNextApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Tag> Tag => Set<Tag>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "SuperAdmin" },
                new Role { Id = 2, Name = "Admin" },
                new Role { Id = 3, Name = "User" }
            );

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Menu)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();

            modelBuilder.Entity<Post>()
     .HasIndex(p => p.CategoryId)
     .IsUnique();

            modelBuilder.Entity<Post>()
                .HasIndex(p => p.PostTitle)
                .IsUnique();

            modelBuilder.Entity<Post>()
                .HasIndex(p => p.Subtitle)
                .IsUnique();
        }
    }
}
