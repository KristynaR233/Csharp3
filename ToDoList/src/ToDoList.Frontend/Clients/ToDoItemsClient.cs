using System;
using ToDoList.Domain.DTOs;
using ToDoList.Frontend.Models;

namespace ToDoList.Frontend.Clients;

public class ToDoItemsClient : IToDoItemsClient
{
    private readonly HttpClient httpClient;

    public ToDoItemsClient (HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ToDoItemsView?> ReadItemByIdAsync(int itemId)
    {

        var response = await httpClient.GetFromJsonAsync<List<ToDoItemGetResponseDto>>($"api/ToDoItem{itemId}");

        var toDoItem = new ToDoItemsView(Id = response.Id, Name = response.Name, Description = response.Description, IsCompleted = response.IsCompleted);

        return toDoItem;
    }
}
