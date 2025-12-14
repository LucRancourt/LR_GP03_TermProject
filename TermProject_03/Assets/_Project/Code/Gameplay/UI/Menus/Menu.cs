using UnityEngine;
using UnityEngine.UI;

using _Project.Code.Core.Audio;
using _Project.Code.Core.General;
using _Project.Code.Core.ServiceLocator;

public abstract class Menu<T> : Singleton<T> where T : MonoBehaviour
{
    [SerializeField] private AudioCue clickSFX;

    private Button[] _menuButtons;


    protected override void Awake()
    {
        base.Awake();

        _menuButtons = GetComponentsInChildren<Button>(true);

        foreach (Button button in _menuButtons)
        {
            button.onClick.AddListener(PlayClickSFX);
            button.gameObject.AddComponent<TweenedButton>();
        }
    }

    private void PlayClickSFX()
    {
        ServiceLocator.Get<AudioManager>().PlaySound(clickSFX);
    }

    public abstract void OpenMenu();
    public abstract void CloseMenu();
}

public abstract class Menu : MonoBehaviourService
{
    [SerializeField] private AudioCue clickSFX;

    private Button[] _menuButtons;


    protected virtual void Awake()
    {
        _menuButtons = GetComponentsInChildren<Button>(true);

        foreach (Button button in _menuButtons)
        {
            button.onClick.AddListener(PlayClickSFX);
            button.gameObject.AddComponent<TweenedButton>();
        }
    }

    private void PlayClickSFX()
    {
        ServiceLocator.Get<AudioManager>().PlaySound(clickSFX);
    }

    public abstract void OpenMenu();
    public abstract void CloseMenu();
}
