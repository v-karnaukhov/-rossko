using Microsoft.EntityFrameworkCore;
using PermutationsService.Web.DataAccess.Entities;

namespace PermutationsService.Web.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<PermutationEntry> PermutationEntries { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
    }
}
