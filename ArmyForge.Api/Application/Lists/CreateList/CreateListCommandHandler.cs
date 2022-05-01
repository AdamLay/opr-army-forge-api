using ArmyForge.Api.Application.Common;
using ArmyForge.Api.Domain.Entities;
using MediatR;

namespace ArmyForge.Api.Application.Lists.CreateList;

public class CreateListCommandHandler : IRequestHandler<CreateListCommand, CreateListResponse>
{
  private readonly IListRepository _listRepository;

  public CreateListCommandHandler(IListRepository listRepository)
  {
    _listRepository = listRepository;
  }

  public async Task<CreateListResponse> Handle(CreateListCommand command, CancellationToken cancellationToken)
  {
    string id = await Nanoid.Nanoid.GenerateAsync(size: 6);

    await _listRepository.SetAsync(new ArmyForgeList
    {
      Id = id,
      PasswordHash = command.Password is null
        ? null
        // Low work factor to save resources, doesn't need to be that secure
        : BCrypt.Net.BCrypt.HashPassword(command.Password, 4),
      SavedList = command.SavedList
    });

    return new CreateListResponse(id);
  }
}