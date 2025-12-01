using System;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Test;

public static class ActionResultExtensions
{
    public static T? GetValue<T>(this ActionResult<T> result) => result.Result is null
    ? result.Value
    : (T?)(result.Result as ObjectResult)?.Value;

    public static async Task<T?> GetValueAsync<T>(this Task<ActionResult<T>> task)
    {
        var result = await task;
        return result.GetValue<T>();
    }



}

