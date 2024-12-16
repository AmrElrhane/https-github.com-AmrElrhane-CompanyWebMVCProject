using Company.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        T GetById(int id);

        IEnumerable<T> GetAll();

        void Add(T Entity);

        void Update(T Entity);

        void Delete(T Entity);
    }
}
