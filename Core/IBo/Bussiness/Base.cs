using MI.Dal.IDbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MI.Bo
{
    public abstract class Base<T> where T : class
    {
        public List<T> getObjectToList(List<object> obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return JsonConvert.DeserializeObject<List<T>>(json);
        }
        public bool InsertOrUpdate(T entity, string key)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var obj = _context.Set<T>().Find(key);
                    if (obj != null)
                    {
                        _context.Set<T>().Update(entity);

                    }
                    else
                    {
                        _context.Add(entity);
                    }
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(Logger.LogType.Error, ex.Message);

                return false;
            }
        }
        public bool Add(T entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Add(entity);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
               //Logger.WriteLog(Logger.LogType.Error, ex.Message);

                return false;
            }
        }
        public List<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    IQueryable<T> items = _context.Set<T>();
                    if (includeProperties != null)
                    {
                        foreach (var includeProperty in includeProperties)
                        {
                            items = items.Include(includeProperty);
                        }
                    }
                    return items.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<T>();
        }
        public List<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            using (IDbContext _context = new IDbContext())
            {
                IQueryable<T> items = _context.Set<T>();
                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        items = items.Include(includeProperty);
                    }
                }
                return items.Where(predicate).ToList();
            }
        }
        public T FindById(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    IQueryable<T> items = _context.Set<T>();

                    if (includeProperties != null)
                    {
                        foreach (var includeProperty in includeProperties)
                        {
                            items = items.Include(includeProperty);
                        }
                    }
                    return items.FirstOrDefault(predicate);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public T FindById(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.Set<T>().Find(id);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public T FindById(string id, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.Set<T>().Find(id);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public bool Remove(T entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Set<T>().Remove(entity);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool Remove(int id)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var entity = _context.Set<T>().Find(id);
                    _context.Set<T>().Remove(entity);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool RemoveMultiple(List<T> entities)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Set<T>().RemoveRange(entities);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool Update(T entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Set<T>().Update(entity);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
