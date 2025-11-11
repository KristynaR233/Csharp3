using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using Microsoft.AspNetCore.Cors.Infrastructure;
using ToDoList.Persistence;

namespace ToDoList.Test;

public class PostTests
{

    [Fact]
    public void Post_ValidRequest_ReturnNewItem()
    {
        // Arrange
        var connectionString = "Data Source=../../../data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request);
        var value = result.GetValue();


        // Assert
        Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.Name, value.Name);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
    }



}
