using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi;

namespace ToDoList.Test;

public class DeleteTests
{
    [Fact]
    public void Delete_ValidId_ReturnsNoContent()
    {

        //Arrange
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        //Act
        var controller = new ToDoItemsController();
        controller.AddItemToStorage(toDoItem);

        var result = controller.DeleteById(1);

        //Assert
        Assert.IsType<NoContentResult>(result);

    }

    [Fact]
    public void Delete_ValidId_ReturnsNotFound()
    {
        // Arrange
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        var controller = new ToDoItemsController();
        controller.AddItemToStorage(toDoItem);

        // Act
        var invalidId = -36;
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
       
    }
 }
