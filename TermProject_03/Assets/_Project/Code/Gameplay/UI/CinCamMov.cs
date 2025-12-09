using _Project.Code.Core.General;
using _Project.Code.Core.ServiceLocator;
using UnityEngine;

public class CinCamMov : Singleton<CinCamMov>
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private CafeMenuTween[] menus;

    [SerializeField] private Animator cafeDoor;
    [SerializeField] private Animator levelSelect;

    private int index = 0;


    public void NextCamPos()
    {
        if (index >= waypoints.Length - 1)
        {
            ClearListenersOnMenus();

            menus[0].OnTransformEnd += menus[1].PlayForward;
            menus[1].OnTransformEnd += () => { DisplayLevelSelect(); ClearListenersOnMenus(); } ;
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
            Invoke("NextCamPos", 2.0f);
        
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
        menus[1].OnTransformEnd -= () => { DisplayLevelSelect(); ClearListenersOnMenus(); };

        menus[1].OnTransformEnd -= menus[0].PlayBackward;
        menus[1].OnTransformEnd -= StartFade;
        menus[0].OnTransformEnd -= ClearListenersOnMenus;
    }

    private void DisplayLevelSelect()
    {
        ServiceLocator.Get<SceneService>().LoadScene("Sandbox");
        //Invoke("ReturnToMainMenu", 5.0f);
        //levelSelect.Show();
    }

    public void ReturnToMainMenu()
    {
        //levelSelect.Hide();
        ClearListenersOnMenus();

        menus[1].OnTransformEnd += menus[0].PlayBackward;
        menus[1].OnTransformEnd += StartFade;
        menus[0].OnTransformEnd += ClearListenersOnMenus;
        menus[1].PlayBackward();

        FadeTo.Instance.OnFadeComplete += ServiceLocator.Get<SceneService>().ReloadCurrentScene;
    }

    private void OnDestroy()
    {
        if (FadeTo.Instance)
            FadeTo.Instance.OnFadeComplete -= ServiceLocator.Get<SceneService>().ReloadCurrentScene;

        ClearListenersOnMenus();
    }
}
