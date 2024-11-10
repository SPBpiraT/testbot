using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserDataService.Grpc.Models;

namespace UserDataService.Grpc.Data
{
    public class UserDataContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<TgUser> Users { get; set; }

        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
