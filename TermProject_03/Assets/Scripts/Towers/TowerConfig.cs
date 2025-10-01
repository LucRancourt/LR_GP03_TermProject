using UnityEngine;
using UnityEditor;


// CANT WORK FOR THE UPGRADES AND INITIAL PLACEMENT AS SETUP NOW

// Maybe have another class to add to the SO para?
// Tier / Stats / Costs/CanUpgrade / Model
// Name / Icon / Limit / PlacementCost / List of the above for each upgrade tier?



[CustomEditor(typeof(TowerConfig))]
public class TowerConfigCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TowerConfig towerConfig = (TowerConfig)target;


        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        towerConfig.Name = EditorGUILayout.TextField("Name", towerConfig.Name);
        towerConfig.UnitLimit = EditorGUILayout.IntField("Unit Limit", towerConfig.UnitLimit);
        towerConfig.UpgradeTier = EditorGUILayout.IntField("Upgrade Tier", towerConfig.UpgradeTier);
        towerConfig.Model = (GameObject)EditorGUILayout.ObjectField("Tower Model", towerConfig.Model, typeof(GameObject), false);
        towerConfig.Icon = (Sprite)EditorGUILayout.ObjectField("Tower Icon", towerConfig.Icon, typeof(Sprite), false);

        EditorGUILayout.Space(20.0f);


        EditorGUILayout.LabelField("Costs", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        towerConfig.PlacementCost = EditorGUILayout.IntField("Placement Cost", towerConfig.PlacementCost);
        towerConfig.UpgradeCost = EditorGUILayout.IntField("Upgrade Cost", towerConfig.UpgradeCost);
        towerConfig.SellValue = EditorGUILayout.IntField("Sell Value", towerConfig.SellValue);

        EditorGUILayout.Space(20.0f);


        EditorGUILayout.LabelField("Tower Type", EditorStyles.boldLabel);
        towerConfig.Type = (TowerType)EditorGUILayout.EnumPopup(towerConfig.Type);
        EditorGUILayout.Space();

        if (towerConfig.Type == TowerType.Attack || towerConfig.Type == TowerType.Spawn)
            towerConfig.Rate = EditorGUILayout.FloatField("Rate", towerConfig.Rate);

        if (towerConfig.Type == TowerType.Attack || towerConfig.Type == TowerType.Support)
            towerConfig.Rate = EditorGUILayout.FloatField("Range", towerConfig.Range);

        switch (towerConfig.Type)
        {
            case TowerType.Attack:
                towerConfig.Damage = EditorGUILayout.FloatField("Damage", towerConfig.Damage);
                break;

            case TowerType.Spawn:
                towerConfig.SpawnedUnitModel = (GameObject)EditorGUILayout.ObjectField("Spawned Unit Model", towerConfig.SpawnedUnitModel, typeof(GameObject), false);
                towerConfig.SpawnedUnitHealth = EditorGUILayout.FloatField("Spawned Unit Health", towerConfig.SpawnedUnitHealth);
                towerConfig.SpawnedUnitSpeed = EditorGUILayout.FloatField("Spawned Unit Speed", towerConfig.SpawnedUnitSpeed);
                break;

           // case TowerType.Support:
           //     towerConfig.
        }

        if (GUI.changed)
            EditorUtility.SetDirty(towerConfig);
    }
}


[CreateAssetMenu(fileName = "TowerConfig", menuName = "Scriptable Objects/Tower")]
public class TowerConfig : ScriptableObject
{
    // All Towers
    public string Name;
    public int UnitLimit;
    public int UpgradeTier;
    public GameObject Model;
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
    public GameObject SpawnedUnitModel;
    public float SpawnedUnitHealth;
    public float SpawnedUnitSpeed;

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