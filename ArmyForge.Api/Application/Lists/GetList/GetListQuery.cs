using MediatR;

namespace ArmyForge.Api.Application.Lists.GetList;

public record GetListQuery(string Id) : IRequest<GetListResponse>;