using _Project.Code.Core.MVC;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectedDisplay : BaseView<LevelInfo>
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private Button playButton;

    public event Action<string> OnPlayButtonPressed;


    private void Start()
    {
        Hide();

        playButton.onClick.AddListener(() => OnPlayButtonPressed?.Invoke(nameText.text));
    }

    public override void UpdateDisplay(LevelInfo levelInfo)
    {
        nameText.text = levelInfo.Name;
        difficultyText.text = "Difficulty: " + levelInfo.Difficulty;
        descriptionText.text = levelInfo.Description;

        Show();
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
    }
}
