using System.Collections.Generic;

public class TowerGroup : ITower
{
    private List<ITower> _towers = new();

    public void Add(ITower tower) => _towers.Add(tower);
    public void Remove(ITower tower) => _towers.Remove(tower);

    public void ShowVisuals()
    {
        foreach (ITower tower in _towers)
        {
            tower.ShowVisuals();
        }
    }

    public void HideVisuals()
    {
        foreach (ITower tower in _towers)
        {
            tower.HideVisuals();
        }
    }
}