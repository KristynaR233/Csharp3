using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Data.Common;
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


        public ToDoItem? ReadById(int id) => context.ToDoItems.Find(id);


        public void UpdateById(ToDoItem item)
        {
            var itemToUpdate = context.ToDoItems.Find(item.ToDoItemId) ?? throw new ArgumentOutOfRangeException ($"ToDo item with ID{item.ToDoItemId} not found.");
            context.Entry(itemToUpdate).CurrentValues.SetValues(item);
            context.SaveChanges();


        }


        public void DeleteById (int id)
        {
             var itemToDelete = context.ToDoItems.Find(id) ?? throw new ArgumentOutOfRangeException ($"ToDo item with ID{id} not found.");
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }


    }
}
