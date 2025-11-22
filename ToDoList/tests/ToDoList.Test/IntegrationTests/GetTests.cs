
using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Persistence;
using ToDoList.Domain.DTOs;
using ToDoList.Persistence.Repositories;

namespace ToDoList.Test.IntegrationTests;

public class GetTests
{

    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        //Arrange
        var context = new ToDoItemsContext("Data Source=../../../IntergrationTests/data/localdb_test.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var createRequest1 = new ToDoItemCreateRequestDto("Task1", "Desc1", false);
        var createRequest2 = new ToDoItemCreateRequestDto("Task2", "Desc2", false);
        controller.Create(createRequest1);
        controller.Create(createRequest2);



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
