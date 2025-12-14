using _Project.Code.Core.Events;
using _Project.Code.Core.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class OpenPauseMenu : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        { 
            if (ServiceLocator.TryGet(out EventBus bus))
                bus.Publish(new PauseInputEvent()); 
        });
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
