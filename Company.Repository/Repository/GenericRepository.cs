using Company.Data.Contexts;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }

        public void Add(T Entity) => _context.Set<T>().Add(Entity);


        public void Delete(T Entity) => _context.Set<T>().Remove(Entity);

        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();

        public T GetById(int id) => _context.Set<T>().Find(id);

        public void Update(T Entity) => _context.Set<T>().Update(Entity);


    }
}
