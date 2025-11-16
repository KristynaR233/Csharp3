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

        public void Read() => context.ToDoItems.ToList();


        public void ReadById(int id) => context.ToDoItems.Find(id);


        public void UpdateById (ToDoItem item)
        {
            var itemToUpdate = context.ToDoItems.Find(item.ToDoItemId);
            if (itemToUpdate == null)
            {
                return NotFound();
            }
            itemToUpdate.Name = updatedItem.Name;
            itemToUpdate.Description = updatedItem.Description;
            itemToUpdate.IsCompleted = updatedItem.IsCompleted;

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
