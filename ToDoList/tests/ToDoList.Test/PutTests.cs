using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;

namespace ToDoList.Test;

public class PutTests : IDisposable
{
    private readonly ToDoItemsController _controller = new ToDoItemsController();
    [Fact]
    public void Put_ValidId_ReturnsNoContent()
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

        var request = new ToDoItemUpdateRequestDto(
Name: "Nove jmeno",
Description: "Novy popis",
IsCompleted: true
        );

        // Act

        var result = controller.UpdateById(toDoItem.ToDoItemId, request);

        // Assert
        Assert.IsType<NoContentResult>(request);
    }

    [Fact]
    public void Put_InvalidId_ReturnsNotFound()
    {
        // Arrow
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        var controller = new ToDoItemsController();
        controller.AddItemToStorage(toDoItem);

        var request = new ToDoItemUpdateRequestDto(
Name: "Nove jmeno",
Description: "Novy popis",
IsCompleted: true
        );


        // Act
        var invalidId = -36;
        var result = controller.UpdateById(invalidId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    public void Dispose()
    {
        _controller.ClearStorage();
    }


}
