using UnityEngine;
using UnityEngine.EventSystems;

using _Project.Code.Core.General;


public class BuilderManager : MonoBehaviour
{
    // Variables
    [SerializeField] private Tower onlyCurrentTower_TempVar;
    [SerializeField] private LayerMask groundLayer;
    private GameObject _towerObject;


    // Sits on per Player? Thought maybe Singleton but then how to detect if currently holding something already?

    // One for Building
    // One for Upgrades
    // Main one for Tower in general?

    // Use Pool somewhere instead of below



    // Functions
    public void OnUIClicked()
    {
        if (_towerObject)
            Destroy(_towerObject);
        else
            _towerObject = Instantiate(onlyCurrentTower_TempVar).gameObject;
    }

    private void Update()
    {
        if (_towerObject)
        {
            Vector3 pos = Input.mousePosition;
            pos.z += 5.0f;
            pos = Camera.main.ScreenToWorldPoint(pos);

            Vector3 dir = MyUtils.GetDirection(pos, Camera.main.transform.position);

            Debug.DrawLine(Camera.main.transform.position, dir * 200.0f);
            if (Physics.Raycast(Camera.main.transform.position, dir, out RaycastHit hit, 2000.0f, groundLayer))
            {
                _towerObject.transform.position = hit.point;
            }
        }
            

        if (Input.GetMouseButtonDown(0) && _towerObject)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Instantiate(_towerObject).transform.position = _towerObject.transform.position;
            Destroy(_towerObject);
        }
    }
}
