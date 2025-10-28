using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;

namespace ToDoList.Test;

public class GetTests
{   [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        //Arrange
        var toDoItem1 = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno2",
            Description = "Popis2",
            IsCompleted = false
        };
         var toDoItem2 = new ToDoItem
        {
            ToDoItemId = 2,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        var controller = new ToDoItemsController();
        controller.AddItemToStorage(toDoItem1);
        controller.AddItemToStorage(toDoItem2);
        //Act
        var result = controller.Read();
        var value = result.GetValue();


        //Assert
        Assert.NotNull(value);

        var firstToDo = value.First();
        Assert.Equal(1, firstToDo.Id);




    }

}
