using UnityEngine;

public class TestCamMov : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints_path1;
    [SerializeField] private GameObject[] waypoints_path2;
    [SerializeField] private GameObject[] menus;
    [SerializeField] private bool isPath1 = true;
    private int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (isPath1)
            {
                if (index >= waypoints_path1.Length - 1)
                {
                    foreach (GameObject menu in menus)
                    {
                        menu.SetActive(true);
                    }

                    return;
                }

                transform.position = waypoints_path1[index].transform.position;
                transform.rotation = waypoints_path1[index].transform.rotation;
            }
            else
            {
                if (index >= waypoints_path2.Length-2)
                {
                    foreach (GameObject menu in menus)
                    {
                        menu.SetActive(true);
                    }

                    return;
                }

                transform.position = waypoints_path2[index].transform.position;
                transform.rotation = waypoints_path2[index].transform.rotation;
            }

            index++;
        }
    }
}
