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
{
    public void Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(null, repositoryMock);
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

        repositoryMock.Read().Returns(new List<ToDoItem> {toDoItem1, toDoItem2});


        //Act
        var result = controller.Read();
        var value = result.GetValue();


        //Assert
        Assert.NotNull(value);

        var firstToDo = value.First();

        Assert.Equal(toDoItem1.Name, firstToDo.Name);
        Assert.Equal(toDoItem1.Description, firstToDo.Description);
        Assert.Equivalent(toDoItem1.IsCompleted, firstToDo.IsCompleted);






    }

}
