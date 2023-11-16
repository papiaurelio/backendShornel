using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly StoreDbContext _context;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
                return await _context.SaveChangesAsync();
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Respository<TEntity>() where TEntity : ClaseBase
        {
            if(_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoyryInstance = Activator.CreateInstance(repositoryType
                    .MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoyryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}
