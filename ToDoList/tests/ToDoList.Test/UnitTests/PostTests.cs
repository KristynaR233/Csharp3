using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using ToDoList.Test;

namespace ToDoList.Test.UnitTests
{
    public class PostTests
    {

        [Fact]
        public async Task Post_ValidRequest_ReturnCreatedAtAction()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var request = new ToDoItemCreateRequestDto(
                Name: "Jmeno",
                Description: "Popis",
                IsCompleted: false,
                Category: "Prace"
            );

            // Act
            var result = await controller.Create(request);
            var value = result.GetValue();


            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(value);

            Assert.Equal(request.Description, value.Description);
            Assert.Equal(request.Name, value.Name);
            Assert.Equal(request.IsCompleted, value.IsCompleted);
            Assert.Equal(request.Category, value.Category);
        }





    }
}
