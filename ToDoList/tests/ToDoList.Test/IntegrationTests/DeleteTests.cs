using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

namespace ToDoList.Test.IntegrationTests;

public class DeleteTests
{


    [Fact]
    public async Task Delete_ValidId_ReturnsNoContent()
    {


        //Arrange
        var connectionString = ("Data Source=../../../IntegrationTests/data/localdb_test.db");
        using var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        repository.Create(toDoItem);
        await context.SaveChangesAsync();

        //Act

        var result = await controller.DeleteById(1);

        //Assert
        Assert.IsType<NoContentResult>(result);
        var deletedItem = await context.ToDoItems.FindAsync(toDoItem.ToDoItemId);
        Assert.Null(deletedItem);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../IntegrationTests/data/localdb_test.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        repository.Create(toDoItem);
        await context.SaveChangesAsync();




        // Act
        var invalidId = -1;
        var result = await controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        await context.SaveChangesAsync();
    }




}





