using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

using _Project.Code.Core.ServiceLocator;
using System;

public class LevelSelectMenu : Menu<LevelSelectMenu>
{
    [Header("General")]
    [SerializeField] private CanvasGroup panel;
    [SerializeField] private LevelSelectedDisplay levelSelectDisplay;

    [Header("Buttons")]
    [SerializeField] private Button exitButton;
    private LevelButton[] _levelButtons;

    public event Action OnBackedOut;

    private Tween _tween;
    [Header("Tween Details")]
    [SerializeField] private float duration = 0.25f;


    private void Start()
    {
        panel.alpha = 0.0f;
        panel.interactable = false;
        panel.blocksRaycasts = false;

        levelSelectDisplay.OnPlayButtonPressed += PlayLevel;
        exitButton.onClick.AddListener(() => { CloseMenu(); OnBackedOut?.Invoke(); });

        _levelButtons = GetComponentsInChildren<LevelButton>();

        foreach (LevelButton button in _levelButtons)
        {
            button.OnPressed += levelSelectDisplay.UpdateDisplay;
        }
    }

    private void PlayLevel(string levelName)
    {
        ServiceLocator.Get<SceneService>().LoadScene(levelName.Replace(" ", ""));
    }

    public override void OpenMenu()
    {
        _tween?.Kill();
        _tween = panel.DOFade(1.0f, duration);
        _tween.onComplete += () => { panel.interactable = true; panel.blocksRaycasts = true; };
        _tween.Play();
    }

    public override void CloseMenu()
    {
        panel.interactable = false;
        panel.blocksRaycasts = false;

        levelSelectDisplay.Hide();

        _tween?.Kill();
        _tween = panel.DOFade(0.0f, duration);
        _tween.Play();
    }

    private void OnDestroy()
    {
        levelSelectDisplay.OnPlayButtonPressed -= PlayLevel;
        exitButton.onClick.RemoveAllListeners();

        foreach (LevelButton button in _levelButtons)
        {
            button.OnPressed -= levelSelectDisplay.UpdateDisplay;
        }
    }
}
