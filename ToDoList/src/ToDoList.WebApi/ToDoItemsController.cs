namespace ToDoList.WebApi;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[Route("api/[controller]")] // localshost:5000/api/ToDoItems
[ApiController]
public class ToDoItemsController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request) // pouzijeme DTO - Data Transfer Object
    {
        return Ok();
    }
    [HttpGet]
    public IActionResult Read()
    {
        return Ok();
    }
    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)

    {
        try
        {
            throw new Exception("Neco se opravdu nepovedlo.");
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }
        return Ok();
    }
    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody]ToDoItemUpdateRequestDto request)
    {

        return Ok();
    }
    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        return Ok();
    }
}

