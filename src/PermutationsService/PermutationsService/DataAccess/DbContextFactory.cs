using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PermutationsService.Web.DataAccess
{
    public static class DbContextFactory
    {
        public static Dictionary<string, string> ConnectionStrings { get; set; }

        public static void SetConnectionString(Dictionary<string, string> connStrs)
        {
            ConnectionStrings = connStrs;
        }

        public static DataContext Create(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                throw new ArgumentNullException(nameof(connectionId));
            }

            var connStr = ConnectionStrings[connectionId];
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connStr);

            return new DataContext(optionsBuilder.Options);
        }
    }
}
