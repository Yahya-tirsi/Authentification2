using Authentification2.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentification2.Services
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
