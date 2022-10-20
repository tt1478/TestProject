using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext _repository;
        public RepositoryBase(RepositoryContext repository)
        {
            _repository = repository;
        }
        public void Create(T entity)
        {
            _repository.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _repository.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges ? _repository.Set<T>() : _repository.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges ? _repository.Set<T>().Where(expression) : _repository.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            _repository.Set<T>().Update(entity);
        }
    }
}
