using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TierTowerData))]
public class TierTowerDataCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TierTowerData towerData = (TierTowerData)target;


        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        towerData.Model = (Tower)EditorGUILayout.ObjectField("Tower Model", towerData.Model, typeof(Tower), false);
        towerData.Icon = (Sprite)EditorGUILayout.ObjectField("Tower Icon", towerData.Icon, typeof(Sprite), false);

        EditorGUILayout.Space(20.0f);


        EditorGUILayout.LabelField("Costs", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        towerData.Cost = EditorGUILayout.IntField("Cost", towerData.Cost);
        towerData.SellValue = EditorGUILayout.IntField("Sell Value", towerData.SellValue);

        EditorGUILayout.Space(20.0f);


        EditorGUILayout.LabelField("Tower Type", EditorStyles.boldLabel);
        towerData.Type = (TowerType)EditorGUILayout.EnumPopup(towerData.Type);
        EditorGUILayout.Space();

        if (towerData.Type == TowerType.Attack || towerData.Type == TowerType.Spawn)
            towerData.Cooldown = EditorGUILayout.FloatField("Cooldown", towerData.Cooldown);

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


[CreateAssetMenu(fileName = "NewTierTowerData", menuName = "Scriptable Objects/Tower/TierData")]
public class TierTowerData : ScriptableObject
{
    public Tower Model;
    public Sprite Icon;

    public int Cost;
    public int SellValue;

    public TowerType Type;


    // Attack / Spawn shared
    public float Cooldown;

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