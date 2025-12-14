using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using Microsoft.AspNetCore.Cors.Infrastructure;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using System.Threading.Tasks;

namespace ToDoList.Test.IntegrationTests;

public class PostTests
{

    [Fact]
    public async Task Post_ValidRequest_ReturnNewItem()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../IntegrationTests/data/localdb_test.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false,
            Category: "Prace"
        );

        // Act
        var result = await controller.Create(request);
        var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var value = Assert.IsType<ToDoItemGetResponseDto>(createdAtResult.Value);


        // Assert


        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.Name, value.Name);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Category, value.Category);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        await context.SaveChangesAsync();
    }




}

