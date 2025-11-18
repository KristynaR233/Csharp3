using System;

namespace ToDoList.Frontend.Models;

public record ToDoItemsView(int Id, string Name, string Description, bool IsCompleted)
{

}
