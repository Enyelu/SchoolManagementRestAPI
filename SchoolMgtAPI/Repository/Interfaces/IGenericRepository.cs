using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T data);
        void Delete(T data);
        void Update(T data);
    }
}
    
