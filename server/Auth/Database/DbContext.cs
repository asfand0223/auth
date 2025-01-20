using Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Database
{
    public class ApplicationDbContext : DbContext
    {
        public required DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}
