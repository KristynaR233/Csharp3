using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using ToDoList.Persistence;

namespace ToDoList.Test;

public class PutTests
{

    [Fact]
    public void Put_ValidId_ReturnsNoContent()
    {

        // Arrange
        var connectionString = "DataSource=../../data/localdb.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();



        var request = new ToDoItemUpdateRequestDto(
Name: "Nove jmeno",
Description: "Novy popis",
IsCompleted: true
        );

        // Act

        var result = controller.UpdateById(toDoItem.ToDoItemId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Put_InvalidId_ReturnsNotFound()
    {
        // Arrow
        var connectionString = "DataSource=../../data/localdb.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context);
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
        Assert.IsType<NotFoundResult>(result);
    }



}
