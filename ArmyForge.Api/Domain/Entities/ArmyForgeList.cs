using Newtonsoft.Json;

namespace ArmyForge.Api.Domain.Entities;

public class ArmyForgeList
{
  [JsonProperty(PropertyName = "id")]
  public string Id { get; set; }
  public string? PasswordHash { get; set; }
  public SavedList SavedList { get; set; }
}