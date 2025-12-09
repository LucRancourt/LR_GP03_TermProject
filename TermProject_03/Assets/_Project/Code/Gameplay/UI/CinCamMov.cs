using _Project.Code.Core.General;
using UnityEngine;

public class CinCamMov : Singleton<CinCamMov>
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private GameObject[] menus;

    [SerializeField] private Animator cafeDoor;

    private int index = 0;


    public void NextCamPos()
    {
        if (index >= waypoints.Length - 1)
        {
            foreach (GameObject menu in menus)
            {
                menu.SetActive(true);
            }

            return;
        }

        waypoints[index+1].SetActive(true);
        waypoints[index].SetActive(false);

        index++;

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
}
