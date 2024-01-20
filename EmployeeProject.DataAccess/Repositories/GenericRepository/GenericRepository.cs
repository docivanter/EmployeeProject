using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeProject.DataAccess.Repositories.GenericRepository
{
    // Generic repository for basic CRUD operations on entities of type T
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private EmployeeDbContext _context = null;
        private DbSet<T> table = null;

        // Constructor to initialize the repository with a specific context
        public GenericRepository(EmployeeDbContext sumatusContext)
        {
            this._context = sumatusContext;
            table = _context.Set<T>();
        }

        // Default constructor (can be used when no specific context is provided)
        public GenericRepository()
        {
        }

        // Get a collection of entities based on a specified expression
        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression)
        {
            return await table.Where(expression).ToListAsync();
        }

        // Get a single entity by ID based on a specified expression
        public async Task<T> GetById(Expression<Func<T, bool>> expression)
        {
            return await table.FirstOrDefaultAsync(expression);
        }

        // Insert a new entity into the repository
        public async Task Insert(T obj)
        {
            await table.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        // Update an existing entity in the repository
        public async Task Update(T obj)
        {
            table.Update(obj);
            await _context.SaveChangesAsync();
        }

        // Delete an entity from the repository
        public async Task Delete(T obj)
        {
            table.Remove(obj);
            await _context.SaveChangesAsync();
        }

        // Get a queryable representation of the entity
        public IQueryable<T> GetQueryable<TItem>()
        {
            return table.AsQueryable();
        }
    }
}