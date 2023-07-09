using Data.Context;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StagePerfContext Context;
        public Repository(StagePerfContext context)
        {
            Context = context;
        }

        public string Add(T entity)
        {
            string response = "";
            try
            {
                Context.Set<T>().Add(entity);
                Context.SaveChanges();

                response = "Added done";

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response = "Add Not done , with Exeption \n" + e;
            }
            return response;
        }

        public T Get(Expression<Func<T, bool>> condition = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            try
            {
                IQueryable<T> query = Context.Set<T>();

                if (includes != null)
                {
                    query = includes(query);
                }

                if (condition != null)
                {
                    return query.FirstOrDefault(condition);
                }

                return query.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> condition = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            try
            {

                IQueryable<T> query = Context.Set<T>();

                if (includes != null)
                {
                    query = includes(query);
                }
                if (condition != null)
                    return query.Where(condition).ToList();

                else
                    return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public string Remove(int id)
        {
            if (id == null)
            {
                return "Id null";
            }

            try
            {
                T table = Context.Set<T>().Find(id);
                Context.Set<T>().Remove(table);
                Context.SaveChanges();
                return "Delete Done";
            }
            catch (Exception ex)
            {
                return "Delete error";


            }
        }

        public string Update(T entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
                return "Update Done";


            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
