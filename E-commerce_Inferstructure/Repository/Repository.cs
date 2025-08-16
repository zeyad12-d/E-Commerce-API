using E_commerce_Core.Interfaces;
using E_commerce_Inferstructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_commerce_Inferstructure.Repositry
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDBcontext _context;
        private readonly DbSet<TEntity> _Entity;

        public Repository(ApplicationDBcontext Dbcontext)
        {
            _context = Dbcontext;
            _Entity = Dbcontext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await _Entity.AddAsync(entity);
            return entry.Entity;
        }

        public async Task<bool> Delete(int Id)
        {
           var entitiy= await _Entity.FindAsync(Id);
            if (entitiy == null)
            {
                return false;
            }
            _Entity.Remove(entitiy);
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()=>  await _Entity.AsNoTracking().ToListAsync();
        

        public async Task<TEntity> GetById(int Id)=> await _Entity.FindAsync(Id);


        public void Update(TEntity entity)
        {
            _Entity.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;



        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _Entity.AnyAsync(predicate);
        }
        public IQueryable<TEntity> Query()
        {
            return _Entity.AsQueryable();
        }




        }
}
