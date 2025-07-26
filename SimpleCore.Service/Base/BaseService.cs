using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleCore.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Service.Base
{
    public class BaseService<TEntity,TView> : IBaseService<TEntity,TView> where TEntity : class, new()
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseRepository<TEntity> _repository;

        public BaseService(IMapper mapper, IBaseRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            
            return entities;
        }
        public async Task<TEntity> GetByLongIdAsync(long id)
        {
            return await _repository.GetByLongIdAsync(id);
        }
        public async Task<bool> AddAllAsync(List<TEntity> entities)
        {
            return await _repository.AddAllAsync(entities);
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAllAsync(List<TEntity> entities)
        {
            return await _repository.UpdateAllAsync(entities);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _repository.UpdateAsync(entity);
        }
    }
}
