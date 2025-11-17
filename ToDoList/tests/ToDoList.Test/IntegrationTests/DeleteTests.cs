using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

namespace ToDoList.Test.IntegrationTests;

public class DeleteTests
{

    [Fact]
    public void Delete_ValidId_ReturnsNoContent()
    {

        //Arrange
        var context = new ToDoItemsContext("Data Source=../../../IntergrationTests/data/localdb_test.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(context, repository);

        var ToDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        //Act

        var result = controller.DeleteById(1);

        //Assert
        Assert.IsType<NotFoundResult>(result);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        context.SaveChanges();


    }



    [Fact]
    public void Delete_ValidId_ReturnsNotFound()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../IntergrationTests/data/localdb_test.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(context, repository);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };



        // Act
        var invalidId = -1;
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        context.SaveChanges();

    }




}
