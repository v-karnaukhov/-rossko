using System.Threading.Tasks;
using PermutationsService.Web.DataAccess.Abstract;

namespace PermutationsService.Web.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork()
        {
            _context = DbContextFactory.Create("DB1");
            PermutationsRepository = new PermutationsRepository(_context);
        }

        public IPermutationsRepository PermutationsRepository { get; set; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
