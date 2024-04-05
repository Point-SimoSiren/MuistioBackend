using Microsoft.EntityFrameworkCore;
using MuistioBackend.Models;

namespace MuistioBackend.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Memo> Memos { get; set; }
    }
}
