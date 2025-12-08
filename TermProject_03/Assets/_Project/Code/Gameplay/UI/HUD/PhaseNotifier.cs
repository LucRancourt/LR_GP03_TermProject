using _Project.Code.Core.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PhaseNotifier : BaseView<string>
{
    [SerializeField] private TextMeshProUGUI notificationMessage;
    [SerializeField] private float secondsToDisappear = 5.0f;

    private Tween _tween;
    [Header("Tween Details")]
    [SerializeField] private float duration = 0.25f;


    public override void UpdateDisplay(string message)
    {
        notificationMessage.text = message;

        Show();
    }

    public override void Show()
    {
        if (gameObject.activeSelf)
        {
            Hide();
            Invoke("Show", duration + 0.1f);
            return;
        }

        base.Show();

        _tween?.Kill();
        _tween = transform.DOScale(1.0f, duration);
        _tween.Play();

        Invoke("Hide", secondsToDisappear);
    }

    public override void Hide()
    {
        _tween?.Kill();
        _tween = transform.DOScale(0.0f, duration);
        _tween.onComplete += () => base.Hide();
        _tween.Play();
    }
}
