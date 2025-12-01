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
    public async Task Delete_DeleteByIdValidItemId_ReturnsNoContent()
    {

        //Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
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

        var result = await controller.DeleteById(1);

        //Assert
        Assert.IsType<NoContentResult>(result);

        await repositoryMock.Received(1).ReadById(1);
        await repositoryMock.Received(1).DeleteById(1);


    }



    [Fact]
    public async Task Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
       repositoryMock.ReadById(1).Returns((ToDoItem?)null);


        // Act

        var result = await controller.DeleteById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        await repositoryMock.Received(1).ReadById(1);
        await repositoryMock.DidNotReceive().DeleteById(Arg.Any<int>());


    }




}
