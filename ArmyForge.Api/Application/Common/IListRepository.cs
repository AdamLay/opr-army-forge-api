using ArmyForge.Api.Domain.Entities;

namespace ArmyForge.Api.Application.Common;

public interface IListRepository
{
  Task<ArmyForgeList?> GetAsync(string id);
  Task SetAsync(ArmyForgeList list);
}