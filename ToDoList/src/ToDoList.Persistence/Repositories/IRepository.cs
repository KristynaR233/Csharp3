using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domain.Models;

namespace ToDoList.Persistence.Repositories
{
    public interface IRepository<T>
    where T : class
    {
        public void Create(T item);

        public IEnumerable<T> Read();

        public T? ReadById(int id);

        public void UpdateById(T item);

        public void DeleteById(int id);
    }
}
