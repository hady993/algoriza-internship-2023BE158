﻿using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IGeneralRepository<TEntity> where TEntity : class
    {
        Task AddEntityAsync(TEntity entity);
        Task AddEntitiesAsync(IEnumerable<TEntity> entities);
        Task<bool> EditEntityAsync(TEntity entity, int id);
        void DeleteEntity(TEntity entity);
        Task<bool> DeleteEntityByIdAsync(int id);
        Task<TEntity> GetEntityByIdAsync(int id, string? includeProperties = null);
        Task<IEnumerable<TEntity>> GetAllAsync(string? includeProperties = null);
    }
}
