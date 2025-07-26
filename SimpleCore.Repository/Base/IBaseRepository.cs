using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Repository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 獲取所有
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();
        /// <summary>
        /// 查詢實體 long id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByLongIdAsync(long id);
        /// <summary>
        /// 新增一個實體
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddAsync(TEntity entity);
        /// <summary>
        /// 新增很多實體
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> AddAllAsync(List<TEntity> entities);
        /// <summary>
        /// 更新單個實體
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity);
        /// <summary>
        /// 更新多個實體
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> UpdateAllAsync(List<TEntity> entities);
    }
}
