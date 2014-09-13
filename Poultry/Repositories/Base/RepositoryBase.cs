using Poultry.DbContexts;
using Poultry.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Poultry.Repositories.Base
{
    public interface IRepositoryBase<T> where T : class, IModel
    {
        T Create(T t);
        T Update(T t);
        T GetById(int id);
        IEnumerable<T> GetAll();
        T Delete(T t);
    }


    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IModel
    {
        private readonly DataBaseContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase()
        {
            var dbContext = new DataBaseContext();
            var type = typeof(T).Name;
            var prop = typeof(DataBaseContext).GetProperty(type);
            _dbSet = prop.GetValue(dbContext) as DbSet<T>;
            _context = dbContext;

        }



        #region CRUD Methods
        public T Create(T t)
        {
            t.CreatedOn = DateTime.Now;
            t.LastModifiedOn = DateTime.Now;
            t.IsDeleted = false;
            try
            {
                _dbSet.Add(t);
                _context.SaveChanges();
                return t;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Update(T t)
        {
            t.LastModifiedOn = DateTime.Now;
            t.IsDeleted = false;
            try
            {
                _context.Entry(t).State = EntityState.Modified;
                _context.SaveChanges();
                return t;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public T GetById(int id)
        {
            try
            {
                return _dbSet.First(t => t.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _dbSet.Where(t => t.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Delete(T t)
        {
            try
            {
                t.IsDeleted = true;
                _context.Entry(t).State = EntityState.Modified;
                _context.SaveChanges();
                return t;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}