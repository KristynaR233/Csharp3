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
        var connectionString = "Data Source=../../../IntergrationTests/data/localdb_test.db";
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
        context.SaveChanges();

        // Act
        var result = controller.ReadById(toDoItem.ToDoItemId);
        var resultResult = result.Result;
        var value = result.GetValue();



        // Assert
       Assert.IsType<OkObjectResult>(resultResult);
       Assert.NotNull(value);

       Assert.Equal(toDoItem.ToDoItemId, value.Id);
       Assert.Equal(toDoItem.Description, value.Description);
       Assert.Equal(toDoItem.Name, value.Name);
       Assert.Equal(toDoItem.IsCompleted, value.IsCompleted);




        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        context.SaveChanges();



    }
    [Fact]
    public void GetById_InvalidId_ReturnsNotFound()
    { // Arrange
        var context = new ToDoItemsContext("Data Source=../../../IntergrationTests/data/localdb_test.db");
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
        var result = controller.ReadById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        context.SaveChanges();

    }




}
