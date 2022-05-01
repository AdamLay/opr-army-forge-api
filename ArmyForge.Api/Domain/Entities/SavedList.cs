namespace ArmyForge.Api.Domain.Entities;

public class SavedList
{
  public string GameSystem { get; set; }
  public string ArmyId { get; set; }
  public string[] ArmyIds { get; set; }
  public string ArmyFaction { get; set; }
  public string ArmyName { get; set; }
  public string Modified { get; set; }
  public int SaveVersion { get; set; }
  public int ListPoints { get; set; }
  public ListState List { get; set; }
}

public class ListState
{
  public string CreationTime { get; set; }
  public string Name { get; set; }
  public int PointsLimit { get; set; }
  public UnitState[] Units { get; set; }
}

public class UnitState
{
  public string Id { get; set; }
  public string ArmyId { get; set; }
  public string SelectionId { get; set; }
  public SelectedUpgrade[] SelectedUpgrades { get; set; }
  public bool Combined { get; set; }
  public string JoinToUnit { get; set; }
}

public class SelectedUpgrade
{
  public string InstanceId { get; set; }
  public string UpgradeId { get; set; }
  public string OptionId { get; set; }
}