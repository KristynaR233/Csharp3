
using NSubstitute;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;
using System.Threading.Tasks;
using ToDoList.Domain.DTOs;

namespace ToDoList.Test.UnitTests;

public class GetByIdTests
{
    [Fact]
    public async Task Get_ReakByIdWhenSomeItemAvailable_ReutrunsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        repositoryMock.ReadByIdAsync(1).Returns(toDoItem);


        // Act
        var result = await controller.ReadById(toDoItem.ToDoItemId);


        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var dto = Assert.IsType<ToDoItemGetResponseDto>(okResult.Value);
        Assert.NotNull(dto);

        Assert.Equal(1, dto.Id);
        Assert.Equal(toDoItem.Name, dto.Name);
        Assert.Equal(toDoItem.Description, dto.Description);
        Assert.Equal(toDoItem.IsCompleted, dto.IsCompleted);



    }
    [Fact]
    public async Task Get_ReadByIdWhenItemsIsNull_ReturnsNotFound()
    { // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns((ToDoItem?)null);

        // Act
        var result = await controller.ReadById(1);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);

        await repositoryMock.Received(1).ReadByIdAsync(1);


    }




}



