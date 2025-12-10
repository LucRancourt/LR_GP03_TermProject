using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class LevelButton : MonoBehaviour
{
    [SerializeField] private LevelInfo levelInfo;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;

    public event Action<LevelInfo> OnPressed;


    private void Start()
    {
        icon.sprite = levelInfo.Icon;
        nameText.text = levelInfo.Name;

        GetComponent<Button>().onClick.AddListener(() => OnPressed?.Invoke(levelInfo));
    }

    public LevelInfo GetLevelInfo() { return levelInfo; }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
