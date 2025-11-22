using NSubstitute;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;
using ToDoList.Domain.DTOs;

namespace ToDoList.Test.UnitTests;

public class PutTests
{
    [Fact]
    public void Put_UpdateByIdWhenItemUpdated_ReturnsNOContent()
    {

        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        repositoryMock.ReadById(1).Returns(toDoItem);
        var controller = new ToDoItemsController(repositoryMock);

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
    public void Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        // Arrow
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
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
