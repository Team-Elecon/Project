using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApp.core
{
   public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MyCon _context = null;//_context qual is equal to db
        private DbSet<T> table = null;

        public GenericRepository()
        {
            this._context = new MyCon();
            this.table = _context.Set<T>();
        }
        public void Delete(int id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);//_context.SaveChages();
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public async void SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
