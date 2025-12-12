using System;
using System.Collections.Specialized;
using System.Text;

namespace ToDoList.Frontend.Models;

public class ToDoItemView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public string? Category { get; set; }

}

