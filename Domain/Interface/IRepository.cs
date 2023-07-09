using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> condition = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

        IEnumerable<T> GetList(Expression<Func<T, bool>> condition = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

        //IEnumerable<Entity> GetAll();   
        //IEnumerable<Entity> Find(Expression<Func<Entity, bool>> predicate);

        //Task<Entity> Get(Expression<Func<Entity, bool>> condition = null,Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>> includes = null);

        string Update(T entity);
        string Add(T entity);

        string Remove(int id);
    }
}
