using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.core.Interface
{
    public interface IRepository<T> where T : class
    {
        T GetById(Int64 id);
        T GetSingleBySpec(ISpecification<T> spec);
        IEnumerable<T> ListAll();
        IEnumerable<T> List(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
