using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : Menu
{
    [Header("General")]
    [SerializeField] private GameObject panel;
    private Vector3 _originalScale;

    [Header("Buttons")]
    [SerializeField] private Button closeButton;

    private Tween _tween;
    [Header("Tween Details")]
    [SerializeField] private float duration = 0.25f;


    private void Start()
    {
        closeButton.onClick.AddListener(() => { CloseMenu(); });

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
        closeButton.onClick.RemoveAllListeners();
    }
}
