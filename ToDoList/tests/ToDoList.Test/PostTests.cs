using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace ToDoList.Test;

public class PostTests : IDisposable
{
    private readonly ToDoItemsController _controller = new ToDoItemsController();
    [Fact]
    public void Post_ValidRequest_ReturnNewItem()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request);
        var value = result.GetValue();


        // Assert
        Assert.IsType<ToDoItemCreateRequestDto>(result.Value);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.Name, value.Name);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
    }
    public void Dispose()
    {
        _controller.ClearStorage();
    }


}
