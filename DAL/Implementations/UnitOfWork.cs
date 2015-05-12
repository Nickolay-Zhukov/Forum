﻿using System;
using System.Threading.Tasks;
using Core.Models;
using DAL.DbContext;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        #region Constructor
        public UnitOfWork()
        {
            _dbContext = new ApplicationDbContext();
        }
        #endregion

        // Repositories
        private IGenericRepository<Theme> _themesRepository;
        public IGenericRepository<Theme> ThemesRepository
        {
            get { return _themesRepository ?? (_themesRepository = new GenericRepository<Theme>(_dbContext)); }
        }

        private IGenericRepository<ApplicationUser> _usersRepository;
        public IGenericRepository<ApplicationUser> UsersRepository
        {
            get { return _usersRepository ?? (_usersRepository = new GenericRepository<ApplicationUser>(_dbContext)); }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        #region IDisposable members
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing) _dbContext.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}