using Microsoft.EntityFrameworkCore;
using PermutationsService.Web.DataAccess.Abstract;
using PermutationsService.Web.DataAccess.Entities;

namespace PermutationsService.Web.DataAccess.Concrete
{
    public class PermutationsRepository : GenericRepository<PermutationEntry>, IPermutationsRepository
    {
        public PermutationsRepository(DbContext context) : base(context)
        {
        }
    }
}
