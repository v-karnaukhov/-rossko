using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PermutationsService.Data.DataAccess.Entities;

namespace PermutationsService.Data.DataAccess
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
