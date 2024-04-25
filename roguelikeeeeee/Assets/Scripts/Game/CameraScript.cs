using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject objectToFollow;
    public Vector3 offset;
    public float dumping;
    private Vector3 velocity = Vector3.zero;
    private bool minimapIsOn = false;

    private MinimapController minimapControllerScript;

    private void Start()
    {
        minimapControllerScript = GameObject.Find("Minimap Camera").GetComponent<MinimapController>();
    }
    void Update()
    {
        Vector3 cameraFollow = objectToFollow.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, cameraFollow, ref velocity, dumping);

        if (!PauseControls.pauseIsOn)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                minimapControllerScript.minimapTexture.SetActive(minimapIsOn);
                minimapControllerScript.fullMinimapTexture.SetActive(!minimapIsOn);
                minimapControllerScript.minimapCamera.SetActive(minimapIsOn);
                minimapControllerScript.fullMinimapCamera.SetActive(!minimapIsOn);

                minimapIsOn = !minimapIsOn;
            }
        }
    }
}
