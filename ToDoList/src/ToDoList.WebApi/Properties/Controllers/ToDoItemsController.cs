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

    private readonly IRepositoryAsync<ToDoItem> repository;

    public ToDoItemsController(IRepositoryAsync<ToDoItem> repository)
    {

        this.repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItemGetResponseDto>> Create(ToDoItemCreateRequestDto request) // pouzijeme DTO - Data Transfer Object
    {
        var item = request.ToDomain();

        try
        {
            await repository.CreateAsync(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);

        }

        return CreatedAtAction(nameof(ReadById), new { toDoItemId = item.ToDoItemId }, ToDoItemGetResponseDto.FromDomain(item));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemGetResponseDto>>> Read()
    {
        IEnumerable<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = await repository.ReadAsync();

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
    public async Task<ActionResult<ToDoItemGetResponseDto>> ReadById(int toDoItemId)

    {
        ToDoItem? itemToGet;
        try
        {
            itemToGet = await repository.ReadByIdAsync(toDoItemId);

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
    public async Task<ActionResult> UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var updatedItem = request.ToDomain();
        updatedItem.ToDoItemId = toDoItemId;

        try
        {
            var itemToUpdate = await repository.ReadByIdAsync(toDoItemId);
            if (itemToUpdate is null)
            {
                return NotFound();
            }

            await repository.UpdateByIdAsync(updatedItem);

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }

        return NoContent();

    }


    [HttpDelete("{toDoItemId:int}")]
    public async Task<IActionResult> DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = await repository.ReadByIdAsync(toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound();
            }
            await repository.DeleteByIdAsync(toDoItemId);

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }
        return NoContent();

    }

}

