using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi;

namespace ToDoList.Test;

public class DeleteTests
{

    [Fact]
    public void Delete_ValidId_ReturnsNoContent()
    {

        //Arrange
        var connectionString = "Data Source=../../../data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context);

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
        var invalidId = -1;
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        context.SaveChanges();

    }




}
