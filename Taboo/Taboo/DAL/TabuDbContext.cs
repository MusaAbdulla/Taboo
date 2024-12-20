using Microsoft.EntityFrameworkCore;
using Taboo.Entities;

namespace Taboo.DAL
{
    public class TabuDbContext : DbContext
    {
        public TabuDbContext(DbContextOptions opt) : base(opt)
        {
        }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Game > Games { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<BannedWord> BannedWords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TabuDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
