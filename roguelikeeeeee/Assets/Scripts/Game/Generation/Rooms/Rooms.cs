using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    public GameObject bossRoom;
    public GameObject exitRoom;
    public GameObject swordRoom;
    public GameObject startRoom;
    public GameObject[] randomRooms;
    internal int maxRooms = 10;
    internal int maxSwordRooms = 2;
    private int currentSwordRooms = 0;

    private List<Vector2> roomPositions = new List<Vector2>();
    internal List<Vector2> savedRoomPositions = new List<Vector2>();
    private Vector2 curRoomPos;
    private Dictionary<Vector2, string> roomTypes = new Dictionary<Vector2, string>();

    private bool isNewFloor = true;


    [HideInInspector] public Vector2[] directions = {
    new Vector2(0, 42), // up
    new Vector2(0, -42), // down
    new Vector2(54, 0), // right
    new Vector2(-54, 0) // left
    };

    private void Start()
    {
        if (maxSwordRooms < 1) maxSwordRooms = 1;

        if (isNewFloor)
        {
            GameObject startRoomInstantiated = Instantiate(startRoom, startRoom.gameObject.transform.position, Quaternion.identity, transform);
            startRoom.gameObject.transform.position += new Vector3(1000f, 1000f);
            curRoomPos = startRoomInstantiated.transform.position;
            roomPositions.Add(startRoomInstantiated.transform.position);
            savedRoomPositions.Add(startRoomInstantiated.transform.position);
            PlayerController.player.transform.position = startRoomInstantiated.transform.position;
            //Debug.Log("Start Room Created at " + curRoomPos);
        }
    }

    private void Update()
    {
        if (savedRoomPositions.Count < maxRooms) GenerateRooms();
    }

    private void GenerateRooms()
    {
        #region Method "Simple"
        //Pros:
        //+ Very Simple
        //Cons:
        //- Difficult to implement sequental doors opening algorithm

        Vector2 rndDir = directions[Random.Range(0, directions.Length)];
        curRoomPos += rndDir;

        if (!roomPositions.Contains(curRoomPos))
        {
            //Boss Room
            if (roomPositions.Count == maxRooms - 2)
            {
                roomPositions.Add(curRoomPos);
                Instantiate(bossRoom, curRoomPos, Quaternion.identity, transform);
                savedRoomPositions.Add(curRoomPos);
                roomTypes[curRoomPos] = "BossRoom";
            }
            //Exit Room
            else if (roomPositions.Count == maxRooms - 1)
            {
                roomPositions.Add(curRoomPos);
                Instantiate(exitRoom, curRoomPos, Quaternion.identity, transform);
                savedRoomPositions.Add(curRoomPos);
                roomTypes[curRoomPos] = "ExitRoom";
            }
            //Sword Room
            else if (currentSwordRooms < maxSwordRooms && Random.value < Random.Range(0f, 1f))
            {
                roomPositions.Add(curRoomPos);
                Instantiate(swordRoom, curRoomPos, Quaternion.identity, transform);
                savedRoomPositions.Add(curRoomPos);
                roomTypes[curRoomPos] = "SwordRoom";
                currentSwordRooms++;
            }
            //Random Room
            else
            {
                roomPositions.Add(curRoomPos);
                Instantiate(rndRooms(), curRoomPos, Quaternion.identity, transform);
                savedRoomPositions.Add(curRoomPos);
                roomTypes[curRoomPos] = "RandomRoom";
            }
        }
        #endregion
    }

    private GameObject rndRooms()
    {
        int num = Random.Range(0, randomRooms.Length);
        return randomRooms[num];
    }

    public void RegenerateRooms()
    {
        isNewFloor = false;

        // Очищаем старые комнаты
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("StartRoom")) continue; // Не удаляем стартовую комнату
            Destroy(child.gameObject);
        }

        // Восстанавливаем комнаты по сохранённым данным
        foreach (var roomPos in savedRoomPositions)
        {
            if (roomPos == savedRoomPositions[0]) // Стартовая комната уже есть
                continue;

            if (roomTypes.ContainsKey(roomPos))
            {
                string roomType = roomTypes[roomPos];

                if (roomType == "BossRoom")
                    Instantiate(bossRoom, roomPos, Quaternion.identity, transform);
                else if (roomType == "ExitRoom")
                    Instantiate(exitRoom, roomPos, Quaternion.identity, transform);
                else if (roomType == "SwordRoom")
                    Instantiate(swordRoom, roomPos, Quaternion.identity, transform);
                else if (roomType == "RandomRoom")
                    Instantiate(randomRooms[Random.Range(0, randomRooms.Length)], roomPos, Quaternion.identity, transform);
            }
        }
    }
}
