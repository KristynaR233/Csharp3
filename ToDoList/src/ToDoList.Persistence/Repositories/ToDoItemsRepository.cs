using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

namespace ToDoList.Persistence.Repositories
{
    public class ToDoItemsRepository : IRepositoryAsync<ToDoItem>
    {
        private readonly ToDoItemsContext context;
         public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }

        public async Task CreateAsync(ToDoItem item)
        {
            await context.ToDoItems.AddAsync(item);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<ToDoItem>> ReadAsync() => await context.ToDoItems.ToListAsync();


        public async Task <ToDoItem?> ReadByIdAsync(int id) => await context.ToDoItems.FindAsync(id);


        public async Task UpdateByIdAsync(ToDoItem item)
        {
            var itemToUpdate = await context.ToDoItems.FindAsync(item.ToDoItemId) ?? throw new ArgumentOutOfRangeException ($"ToDo item with ID{item.ToDoItemId} not found.");
            context.Entry(itemToUpdate).CurrentValues.SetValues(item);
            await context.SaveChangesAsync();


        }


        public async Task DeleteByIdAsync(int id)
        {
            var itemToDelete = await context.ToDoItems.FindAsync(id) ?? throw new ArgumentOutOfRangeException ($"ToDo item with ID{id} not found.");
            context.ToDoItems.Remove(itemToDelete);
            await context.SaveChangesAsync();
        }


    }
}
