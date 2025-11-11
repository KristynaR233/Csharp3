
using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Persistence;
using ToDoList.Domain.DTOs;

namespace ToDoList.Test;

public class GetTests
{

    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        //Arrange
        var connectionString = "Data Source=../../../data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context);

        var createRequest1 = new ToDoItemCreateRequestDto("Task1", "Desc1", false);
        var createRequest2 = new ToDoItemCreateRequestDto("Task2", "Desc2", false);



        //Act
        var result = controller.Read();
        var value = result.GetValue();


        //Assert
        Assert.NotNull(value);

        var firstToDo = value.First();

        Assert.Equal(createRequest1.Name, firstToDo.Name);
        Assert.Equal(createRequest1.Description, firstToDo.Description);
        Assert.Equal(createRequest1.IsCompleted, firstToDo.IsCompleted);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        context.SaveChanges();




    }



}
