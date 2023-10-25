using Application.Common.Models;
using Application.TodoItems.Commands.CreateTodoItem;
using Application.TodoItems.Commands.DeleteTodoItem;
using Application.TodoItems.Commands.UpdateTodoItem;
using Application.TodoItems.Commands.UpdateTodoItemDetail;
using Application.TodoItems.Queries.GetTodoItemsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace dotUpgradeCQRS.MinimalApi;

public static class TodoItemsEndpoints
{
    private static async Task<IResult> GetTodoItemsWithPagination([AsParameters] GetTodoItemsWithPaginationQuery query,
        IMediator mediator)
    {
        var result = await mediator.Send(query);

        return Results.Ok(result);
    }

    private static async Task<IResult> CreateTodoItem([FromBody] CreateTodoItemCommand command,
        IMediator mediator)
    {
        var result = await mediator.Send(command);

        return Results.Ok(result);
    }

    private static async Task<IResult> UpdateTodoItem([FromRoute] int id, [FromBody] UpdateTodoItemCommand command,
        IMediator mediator)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }
        
        await mediator.Send(command);
        return Results.NoContent();
    }
    
    private static async Task<IResult> UpdateTodoItemDetail([FromRoute] int id, [FromBody] UpdateTodoItemDetailCommand command,
        IMediator mediator)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }
        
        await mediator.Send(command);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteTodoItem([FromRoute] int id, IMediator mediator)
    {
        await mediator.Send(new DeleteTodoItemCommand(id));
        return Results.NoContent();
    }
    
    public static void AddTodoItemsEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/todoitems", GetTodoItemsWithPagination)
            .Produces<PaginatedList<TodoItemBriefDto>>()
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

        routeBuilder.MapPost("/todoitems", CreateTodoItem)
            .WithOpenApi();

        routeBuilder.MapPut("/todoitems/{id}", UpdateTodoItem)
            .WithOpenApi();

        routeBuilder.MapPut("/todoitems/UpdateDetail/{id}", UpdateTodoItemDetail)
            .WithOpenApi();

        routeBuilder.MapDelete("/todoitems/{id}", DeleteTodoItem)
            .WithOpenApi();
    }
}