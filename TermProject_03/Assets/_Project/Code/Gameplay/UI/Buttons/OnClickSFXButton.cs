using UnityEngine;
using UnityEngine.UI;

using _Project.Code.Core.Audio;
using _Project.Code.Core.ServiceLocator;

[RequireComponent(typeof(Button))]
public class OnClickSFXButton : MonoBehaviour
{
    [SerializeField] private AudioCue sfx;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ServiceLocator.Get<AudioManager>().PlaySound(sfx));
    }
}
