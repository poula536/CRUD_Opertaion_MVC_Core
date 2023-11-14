using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCAppContext _dbContext;
        public GenericRepository(MVCAppContext dbContext)
        {
            _dbContext = dbContext;
            //dbContext = new MVCAppContext();
        }

        public async Task<int> Add(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
            return await _dbContext.SaveChangesAsync();
        } 

        public async Task<int> Delete(T item)
        {
           _dbContext.Set<T>().Remove(item);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<T> Get(int id)
            //=> _dbContext.Set<T>().Where(I => I.Id == id).FirstOrDefault();
            => await _dbContext.Set<T>().FindAsync(id);

        public async Task <IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
                return (IEnumerable<T>) await _dbContext.Set<Employee>().Include(E=> E.Department).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();

        }

        public async Task<int> Update(T item)
        {
            _dbContext.Set<T>().Update(item);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
