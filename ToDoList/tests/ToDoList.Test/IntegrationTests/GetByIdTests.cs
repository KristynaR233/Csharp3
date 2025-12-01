using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToDoList.Test.IntegrationTests;

public class GetByIdTests
{

    [Fact]
    public async Task GetById_ValidId_ReturnsItem()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);
        var toDoItem = new ToDoItem
        {

            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        context.ToDoItems.Add(toDoItem);
        await context.SaveChangesAsync();

        // Act
        var result = await controller.ReadById(toDoItem.ToDoItemId);
        var value = result.GetValue<ToDoItemGetResponseDto>();



        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.NotNull(value);

        Assert.Equal(toDoItem.ToDoItemId, value.Id);
        Assert.Equal(toDoItem.Description, value.Description);
        Assert.Equal(toDoItem.Name, value.Name);
        Assert.Equal(toDoItem.IsCompleted, value.IsCompleted);




        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        await context.SaveChangesAsync();



    }
    [Fact]
    public async Task GetById_InvalidId_ReturnsNotFound()
    { // Arrange
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

        // Act
        var invalidId = -36;
        var result = await controller.ReadById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        await context.SaveChangesAsync();

    }


}


