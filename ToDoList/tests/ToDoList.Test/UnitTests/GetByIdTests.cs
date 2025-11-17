
using NSubstitute;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

namespace ToDoList.Test.UnitTests;

public class GetByIdTests
{
     [Fact]
    public void GetById_ValidId_ReturnsItem()
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

        var controller = new ToDoItemsController(null, repositoryMock);

        // Act
        var result = controller.ReadById(toDoItem.ToDoItemId);
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.NotNull(value);

        Assert.Equal(1, value.Id);
        Assert.Equal(toDoItem.Name, value.Name);
        Assert.Equal(toDoItem.Description, value.Description);
        Assert.Equal(toDoItem.IsCompleted, value.IsCompleted);



    }
    [Fact]
    public void GetById_InvalidId_ReturnsNotFound()
    { // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(null, repositoryMock);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        // Act
        var invalidId = -36;
        var result = controller.ReadById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);


    }




}



