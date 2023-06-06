using Advocate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Interfaces
{
    public interface IGenericServiceAsync<T> where T : BaseEntity
    {
        IEnumerable<T> GetAllAsync();
        T FindByIdAsync(int Id);
        int SaveAsync(T obj);
        int UpdateAsync(T obj);
        int DeleteAsync(T obj);
    }
}
