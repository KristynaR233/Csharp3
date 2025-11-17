using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domain.Models;

namespace ToDoList.Persistence.Repositories
{
    public class ToDoItemsRepository : IRepository<ToDoItem>
    {
        private readonly ToDoItemsContext context;
         public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }

        public void Create(ToDoItem item)
        {
            context.ToDoItems.Add(item);
            context.SaveChanges();

        }

        public IEnumerable<ToDoItem> Read() => context.ToDoItems.ToList();


        public ToDoItem ReadById(int id) => context.ToDoItems.Find(id);


        public void UpdateById(ToDoItem item)
        {
            var itemToUpdate = context.ToDoItems.Find(item.ToDoItemId);
            if (itemToUpdate == null)
            {
                throw new KeyNotFoundException($"Item with ID {item.ToDoItemId} not found");
            }
            itemToUpdate.Name = item.Name;
            itemToUpdate.Description = item.Description;
            itemToUpdate.IsCompleted = item.IsCompleted;

            context.SaveChanges();


        }


        public void DeleteById (int id)
        {
             var itemToDelete = context.ToDoItems.Find(id);
            if (itemToDelete is null)
            {
               throw new NotImplementedException();
            }
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }


    }
}
