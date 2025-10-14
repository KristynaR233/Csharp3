using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;

namespace ToDoList.Test;

public class GetTests
{

        public void Get_AllItems_ReturnsAllItems()
    {
        //Arrange
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false

        };
        var controller = new ToDoItemsController();
        controller.AddItemToStorage(toDoItem);

        // Act
        var result = controller.Read();

        // Assert
        Assert.Equals()




    }


    }


