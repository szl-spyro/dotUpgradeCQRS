using Application.TodoLists.Commands.CreateTodoList;
using Application.TodoLists.Commands.DeleteTodoList;
using Application.TodoLists.Commands.UpdateTodoList;
using Application.TodoLists.Queries.GetTodos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace dotUpgradeCQRS.MinimalApi;

public static class TodoLists
{
    private static async Task<TodosVm> GetTodoLists(IMediator mediator)
    {
        return await mediator.Send(new GetTodosQuery());
    }

    private static async Task<int> CreateTodoList([FromBody]CreateTodoListCommand command, IMediator mediator)
    {
        return await mediator.Send(command);
    }

    private static async Task<IResult> UpdateTodoList([FromRoute] int id, [FromBody]UpdateTodoListCommand command, IMediator mediator)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }
        
        await mediator.Send(command);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteTodoList([FromRoute] int id, IMediator mediator)
    {
        await mediator.Send(new DeleteTodoListCommand(id));
        return Results.NoContent();
    }
    
    public static void AddTodoListsEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/todolists", GetTodoLists)
            .WithOpenApi();

        routeBuilder.MapPost("/todolists", CreateTodoList)
            .WithOpenApi();

        routeBuilder.MapPut("/todolists/{id}", UpdateTodoList)
            .WithOpenApi();

        routeBuilder.MapDelete("/todolists/{id}", DeleteTodoList)
            .WithOpenApi();
    }
}