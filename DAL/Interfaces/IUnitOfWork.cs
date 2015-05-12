using System;
using System.Threading.Tasks;
using Core.Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Theme> ThemesRepository { get; }
        IGenericRepository<ApplicationUser> UsersRepository { get; }

        Task SaveChangesAsync();
    }
}