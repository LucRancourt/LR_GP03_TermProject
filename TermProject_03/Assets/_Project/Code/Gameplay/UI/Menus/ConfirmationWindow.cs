using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationWindow : Menu
{
    [Header("General")]
    [SerializeField] private GameObject panel;
    private Vector3 _originalScale;

    [Header("Buttons")]
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button declineButton;

    public event Action OnConfirmed;
    public event Action OnDeclined;

    private Tween _tween;
    [Header("Tween Details")]
    [SerializeField] private float duration = 0.25f;


    private void Start()
    {
        confirmButton.onClick.AddListener(() => { CloseMenu(); OnConfirmed?.Invoke(); });
        declineButton.onClick.AddListener(() => { CloseMenu(); OnDeclined?.Invoke(); });

        _originalScale = panel.transform.localScale;

        panel.transform.localScale = Vector3.zero;
    }

    public override void OpenMenu()
    {
        _tween?.Kill();
        _tween = panel.transform.DOScale(_originalScale, duration).SetUpdate(true);
        _tween.Play();
    }

    public override void CloseMenu()
    {
        _tween?.Kill();
        _tween = panel.transform.DOScale(0.0f, duration).SetUpdate(true); ;
        _tween.Play();
    }

    private void OnDestroy()
    {
        confirmButton.onClick.RemoveAllListeners();
        declineButton.onClick.RemoveAllListeners();
    }
}
