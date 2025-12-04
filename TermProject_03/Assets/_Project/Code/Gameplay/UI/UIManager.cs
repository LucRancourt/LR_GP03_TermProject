using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIItem[] uiItems;
    private Dictionary<UIItemKey, GameObject> _uiDictionary = new();


    private void Awake()
    {
        foreach (UIItem item in uiItems)
        {
            if (!_uiDictionary.ContainsKey(item.key))
            {
                _uiDictionary[item.key] = item.uiElement;
                _uiDictionary[item.key].SetActive(false);
            }
            else
                Debug.LogWarning($"Duplicate key [{item.key}]!.");
        }
    }

    public GameObject Get(UIItemKey key)
    {
        if (_uiDictionary.TryGetValue(key, out GameObject UIElement))
            return UIElement;

        Debug.LogError($"No value found for {key}!");
        return null;
    }
}


[System.Serializable]
public class UIItem
{
    public UIItemKey key;
    public GameObject uiElement;
}

public enum UIItemKey
{
    UnitInventory,
    PlayerInventory,
    Timer,
    WaveCounter,
    PlayerWallet,
    WinScreen,
    LossScreen
}