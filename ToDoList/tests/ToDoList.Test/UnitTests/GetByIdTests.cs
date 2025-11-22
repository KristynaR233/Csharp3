
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
    public void Get_ReakByIdWhenSomeItemAvailable_ReutrunsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var toDoItem = new ToDoItem
            {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
            };
        repositoryMock.ReadById(1).Returns(toDoItem);


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
    public void Get_ReadByIdWhenItemsIsNull_ReturnsNotFound()
    { // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

      repositoryMock.ReadById(Arg.Any<int>()).Returns((ToDoItem?)null);

        // Act
        var result = controller.ReadById(1);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);

        repositoryMock.Received(1).ReadById(1);


    }




}



