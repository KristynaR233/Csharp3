using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

namespace ToDoList.Test.IntegrationTests;

public class PutTests
{

    [Fact]
    public async Task Put_ValidId_ReturnsNoContent()
    {

        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../IntegrationTests/data/localdb_test.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        await context.ToDoItems.AddAsync(toDoItem);
        await context.SaveChangesAsync();



        var request = new ToDoItemUpdateRequestDto(
Name: "Nove jmeno",
Description: "Novy popis",
IsCompleted: true
        );

        // Act

        var result =await controller.UpdateById(toDoItem.ToDoItemId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task Put_InvalidId_ReturnsNotFound()
    {
        // Arrow
        var context = new ToDoItemsContext("Data Source=../../../IntegrationTests/data/localdb_test.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };



        var request = new ToDoItemUpdateRequestDto(
Name: "Nove jmeno",
Description: "Novy popis",
IsCompleted: true
        );


        // Act
        var invalidId = -36;
        var result = controller.UpdateById(invalidId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);

        // Clean up
        context.ToDoItems.RemoveRange(context.ToDoItems);
        await context.SaveChangesAsync();
    }



}
