using Microsoft.EntityFrameworkCore;
using PermutationsService.Data.DataAccess.Abstract;
using PermutationsService.Data.DataAccess.Entities;

namespace PermutationsService.Data.DataAccess.Concrete
{
    public class PermutationsRepository : GenericRepository<PermutationEntry>, IPermutationsRepository
    {
        public PermutationsRepository(DbContext context) : base(context)
        {
        }
    }
}
