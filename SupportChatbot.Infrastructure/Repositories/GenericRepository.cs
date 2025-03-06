using MicroOrm.Dapper.Repositories;
using SupportChatbot.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupportChatbot.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DapperRepository<T> repository;

        protected GenericRepository(DapperRepository<T> repo)
        {
            repository = repo;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await repository.FindAllAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await repository.FindAllAsync(predicate);
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await repository.FindByIdAsync(id);
        }

        public virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return repository.FindAsync(predicate);
        }

        public virtual async Task<bool> AddAsync(T item)
        {
            return await repository.InsertAsync(item);
        }


        public virtual Task<int> AddBulkAsync(IEnumerable<T> items, IDbTransaction transaction = null)
        {
            return repository.BulkInsertAsync(items, transaction);
        }

        public virtual async Task<bool> UpdateAsync(T item, params Expression<Func<T, object>>[] includes)
        {
            return await repository.UpdateAsync(item, includes);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var item = repository.FindById(id);
            return await repository.DeleteAsync(item);
        }


    }
}
