using System;
using Core.Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ApplicationUser> UsersRepository { get; }
        IGenericRepository<Theme> ThemesRepository { get; }
        IGenericRepository<Message> MessagesRepository { get; }

        void SaveChanges();
    }
}