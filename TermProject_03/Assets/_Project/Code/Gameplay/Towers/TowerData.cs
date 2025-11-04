using UnityEngine;
using UnityEditor;


// CANT WORK FOR THE UPGRADES AND INITIAL PLACEMENT AS SETUP NOW

// Maybe have another class to add to the SO para?
// Tier / Stats / Costs/CanUpgrade / Model
// Name / Icon / Limit / PlacementCost / List of the above for each upgrade tier?



[CustomEditor(typeof(TowerData))]
public class TowerDataCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TowerData towerData = (TowerData)target;


        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        towerData.Name = EditorGUILayout.TextField("Name", towerData.Name);
        towerData.UnitLimit = EditorGUILayout.IntField("Unit Limit", towerData.UnitLimit);
        towerData.SpaceTaken = EditorGUILayout.FloatField("Space Taken", towerData.SpaceTaken);
        towerData.NextUpgradeTier = (TowerData)EditorGUILayout.ObjectField("Upgrade Tier", towerData.NextUpgradeTier, typeof(TowerData), false);
        towerData.Model = (Tower)EditorGUILayout.ObjectField("Tower Model", towerData.Model, typeof(Tower), false);
        towerData.Icon = (Sprite)EditorGUILayout.ObjectField("Tower Icon", towerData.Icon, typeof(Sprite), false);

        EditorGUILayout.Space(20.0f);


        EditorGUILayout.LabelField("Costs", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        towerData.PlacementCost = EditorGUILayout.IntField("Placement Cost", towerData.PlacementCost);
        towerData.UpgradeCost = EditorGUILayout.IntField("Upgrade Cost", towerData.UpgradeCost);
        towerData.SellValue = EditorGUILayout.IntField("Sell Value", towerData.SellValue);

        EditorGUILayout.Space(20.0f);


        EditorGUILayout.LabelField("Tower Type", EditorStyles.boldLabel);
        towerData.Type = (TowerType)EditorGUILayout.EnumPopup(towerData.Type);
        EditorGUILayout.Space();

        if (towerData.Type == TowerType.Attack || towerData.Type == TowerType.Spawn)
            towerData.Rate = EditorGUILayout.FloatField("Rate", towerData.Rate);

        if (towerData.Type == TowerType.Attack || towerData.Type == TowerType.Support)
            towerData.Range = EditorGUILayout.FloatField("Range", towerData.Range);

        switch (towerData.Type)
        {
            case TowerType.Attack:
                towerData.Damage = EditorGUILayout.FloatField("Damage", towerData.Damage);
                break;

            case TowerType.Spawn:
                towerData.SpawnedUnit = (SpawnedUnit)EditorGUILayout.ObjectField("Spawned Unit", towerData.SpawnedUnit, typeof(SpawnedUnit), false);
                break;

           // case TowerType.Support:
           //     towerData.
        }

        
        if (GUI.changed)
            EditorUtility.SetDirty(towerData);

        serializedObject.ApplyModifiedProperties();
    }
}


[CreateAssetMenu(fileName = "NewTowerData", menuName = "Scriptable Objects/Tower/Data")]
public class TowerData : ScriptableObject
{
    // All Towers
    public string Name;
    public int UnitLimit;
    public float SpaceTaken;
    public TowerData NextUpgradeTier;
    public Tower Model;
    public Sprite Icon;

    public int PlacementCost;
    public int UpgradeCost;
    public int SellValue;


    public TowerType Type;

    // Attack / Spawn shared
    public float Rate;

    // Attack / Support shared
    public float Range;

    // Attack
    public float Damage;

    // Spawn
    public SpawnedUnit SpawnedUnit;

    // Support
    //increased stat
    //reduced stat
}


public enum TowerType
{
    Attack,
    Spawn,
    Support // Money Maker +
}