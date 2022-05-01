using ArmyForge.Api.Domain.Entities;
using MediatR;

namespace ArmyForge.Api.Application.Lists.CreateList;

public record CreateListCommand : IRequest<CreateListResponse>
{
  public string? Password { get; init; }
  public SavedList SavedList { get; init; }
}