
using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Persistence;

namespace ToDoList.Test;

public class GetTests
{

    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        //Arrange
        var connectionString = "DataSource=../../data/localdb.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context);

        var toDoItem1 = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno1",
            Description = "Popis1",
            IsCompleted = false
        };
        var toDoItem2 = new ToDoItem
        {
            ToDoItemId = 2,
            Name = "Jmeno2",
            Description = "Popis2",
            IsCompleted = true
        };


        //Act
        var result = controller.Read();
        var value = result.GetValue();


        //Assert
        Assert.NotNull(value);

        var firstToDo = value.First();
        Assert.Equal(1, firstToDo.Id);
        Assert.Equal(toDoItem1.Name, firstToDo.Name);
        Assert.Equal(toDoItem1.Description, firstToDo.Description);
        Assert.Equal(toDoItem1.IsCompleted, firstToDo.IsCompleted);




    }



}
