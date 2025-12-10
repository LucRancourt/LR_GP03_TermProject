using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(BaseTowerData))]
public class BaseTowerDataCustomEditor : Editor
{
    SerializedProperty tiersPropertyField;


    private void OnEnable()
    {
        tiersPropertyField = serializedObject.FindProperty("TowerTiers");
    }

    public override void OnInspectorGUI()
    {
        BaseTowerData towerData = (BaseTowerData)target;


        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        towerData.Name = EditorGUILayout.TextField("Name", towerData.Name);
        towerData.UnitLimit = EditorGUILayout.IntField("Unit Limit", towerData.UnitLimit);
        towerData.SpaceTaken = EditorGUILayout.FloatField("Space Taken", towerData.SpaceTaken);


        EditorGUILayout.Space(20.0f);


        EditorGUILayout.LabelField("Tower Type", EditorStyles.boldLabel);
        towerData.Type = (TowerType)EditorGUILayout.EnumPopup(towerData.Type);
        EditorGUILayout.Space();


        int oldTierListSize = tiersPropertyField.arraySize;

        EditorGUILayout.PropertyField(tiersPropertyField, true);

        if (tiersPropertyField.arraySize > oldTierListSize)
        {
            SerializedProperty newTierTowerData = tiersPropertyField.GetArrayElementAtIndex(tiersPropertyField.arraySize - 1);
            newTierTowerData.objectReferenceValue = null;
        }

        CheckTiersList(towerData);

        /*if (towerData.Type == TowerType.Attack)
            towerData.Projectile = (BaseProjectile)EditorGUILayout.ObjectField("Projectile", towerData.Projectile, typeof(BaseProjectile), false);*/

        if (GUI.changed)
            EditorUtility.SetDirty(towerData);

        serializedObject.ApplyModifiedProperties();
    }

    private void CheckTiersList(BaseTowerData towerData)
    {
        HashSet<TierTowerData> inList = new HashSet<TierTowerData>();

        for (int i = 0; i < tiersPropertyField.arraySize; i++)
        {
            SerializedProperty tierProp = tiersPropertyField.GetArrayElementAtIndex(i);
            TierTowerData tier = tierProp.objectReferenceValue as TierTowerData;

            if (tier == null)
            {
                EditorGUILayout.HelpBox("Missing TowerTierData!", MessageType.Error);
                continue;
            }

            if (!inList.Add(tier))
            {
                tiersPropertyField.DeleteArrayElementAtIndex(i);
                continue;
            }

            if (tier.Type != towerData.Type)
            {
                EditorGUILayout.HelpBox($"{tier.name} Type does not match {towerData.Name} Type!", MessageType.Error);
            }
        }
    }
}
#endif

[CreateAssetMenu(fileName = "NewBaseTowerData", menuName = "Scriptable Objects/Tower/BaseData")]
public class BaseTowerData : ScriptableObject
{
    public string Name;
    public int UnitLimit;
    public float SpaceTaken;

    public TowerType Type;
    public List<TierTowerData> TowerTiers;

    //public BaseProjectile Projectile;


    public TierTowerData GetTowerTierData(int index)
    {
        if (TowerTiers.Count > index && index >= 0)
            return TowerTiers[index];

        return null;
    }

    public bool TryGetTowerTierData(int index, out TierTowerData towerData)
    {
        if (TowerTiers.Count > index && index >= 0)
        {
            towerData = TowerTiers[index];
            return true;
        }

        towerData = null;
        return false;
    }

    public Sprite GetDefaultIcon()
    {
        if (TryGetTowerTierData(0, out TierTowerData towerData))
        {
            return towerData.Icon;
        }

        Debug.LogError("No Tier 1 exists!");
        return null;
    }

    public int GetPlacementCost()
    {
        if (TryGetTowerTierData(0, out TierTowerData towerData))
        {
            return towerData.Cost;
        }

        Debug.LogError("No Tier 1 exists!");
        return -1;
    }
}


public enum TowerType
{
    Attack,
    Spawn,
    Support // Money Maker +
}