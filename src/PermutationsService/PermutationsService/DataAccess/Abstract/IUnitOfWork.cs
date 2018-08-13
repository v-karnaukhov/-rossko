using System;
using System.Threading.Tasks;

namespace PermutationsService.Web.DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IPermutationsRepository PermutationsRepository { get; set; }
        Task<int> SaveAsync();
        int Save();
    }
}
