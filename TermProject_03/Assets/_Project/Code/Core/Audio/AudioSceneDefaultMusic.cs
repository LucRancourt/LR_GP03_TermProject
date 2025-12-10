using UnityEngine;

namespace _Project.Code.Core.Audio
{
    public class AudioSceneDefaultMusic : MonoBehaviour
    {
        [SerializeField] private AudioCue music;

        
        private void Start()
        {
            if (music)
                AudioManager.Instance.PlayMusic(music);
            else
                Debug.Log("Missing Music!");
        }
    }
}