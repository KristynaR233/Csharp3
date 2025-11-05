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

[Route("api/[controller]")] // localshost:5000/api/ToDoItems
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private static List<ToDoItem> items = [];
    private IActionResult responseDto;

    [HttpPost]
    public ActionResult<ToDoItemCreateRequestDto> Create(ToDoItemCreateRequestDto request) // pouzijeme DTO - Data Transfer Object
    {
        var item = request.ToDomain();

        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);

        }


        return CreatedAtAction(actionName: nameof(ReadById), routeValues: new { ToDoItemId = item.ToDoItemId }, value: ToDoItemGetResponseDto.FromDomain(item));
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        var response = new List<ToDoItemGetResponseDto>();
        try
        {
            foreach (var iresponse in items)
            {
                var i = ToDoItemGetResponseDto.FromDomain(iresponse);
                response.Add(i);
            }


        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);

        }
        return Ok(response);

    }

    [HttpGet("{toDoItemId:int}")]
     public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)

    {      ToDoItemGetResponseDto responseDto;
        try
        {
            var responseID = items.Find(x => x.ToDoItemId.Equals(toDoItemId));
            if (responseID == null)
            {
                return NotFound();
            }
            responseDto = ToDoItemGetResponseDto.FromDomain(responseID);

        }
        catch (FileNotFoundException)
        {
            throw new ArgumentException("Id is not found!");
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }
        return Ok(responseDto);

    }
    [HttpPut("{toDoItemId:int}")]
    public ActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var updatedItem = request.ToDomain();
        try
        {
            var itemToUpdate = items.FindIndex(x => x.ToDoItemId == toDoItemId);
            if (itemToUpdate == -1)
            {
                return NotFound();
            }

            updatedItem.ToDoItemId = toDoItemId;
            items[itemToUpdate] = updatedItem;

            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }
         
    }


    [HttpDelete("{toDoItemId:int}")]
    public ActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = items.Find(x => x.ToDoItemId.Equals(toDoItemId));
            if (items == null)
            {
                return NotFound();
            }
            items.Remove(itemToDelete);
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);//500
        }

    }
    public void AddItemToStorage(ToDoItem item)
    {
        items.Add(item);
    }

    public void ClearStorage()
    {
        items.Clear();
    }
}

