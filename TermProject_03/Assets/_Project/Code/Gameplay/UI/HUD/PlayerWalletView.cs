using TMPro;
using UnityEngine;

public class PlayerWalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI walletAmountText;

    private void Awake()
    {
        PlayerWallet.Instance.OnDataChanged += UpdateDisplay;
    }

    private void UpdateDisplay(int amount)
    {
        walletAmountText.text = amount.ToString();
    }
}
