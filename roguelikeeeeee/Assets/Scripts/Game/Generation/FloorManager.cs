using UnityEngine;
using UnityEngine.UI;

public class FloorManager : MonoBehaviour
{
    public GameObject[] floorPrefab;
    public static int floorNum = 0;
    private GameObject currentFloor;

    private void Start()
    {
        GenerateFloor(0, Vector2.zero);
    }

    public void GenerateFloor(int floorNum, Vector2 playerPos)
    {     
        if (currentFloor != null)
        {
            currentFloor.SetActive(false);
        }

        if (floorNum < floorPrefab.Length)
        {
            currentFloor = Instantiate(floorPrefab[floorNum], playerPos, Quaternion.identity, transform);

            GameObject.Find("FloorText").GetComponent<Animator>().Play("FloorText");
            GameObject.Find("FloorTransition").GetComponent<Animator>().Play("FloorTransition");
            GameObject.Find("FloorText").GetComponent<Text>().text = floorNum.ToString($"Floor {floorNum + 1}");
        }
    }
}
