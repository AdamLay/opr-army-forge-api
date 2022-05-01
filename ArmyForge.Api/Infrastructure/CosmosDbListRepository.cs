using ArmyForge.Api.Application.Common;
using ArmyForge.Api.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace ArmyForge.Api.Infrastructure;

public class CosmosDbListRepository : IListRepository
{
  private readonly Container _container;

  public CosmosDbListRepository(IConfiguration configuration)
  {
    var client = new CosmosClient(
      configuration["CosmosDb:EndpointUri"],
      configuration["CosmosDb:PrimaryKey"],
      new CosmosClientOptions() {ApplicationName = "ArmyForgeApi"});

    _container = client.GetContainer(
      configuration["CosmosDb:DatabaseId"],
      configuration["CosmosDb:ContainerId"]);
  }

  public async Task<ArmyForgeList?> GetAsync(string id)
  {
    ItemResponse<ArmyForgeList>? item = await _container
      .ReadItemAsync<ArmyForgeList>(id, new PartitionKey(id));
    
    return item?.Resource;
  }

  public async Task SetAsync(ArmyForgeList list)
  {
    await _container
      .UpsertItemAsync(list, new PartitionKey(list.Id));
  }
}