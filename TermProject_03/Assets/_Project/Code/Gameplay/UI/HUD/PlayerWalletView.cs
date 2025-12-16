using _Project.Code.Core.Audio;
using _Project.Code.Core.ServiceLocator;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class PlayerWalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI walletAmountText;
    [SerializeField] private TextMeshProUGUI walletChangeText;
    [SerializeField] private AudioCue walletIncreaseSFX;

    private Tween _tween;
    private float duration = 0.15f;

    private Color _red = new Color(227.0f / 255.0f, 61.0f / 255.0f, 65.0f / 255.0f);
    private Color _green = new Color(69.0f / 255.0f, 227.0f / 255.0f, 61.0f / 255.0f);

    private void Awake()
    {
        PlayerWallet.Instance.OnNewDataChanged += UpdateDisplay;

        walletChangeText.transform.localScale = Vector3.zero;
    }

    private void UpdateDisplay(int total, int amount, bool wasAdd)
    {
        walletAmountText.text = total.ToString();

        if (wasAdd)
        {
            walletChangeText.text = "+" + amount.ToString();
            walletChangeText.color = _green;

            ServiceLocator.Get<AudioManager>().PlaySound(walletIncreaseSFX);
        }
        else
        {
            walletChangeText.text = "-" + amount.ToString();
            walletChangeText.color = _red;
        }

        Show();
    }

    private void Show()
    {
        StopAllCoroutines();

        walletChangeText.enabled = true;

        _tween?.Kill();
        _tween = walletChangeText.transform.DOScale(0.7f, duration);
        _tween.Play();

        Invoke("Hide", 1.0f);
    }

    private void Hide()
    {
        StopAllCoroutines();

        _tween?.Kill();
        _tween = walletChangeText.transform.DOScale(0.0f, duration);
        _tween.onComplete += HideForReal;
        _tween.Play();
    }

    private void HideForReal()
    {
        walletChangeText.enabled = false;
    }
}
