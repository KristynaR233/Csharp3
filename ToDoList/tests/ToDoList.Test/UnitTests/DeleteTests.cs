namespace ToDoList.Test.UnitTests;

using NSubstitute;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;
public class DeleteTests
{

    [Fact]
    public void Delete_DeleteByIdValidItemId_ReturnsNoContent()
    {

        //Arrange
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

        //Act

        var result = controller.DeleteById(1);

        //Assert
        Assert.IsType<NoContentResult>(result);

        repositoryMock.Received(1).ReadById(1);
        repositoryMock.Received(1).DeleteById(1);


    }



    [Fact]
    public void Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
       repositoryMock.ReadById(1).Returns((ToDoItem?)null);


        // Act

        var result = controller.DeleteById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        repositoryMock.Received(1).ReadById(1);
        repositoryMock.DidNotReceive().DeleteById(Arg.Any<int>());


    }




}
