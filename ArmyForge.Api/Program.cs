using ArmyForge.Api.Application.Common;
using ArmyForge.Api.Application.Lists.CreateList;
using ArmyForge.Api.Application.Lists.GetList;
using ArmyForge.Api.Application.Lists.UpdateList;
using ArmyForge.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddCors();
builder.Services.AddScoped<IListRepository, CosmosDbListRepository>();

#if DEBUG
builder.Configuration.AddEnvironmentVariables();
#endif

var app = builder.Build();

var config = app.Services.GetRequiredService<IConfiguration>();

foreach (var c in config.AsEnumerable())
{
  Console.WriteLine(c.Key + " = " + c.Value);
}

app.UseCors(policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowAnyHeader(); });

app.MapGet("/list/{id}", async ([FromServices] IMediator mediator, string id) =>
{
  GetListResponse response = await mediator.Send(new GetListQuery(id));

  if (response.SavedList is not null)
    return Results.Ok(response.SavedList);

  return Results.NotFound();
});

app.MapPost("/list",
  async ([FromServices] IMediator mediator, [FromBody] CreateListCommand request) =>
  {
    CreateListResponse result = await mediator.Send(request);

    return Results.Created("/list/" + result.Id, result.Id);
  });

app.MapPut("/list",
  async ([FromServices] IMediator mediator, [FromBody] UpdateListCommand request) =>
  {
    UpdateListResponse result = await mediator.Send(request);

    return result.Success ? Results.Ok() : Results.BadRequest();
  });

app.Run();