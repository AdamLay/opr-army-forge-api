using ArmyForge.Api.Application.Common;
using ArmyForge.Api.Domain.Entities;
using MediatR;

namespace ArmyForge.Api.Application.Lists.UpdateList;

public class UpdateListCommandHandler : IRequestHandler<UpdateListCommand, UpdateListResponse>
{
  private readonly IListRepository _listRepository;

  public UpdateListCommandHandler(IListRepository listRepository)
  {
    _listRepository = listRepository;
  }

  public async Task<UpdateListResponse> Handle(UpdateListCommand command, CancellationToken cancellationToken)
  {
    ArmyForgeList? record = await _listRepository
      .GetAsync(command.Id);

    // Fail if list by this ID not found
    if (record is null || string.IsNullOrEmpty(record.PasswordHash))
      return new UpdateListResponse(false);

    // Fail is password doesn't match
    if (!BCrypt.Net.BCrypt.Verify(command.Password, record.PasswordHash))
      return new UpdateListResponse(false);
    
    await _listRepository.SetAsync(new ArmyForgeList
    {
      Id = record.Id,
      PasswordHash = record.PasswordHash,
      SavedList = command.SavedList
    });

    return new UpdateListResponse(true);
  }
}