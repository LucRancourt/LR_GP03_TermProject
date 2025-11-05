using UnityEngine;

using _Project.Code.Core.General;


public static class CameraToMouseRaycast
{
    public static bool TryRaycast(int layer, out RaycastHit rayHit, float distance = 2000.0f)
    {
        Vector3 pos = Input.mousePosition;
        pos.z += 5.0f;
        pos = Camera.main.ScreenToWorldPoint(pos);

        Vector3 dir = MyUtils.GetDirection(pos, Camera.main.transform.position);

        if (Physics.Raycast(Camera.main.transform.position, dir, out RaycastHit hit, distance, layer))
        {
            rayHit = hit;
            return true;
        }

        rayHit = new RaycastHit();
        return false;
    }

    public static bool TryRaycastWithComponent<T>(int layer, out T componentReturn, float distance = 2000.0f) where T : Component
    {
        Vector3 pos = Input.mousePosition;
        pos.z += 5.0f;
        pos = Camera.main.ScreenToWorldPoint(pos);

        Vector3 dir = MyUtils.GetDirection(pos, Camera.main.transform.position);

        if (Physics.Raycast(Camera.main.transform.position, dir, out RaycastHit hit, distance, layer))
        {
            if (hit.transform.TryGetComponent(out T hitComponent))
            {
                componentReturn = hitComponent;
                return true;
            }
        }

        componentReturn = null;
        return false;
    }
}
