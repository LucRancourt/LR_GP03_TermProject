using _Project.Code.Core.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class OpenSettingsMenu : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
       { 
           if (ServiceLocator.TryGet(out SettingsMenu settings))
           {
               settings.OpenMenu();
           }
       });
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
