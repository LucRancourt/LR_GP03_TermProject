using System;
using UnityEngine;

public class FadeCGroup : MonoBehaviour
{
    public event Action OnFadeComplete;

    private CanvasGroup _cGroup;
    private float _fadeValue;
    private bool _timeToFade;
    private bool _isFadeIn;

    [SerializeField] private bool automaticallyFadeIn = false;
    [SerializeField] private float automaticStartDelay = 3.0f;
    [SerializeField, Range(0, 5.0f)] private float dividerToSlowFade = 3.0f;

    private void Start()
    {
        _cGroup = GetComponentInChildren<CanvasGroup>();

        _fadeValue = 0.0f;
        SetOpacity(_fadeValue);
        _timeToFade = false;

        if (automaticallyFadeIn)
            Invoke("FadeIn", automaticStartDelay);
    }

    public void FadeIn()
    {
        _timeToFade = true;
        _isFadeIn = true;
    }

    public void FadeOut(bool deactivateAfter)
    {
        _timeToFade = true;
        _isFadeIn = false;

        if (deactivateAfter)
            OnFadeComplete += () => _cGroup.gameObject.SetActive(false);
    }

    private void SetOpacity(float alphaValue)
    {
        _cGroup.alpha = Mathf.Clamp01(alphaValue);
    }

    private void Update()
    {
        if (_timeToFade)
        {
            if (_isFadeIn)
            {
                _fadeValue += Time.deltaTime / dividerToSlowFade;
                SetOpacity(_fadeValue);

                if (_fadeValue >= 1.0f)
                {
                    _timeToFade = false;
                    OnFadeComplete?.Invoke();
                }
            }
            else
            {
                _fadeValue -= Time.deltaTime / dividerToSlowFade;
                SetOpacity(_fadeValue);

                if (_fadeValue <= 0.0f)
                {
                    _timeToFade = false;
                    OnFadeComplete?.Invoke();
                }
            }
        }
    }
}