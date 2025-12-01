using NSubstitute;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

namespace ToDoList.Test.UnitTests;

public class GetTests
{   [Fact]
    public async Task Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
         var toDoItem1 = new ToDoItem
            {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
            };

         var toDoItem2 = new ToDoItem
            {
            ToDoItemId = 2,
            Name = "Jmeno2",
            Description = "Popis2",
            IsCompleted = false
            };
            var someItemList = new List<ToDoItem> {toDoItem1, toDoItem2};

        repositoryMock.Read().Returns(someItemList);


        //Act
        var result = await controller.Read();
        var resultResult = result.Result;
        var value = result.GetValue();


        //Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);
        Assert.Equal(toDoItem1.ToDoItemId, value.First().Id);
        Assert.Equal(toDoItem1.Name, value.First().Name);

       await repositoryMock.Received(1).Read();







    }

}
