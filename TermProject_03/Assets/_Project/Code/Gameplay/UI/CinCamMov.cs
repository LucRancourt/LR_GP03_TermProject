using _Project.Code.Core.General;
using _Project.Code.Core.ServiceLocator;
using System;
using UnityEngine;

public class CinCamMov : Singleton<CinCamMov>
{
    [Header("CamTrack Details")]
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private CafeMenuTween[] menus;

    [Header("Animators")]
    [SerializeField] private Animator cafeDoor;
    [SerializeField] private Animator levelSelect;

    private int index = 0;

    private Action _handlerReference;


    private void Start()
    {
        LevelSelectMenu.Instance.OnBackedOut += ReturnToMainMenu;

        _handlerReference = () => { DisplayLevelSelect(); ClearListenersOnMenus(); };
    }

    public void NextCamPos()
    {
        if (index >= waypoints.Length - 1)
        {
            ClearListenersOnMenus();

            menus[0].OnTransformEnd += menus[1].PlayForward;
            menus[1].OnTransformEnd += _handlerReference;
            menus[0].PlayForward();

            return;
        }

        waypoints[index+1].SetActive(true);
        waypoints[index].SetActive(false);

        index++;

        if (index == 4)
        {
            Invoke("NextCamPos", 3.5f);
            return;
        }

        if (index != 1 && index != 7)
        {
            Invoke("NextCamPos", 2.0f);
        }
        
        if (index == 3)
        {
            cafeDoor.SetTrigger("OpenDoor");
            Invoke("StartFade", 1.0f);
        }
    }

    private void StartFade()
    {
        FadeTo.Instance.Fade(true);
        FadeTo.Instance.OnFadeComplete += UnFade;
    }

    private void UnFade()
    {
        Invoke("RealUnFade", 1.5f);
    }

    private void RealUnFade()
    {
        FadeTo.Instance.Fade(false);
        FadeTo.Instance.OnFadeComplete -= UnFade;
    }

    private void ClearListenersOnMenus()
    {
        menus[0].OnTransformEnd -= menus[1].PlayForward;
        menus[1].OnTransformEnd -= _handlerReference;

        menus[1].OnTransformEnd -= menus[0].PlayBackward;
        menus[1].OnTransformEnd -= StartFade;
        menus[0].OnTransformEnd -= ClearListenersOnMenus;
    }


    private void DisplayLevelSelect()
    {
        LevelSelectMenu.Instance.OpenMenu();
        ClearListenersOnMenus();
    }

    public void ReturnToMainMenu()
    {
        ClearListenersOnMenus();

        menus[1].OnTransformEnd += menus[0].PlayBackward;
        menus[1].OnTransformEnd += StartFade;
        menus[0].OnTransformEnd += ClearListenersOnMenus;
        menus[1].PlayBackward();

        FadeTo.Instance.OnFadeComplete += ServiceLocator.Get<SceneService>().ReloadCurrentScene;
    }

    private void OnDisable()
    {
        if (FadeTo.Instance != null)
            FadeTo.Instance.OnFadeComplete -= ServiceLocator.Get<SceneService>().ReloadCurrentScene;

        ClearListenersOnMenus();

        if (LevelSelectMenu.Instance != null)
            LevelSelectMenu.Instance.OnBackedOut -= ReturnToMainMenu;
    }
}
