using UnityEngine;


namespace _Project.Code.Core.Audio
{
    using _Project.Code.Core.ServiceLocator;

    public class AudioSceneDefaultMusic : MonoBehaviour
    {
        [SerializeField] private AudioCue music;

        
        private void Start()
        {
            if (music)
                ServiceLocator.Get<AudioManager>().PlayMusic(music);
            else
                Debug.Log("Missing Music!");
        }
    }
}