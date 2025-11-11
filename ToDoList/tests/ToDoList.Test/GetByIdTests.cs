using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi;

namespace ToDoList.Test;

public class GetByIdTests
{

    [Fact]
    public void GetById_ValidId_ReturnsItem()
    {
        // Arrange
        var connectionString = "Data Source=../../../data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();

        var controller = new ToDoItemsController(context);


        // Act
        var result = controller.ReadById(toDoItem.ToDoItemId);
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.NotNull(value);

        Assert.Equal(1, value.Id);
        Assert.Equal(toDoItem.Name, value.Name);
        Assert.Equal(toDoItem.Description, value.Description);
        Assert.Equal(toDoItem.IsCompleted, value.IsCompleted);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        context.SaveChanges();



    }
    [Fact]
    public void GetById_InvalidId_ReturnsNotFound()
    { // Arrange
        var connectionString = "Data Source=../../../data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context);

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
