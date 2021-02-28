using Microsoft.EntityFrameworkCore;
using SodicTask.Interface;
using SodicTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SodicTask.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        public readonly TaskDbContext _repositoryContext;
        public RepositoryBase(TaskDbContext repositoryContext) =>
            _repositoryContext = repositoryContext;
        public void Create(T entity) => _repositoryContext.Add(entity);
        public void Update(T entity) => _repositoryContext.Update(entity);
        public void Delete(T entity) => _repositoryContext.Remove(entity);
        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges ? _repositoryContext.Set<T>() : _repositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges) =>
            trackChanges ? _repositoryContext.Set<T>().Where(expression) : _repositoryContext.Set<T>().Where(expression).AsNoTracking();
      
         public async Task<bool> IsUnique(System.Linq.Expressions.Expression<Func<T, bool>> expression) =>
            await _repositoryContext.Set<T>().AnyAsync(expression) ? false : true;
      
    }
}