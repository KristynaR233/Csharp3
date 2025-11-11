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

[Route("api/[controller]")] // localshost:5000/api/ToDoItems
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private readonly ToDoItemsContext context;

    public ToDoItemsController(ToDoItemsContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request) // pouzijeme DTO - Data Transfer Object
    {
        var item = request.ToDomain();

        try
        {
            context.ToDoItems.Add(item);
            context.SaveChanges();

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);

        }

        return CreatedAtAction(nameof(ReadById), new { ToDoItemId = item.ToDoItemId }, ToDoItemGetResponseDto.FromDomain(item));
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        List<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = context.ToDoItems.ToList();

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);

        }
        return Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain));

    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)

    {

        try
        {
            var itemToGet = context.ToDoItems.Find(toDoItemId);
            if (itemToGet == null)
            {
                return NotFound();
            }
            var dto = ToDoItemGetResponseDto.FromDomain(itemToGet);
            return Ok(dto);

        }
        catch (FileNotFoundException)
        {
            throw new ArgumentException("Id is not found!");
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }

    }
    [HttpPut("{toDoItemId:int}")]
    public ActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var updatedItem = request.ToDomain();

        try
        {
            var itemToUpdate = context.ToDoItems.Find(toDoItemId);
            if (updatedItem == null)
            {
                return NotFound();
            }
            itemToUpdate.Name = updatedItem.Name;
            itemToUpdate.Description = updatedItem.Description;
            itemToUpdate.IsCompleted = updatedItem.IsCompleted;

            context.SaveChanges();

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }

        return NoContent();

    }


    [HttpDelete("{toDoItemId:int}")]
    public ActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = context.ToDoItems.Find(toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound();
            }
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }
        return NoContent();

    }

}

