using UnityEditor;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    //Every transition to next floor, update the view of camera
    //
    //
    //
    //
    //
    //
    public GameObject minimapTexture;
    public GameObject minimapCamera;

    public GameObject fullMinimapTexture;
    public GameObject fullMinimapCamera;

    private void Update()
    {
        minimapCamera.transform.position = PlayerController.player.transform.position + new Vector3(0, 0, minimapCamera.transform.position.z);

        UpdateMinimapCamera(fullMinimapCamera, fullMinimapTexture);
    }

    private void UpdateMinimapCamera(GameObject cameraObject, GameObject textureObject)
    {
        cameraObject.transform.position = PlayerController.player.transform.position + new Vector3(0, 0, cameraObject.transform.position.z);

        Vector3 cameraPos = FindCenterOfTransforms(cameraObject, textureObject) + new Vector3(0, 0, cameraObject.transform.position.z);

        cameraObject.transform.position = cameraPos;
    }

    public Vector3 FindCenterOfTransforms(GameObject cameraObject, GameObject textureObject)
    {
        Transform[] rooms = GameObject.Find("FloorManager").GetComponent<Transform>().GetComponentsInChildren<Transform>();
        Bounds bounds = new Bounds(rooms[0].transform.position, new Vector3(0, 0));

        if (rooms.Length > 0 && PlayerController.player.enabled)
        {
            foreach (Transform room in rooms)
            {
                bounds.Encapsulate(room.transform.position);
            }

            Camera cam = cameraObject.GetComponent<Camera>();
            float offset = bounds.size.x > bounds.size.y ? bounds.size.x : bounds.size.y;
            offset += 25f; 
            cam.orthographicSize = offset / 2f;

            return bounds.center;
        }
        else
            return PlayerController.player.transform.position;
    }
}
