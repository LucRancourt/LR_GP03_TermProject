using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class OpenSettingsMenu : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { if (SettingsMenu.Instance) SettingsMenu.Instance.OpenMenu(); });
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
