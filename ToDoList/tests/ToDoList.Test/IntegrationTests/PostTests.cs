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
    public void Post_ValidRequest_ReturnNewItem()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../IntegrationTests/data/localdb_test.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);
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

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        context.SaveChanges();
    }




}

