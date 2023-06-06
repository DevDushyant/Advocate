using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Services
{
    public abstract class GenericServiceAsync<T> : IGenericServiceAsync<T> where T : BaseEntity
    {
        private readonly AdvocateContext _context;
        private DbSet<T> entities;

        public GenericServiceAsync(AdvocateContext _context)
        {
            this._context = _context;
            entities = _context.Set<T>();
        }        

        public int DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            else
            {
                var record_detail = entities.SingleOrDefault(s => s.Id == entity.Id);
                _context.Remove(record_detail);
                return _context.SaveChanges();
            }

        }

        public T FindByIdAsync(int Id)
        {
            return entities.SingleOrDefault(s => s.Id == Id);
        }

        public IEnumerable<T> GetAllAsync()
        {
           return  entities.AsEnumerable<T>();
        }

        public int SaveAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                entities.Add(entity);
               _context.SaveChanges();
                return entity.Id;
            }
        }

        public int UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                else
                {
                    return _context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                return 0;
            }
        }
    }
}
