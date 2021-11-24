using Data;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SchoolDbContext _context;
        private readonly DbSet<T> table;

        public GenericRepository(SchoolDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task AddAsync(T data)
        {
           await table.AddAsync(data);
        }

        public void Delete(T data)
        {
            table.Remove(data);
        }

        public void Update(T data)
        {
            table.Update(data);
        }
    }
}
