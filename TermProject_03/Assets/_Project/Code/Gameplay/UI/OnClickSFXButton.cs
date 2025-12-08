using UnityEngine;
using UnityEngine.UI;

using _Project.Code.Core.Audio;


[RequireComponent(typeof(Button))]
public class OnClickSFXButton : MonoBehaviour
{
    [SerializeField] private AudioCue sfx;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.PlaySound(sfx));
    }
}
