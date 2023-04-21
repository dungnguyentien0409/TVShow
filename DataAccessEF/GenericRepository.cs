﻿using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;

namespace DataAccessEF
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
        protected readonly TVShowContext context;
        public GenericRepository(TVShowContext context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression);
        }
        public IQueryable<T> GetAll()
        {

            return context.Set<T>();
        }
        public T? GetById(Guid? id)
        {
            return context.Set<T>().Find(id);
        }
        public T GetByIdOrDefault(Guid? id)
        {
            if (id == null) return Activator.CreateInstance<T>();

            var item = context.Set<T>().Find(id);

            return item != null ? item : Activator.CreateInstance<T>();
        }
        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }
    }
}

