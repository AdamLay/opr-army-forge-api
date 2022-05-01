using ArmyForge.Api.Domain.Entities;
using MediatR;

namespace ArmyForge.Api.Application.Lists.UpdateList;

public record UpdateListCommand : IRequest<UpdateListResponse>
{
  public string Id { get; init; }
  public string Password { get; init; }
  public SavedList SavedList { get; init; }
}