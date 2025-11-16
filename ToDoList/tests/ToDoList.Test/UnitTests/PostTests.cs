using System;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.tests.UnitTests
{
public class PostTests
{

    [Fact]
    public void Post_ValidRequest_ReturnNewItem()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>();
        var controller = new ToDoItemsController(null, repositoryMock);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request);
        var value = result.GetValue();


        // Assert
        Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.Name, value.Name);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
    }




}
    }
