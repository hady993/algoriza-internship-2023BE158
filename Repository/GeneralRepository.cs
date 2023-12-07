using Core.Domain;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public GeneralRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddEntityAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<bool> EditEntityAsync(TEntity entity, int id)
        {
            if (await GetEntityByIdAsync(id) == null)
            {
                return false;
            }

            _context.Set<TEntity>().Update(entity);
            return true;
        }

        public void DeleteEntity(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<bool> DeleteEntityByIdAsync(int id)
        {
            var entity = await GetEntityByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            DeleteEntity(entity);
            return true;
        }

        public async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

    }
}
