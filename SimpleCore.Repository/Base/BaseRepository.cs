using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = _context.Set<TEntity>().AsQueryable();

            return await entities.ToListAsync();
        }
        public async Task<TEntity> GetByLongIdAsync(long id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<bool> AddAllAsync(List<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);

            return await _context.SaveChangesAsync() >= entities.Count;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> UpdateAllAsync(List<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            int affectedRows = await _context.SaveChangesAsync();

            return affectedRows >= entities.Count;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            int affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }
    }
}
