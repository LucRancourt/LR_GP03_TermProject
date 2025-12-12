using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatRow : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI secondaryText;


    public void SetStatRow(Sprite icon, string stat1, string stat2 = "")
    {
        if (icon == null)
        {
            Hide();
            return;
        }
            
        Show();

        image.sprite = icon;
        text.text = stat1;

        if (secondaryText != null && stat2 != "")
            secondaryText.text = stat2;
    }

    public void Show()
    {
        image.enabled = true;
        text.enabled = true;

        if (secondaryText) secondaryText.enabled = true;
    }

    public void Hide()
    {
        image.enabled = false;
        text.enabled = false;

        if (secondaryText) secondaryText.enabled = false;
    }
}
