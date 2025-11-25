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

    public async Task<List<ToDoItemsView>> ReadItemsAsync()
    {
        var toDoItemsViews = new List<ToDoItemsView>();
        var response = await httpClient.GetFromJsonAsync<List<ToDoItemGetResponseDto>>("api/ToDoItems");

        toDoItemsViews = response.Select(dto => new ToDoItemsView(dto.Id, dto.Name, dto.Description, dto.IsCompleted)).ToList();

        return toDoItemsViews;
    }
}
