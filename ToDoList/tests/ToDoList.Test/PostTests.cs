using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;

namespace ToDoList.Test;

public class PostTests
{
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
        Assert.IsType<ToDoItemCreateRequestDto>(value);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.Name, value.Name);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
    }

}
