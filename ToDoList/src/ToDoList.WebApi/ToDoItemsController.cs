namespace ToDoList.WebApi;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using System;
using ToDoList.Persistence;
using Humanizer;
using ToDoList.Persistence.Repositories;

[Route("api/[controller]")] // localshost:5000/api/ToDoItems
[ApiController]
public class ToDoItemsController : ControllerBase
{

    private readonly IRepository<ToDoItem> repository;

    public ToDoItemsController(IRepository<ToDoItem> repository)
    {

        this.repository = repository;
    }

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request) // pouzijeme DTO - Data Transfer Object
    {
        var item = request.ToDomain();

        try
        {
            repository.Create(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);

        }

        return CreatedAtAction(nameof(ReadById), new { toDoItemId = item.ToDoItemId }, ToDoItemGetResponseDto.FromDomain(item));
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        IEnumerable<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = repository.Read();

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);

        }

        return (itemsToGet is null)
        ? NotFound()
        : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain));

    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)

    {
        ToDoItem? itemToGet;
        try
        {
            itemToGet = repository.ReadById(toDoItemId);

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }
        return (itemToGet is null)
        ? NotFound()
        : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet));


    }
    [HttpPut("{toDoItemId:int}")]
    public ActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var updatedItem = request.ToDomain();
        updatedItem.ToDoItemId = toDoItemId;

        try
        {
            var itemToUpdate = repository.ReadById(toDoItemId);
            if (itemToUpdate is null)
            {
                return NotFound();
            }

            repository.UpdateById(updatedItem);

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }

        return NoContent();

    }


    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = repository.ReadById(toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound();
            }
            repository.DeleteById(toDoItemId);

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }
        return NoContent();

    }

}

