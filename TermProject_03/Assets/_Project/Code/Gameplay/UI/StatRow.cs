using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatRow : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;


    public void SetStatRow(Sprite icon, string amount)
    {
        Show();

        image.sprite = icon;
        text.text = amount;
    }

    public void Show()
    {
        image.enabled = true;
        text.enabled = true;
    }

    public void Hide()
    {
        image.enabled = false;
        text.enabled = false;
    }
}
