using System.Threading.Tasks;

namespace SGM.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}