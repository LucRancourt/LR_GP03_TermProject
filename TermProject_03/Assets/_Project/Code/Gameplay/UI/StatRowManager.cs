using UnityEngine;

public class StatRowManager : MonoBehaviour
{
    [Header("Stat Rows")]
    [SerializeField] private StatRow[] statRows = new StatRow[4];

    [Header("Stat Icons")]
    [SerializeField] private Sprite cooldownIcon;
    [SerializeField] private Sprite rangeIcon;
    [SerializeField] private Sprite damageIcon;


    public void SetupStats(TierTowerData tierTowerData)
    {
        switch (tierTowerData.Type)
        {
            case TowerType.Attack:
                statRows[0].SetStatRow(damageIcon, "Damage: " + tierTowerData.Damage.ToString());
                statRows[1].SetStatRow(cooldownIcon, "Cooldown: " + tierTowerData.Cooldown.ToString() + "s");
                statRows[2].SetStatRow(rangeIcon, "Range: " + tierTowerData.Range.ToString());
                statRows[3].Hide();
                statRows[4].Hide();
                break;

            case TowerType.Spawn:
                //statRows[1].SetStatRow(icon, "Cooldown");
                //towerData.SpawnedUnit = (SpawnedUnit)EditorGUILayout.ObjectField("Spawned Unit", towerData.SpawnedUnit, typeof(SpawnedUnit), false);
                break;

                // case TowerType.Support:
                        //statRows[2].SetStatRow(icon, "Range");
                //     towerData.
        }
    }
}
