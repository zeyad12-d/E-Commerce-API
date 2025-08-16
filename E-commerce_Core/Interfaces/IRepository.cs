using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetById(int Id);

        void Update(TEntity entity);

        Task<bool> Delete(int Id);

        Task <TEntity>  AddAsync(TEntity entity);
        IQueryable<TEntity> Query();
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);






    }
}
