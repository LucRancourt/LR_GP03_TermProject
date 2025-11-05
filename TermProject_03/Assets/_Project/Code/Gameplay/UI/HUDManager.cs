using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private HUDItem[] hudItems;
    private Dictionary<HUDItemKey, GameObject> _hudDictionary = new();


    private void Awake()
    {
        foreach (HUDItem item in hudItems)
        {
            if (!_hudDictionary.ContainsKey(item.key))
            {
                _hudDictionary[item.key] = item.uiElement;
                _hudDictionary[item.key].SetActive(false);
            }
            else
                Debug.LogWarning($"Duplicate key [{item.key}]!.");
        }
    }

    public GameObject Get(HUDItemKey key)
    {
        if (_hudDictionary.TryGetValue(key, out GameObject UIElement))
            return UIElement;

        Debug.LogError($"No value found for {key}!");
        return null;
    }
}

[System.Serializable]
public class HUDItem
{
    public HUDItemKey key;
    public GameObject uiElement;
}

public enum HUDItemKey
{
    UnitInventory,
    PlayerInventory,
    WaveCounter,
    PlayerWallet,
    WinScreen,
    LossScreen
}