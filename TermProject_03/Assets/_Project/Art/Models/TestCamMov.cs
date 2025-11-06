using _Project.Code.Core.General;
using UnityEngine;

public class TestCamMov : Singleton<TestCamMov>
{
    [SerializeField] private GameObject[] waypoints_path1;
    //[SerializeField] private GameObject[] waypoints_path2;
    [SerializeField] private GameObject[] menus;
    // [SerializeField] private bool isPath1 = true;
    private int index = 0;


    public void NextCamPos()
    {
        if (index >= waypoints_path1.Length - 1)
        {
            foreach (GameObject menu in menus)
            {
                menu.SetActive(true);
            }

            return;
        }

        waypoints_path1[index+1].SetActive(true);
        waypoints_path1[index].SetActive(false);

        index++;

        if (index != 1 && index != 7)
            Invoke("NextCamPos", 2.0f);
        
        if (index == 3)
        {
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
