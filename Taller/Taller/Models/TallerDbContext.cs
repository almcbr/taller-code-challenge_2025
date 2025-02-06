using Microsoft.EntityFrameworkCore;

namespace Taller.Models
{
    public class TallerDbContext: DbContext
    {
        public TallerDbContext(DbContextOptions<TallerDbContext> options)
      : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}
