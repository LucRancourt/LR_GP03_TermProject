using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class TweenedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Tween _tween;
    [Header("Tween Details")]
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float upwardScaleValue = 1.1f;
    [SerializeField] private float downwardScaleValue = 0.9f;

    private Vector3 _initialScale;


    private void Awake()
    {
        _initialScale = transform.localScale;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _tween?.Kill();
        _tween = transform.DOScale(_initialScale * upwardScaleValue, duration).SetUpdate(true);
        _tween.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetToDefault();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _tween?.Kill();
        _tween = transform.DOScale(_initialScale * downwardScaleValue, duration).SetUpdate(true);
        _tween.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetToDefault();
    }

    private void ResetToDefault()
    {
        _tween?.Kill();
        _tween = transform.DOScale(_initialScale, duration).SetUpdate(true);
        _tween.Play();
    }

    private void OnDestroy()
    {
        _tween?.Kill();
    }
}