using Microsoft.EntityFrameworkCore;
using PermutationsService.Web.DataAccess;
using PermutationsService.Web.DataAccess.Abstract;
using PermutationsService.Web.DataAccess.Concrete;

namespace PermutationsService.Tests.Mocks
{
    public class MockPermutationsService : Web.Services.Concrete.PermutationsService
    {
        private readonly DataContext _context;
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public MockPermutationsService()
        {
            _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DatabaseForTests")
                .Options;
        }

        public override IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(new DataContext(_dbContextOptions));
        }
    }
}
