using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BaseRepo<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _table;


        public BaseRepo(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }


        public async Task<bool> CreateAsync(TEntity entity)
        {
            if (entity == null)
                return false;

           _table.Add(entity);
           return await _context.SaveChangesAsync() > 0;
           
        }

    }
}
