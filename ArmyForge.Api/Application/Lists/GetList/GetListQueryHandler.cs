using ArmyForge.Api.Application.Common;
using ArmyForge.Api.Domain.Entities;
using MediatR;

namespace ArmyForge.Api.Application.Lists.GetList;

public class GetListQueryHandler : IRequestHandler<GetListQuery, GetListResponse>
{
  private readonly IListRepository _listRepository;

  public GetListQueryHandler(IListRepository listRepository)
  {
    _listRepository = listRepository;
  }
  
  public async Task<GetListResponse> Handle(GetListQuery request, CancellationToken cancellationToken)
  {
    ArmyForgeList? list = await _listRepository.GetAsync(request.Id);

    return new GetListResponse(list?.SavedList);
  }
}