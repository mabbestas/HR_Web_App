﻿using HR_Plus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HR_Plus.Domain.Repositories
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> Any(Expression<Func<T, bool>> expression);
        Task<T> GetDefault(Expression<Func<T, bool>> expression);
        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression);

        Task<TResult> GetFilteredFirstOrDefault<TResult>(
                  Expression<Func<T, TResult>> select,//SELECT
                  Expression<Func<T, bool>> where,//WHERE
                  Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, //SIRALAMA
                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null); //JOİN

        Task<List<TResult>> GetFilteredList<TResult>(
                  Expression<Func<T, TResult>> select,  // select
                  Expression<Func<T, bool>> where,  // where 
                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,  // sıralama
                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null); // join







    }
}
