using UnityEngine;

public class SaveFloor : MonoBehaviour
{
    private static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("FloorNum"))
        {
            FloorManager.floorNum = PlayerPrefs.GetInt("FloorNum");
        }
        else
        {
            FloorManager.floorNum = 0; 
        }
    }
}
