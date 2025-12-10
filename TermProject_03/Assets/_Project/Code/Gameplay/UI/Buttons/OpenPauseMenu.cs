using _Project.Code.Core.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class OpenPauseMenu : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { if (EventBus.Instance) EventBus.Instance.Publish(new PauseInputEvent()); });
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
