using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorManager : MonoBehaviour
{
    public GameObject floorPrefab;
    public List<GameObject> floorHistory;
    public static int floorNum = 0;
    internal GameObject currentFloor;
    internal Rooms currentFloorRoomsScript;

    private int totalMaxRooms = 10;
    private int totalMaxSwordRooms = 2;

    private bool canLoad = true; // Флаг для проверки загрузки

    private void Start()
    {
        // Если игра загружается, не создаём новый этаж
        if (canLoad)
        {
            currentFloor = Instantiate(floorPrefab, Vector2.zero, Quaternion.identity, transform);
            floorHistory.Add(currentFloor);

            currentFloorRoomsScript = currentFloor.GetComponent<Rooms>();

            currentFloorRoomsScript.maxRooms = totalMaxRooms;
            currentFloorRoomsScript.maxSwordRooms = totalMaxSwordRooms;

            GameObject.Find("FloorText").GetComponent<Animator>().Play("FloorText");
            GameObject.Find("FloorTransition").GetComponent<Animator>().Play("FloorTransition");
            GameObject.Find("FloorText").GetComponent<Text>().text = "Floor 1";

        }
        canLoad = false;
    }
    public void GenerateFloor(int floorNumPar, Vector2 playerPos)
    {
        //playerPos += new Vector2(500f, 500f);
        if (floorNumPar < byte.MaxValue - Random.Range(1, Mathf.Lerp(25, 100, totalMaxRooms)))
        {
            currentFloor = Instantiate(floorPrefab, playerPos, Quaternion.identity, transform);
            currentFloor.name = "Floor " + floorNumPar.ToString();

            currentFloorRoomsScript = currentFloor.GetComponent<Rooms>();

            totalMaxRooms += Random.Range(2, 6); //rnd amount of new rooms; from 2 to 5
            totalMaxSwordRooms += Random.Range(0, 2); //rnd amount of new sword rooms; from 0 to 1

            currentFloorRoomsScript.maxRooms = totalMaxRooms;
            currentFloorRoomsScript.maxSwordRooms = totalMaxSwordRooms;

            floorHistory.Add(currentFloor);
            floorHistory[floorNumPar - 1].SetActive(false);
            //Destroy(floorHistory[floorNumPar - 1]);

            GameObject.Find("FloorText").GetComponent<Animator>().Play("FloorText");
            GameObject.Find("FloorTransition").GetComponent<Animator>().Play("FloorTransition");
            GameObject.Find("FloorText").GetComponent<Text>().text = floorNumPar.ToString($"Floor {floorNumPar + 1}");
        }
    }
}