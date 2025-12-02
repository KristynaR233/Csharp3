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
        public Task CreateAsync(T item);

        public Task<IEnumerable<T>> ReadAsync();

        public Task<T?> ReadByIdAsync(int id);

        public Task UpdateByIdAsync(T item);

        public Task DeleteByIdAsync(int id);
    }
}
