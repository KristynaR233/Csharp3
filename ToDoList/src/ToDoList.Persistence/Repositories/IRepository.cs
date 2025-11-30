using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domain.Models;

namespace ToDoList.Persistence.Repositories
{
    public interface IRepositoryAsync<T>
    where T : class
    {
        public Task Create(T item);

        public Task<IEnumerable<T>> Read();

        public Task<T?> ReadById(int id);

        public Task UpdateById(T item);

        public Task DeleteById(int id);
    }
}
