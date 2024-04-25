#region idk
//using System.Collections.Generic;
//using UnityEngine;

//public class Rooms : MonoBehaviour
//{
//    public GameObject startRoom;
//    public GameObject[] rooms;
//    public int maxRooms;

//    private HashSet<Vector2> generatedNumbers = new HashSet<Vector2>();
//    private List<Vector2> roomPositions = new List<Vector2>();
//    private Vector2 newRoomPos;

//    Vector2[] directions = {
//        new Vector2(0, 42), //up
//        new Vector2(0, -42), //down
//        new Vector2(54, 0), //right
//        new Vector2(-54, 0), //left
//    };

//    private void Start()
//    {
//        GameObject startRoomInstantiated = Instantiate(startRoom, GameObject.Find("Player") ? PlayerController.player.transform.position : transform.position, Quaternion.identity, transform);
//        roomPositions.Add(startRoomInstantiated.transform.position);
//        newRoomPos = startRoomInstantiated.transform.position;
//    }

//    private void Update()
//    {
//        if (roomPositions.Count < maxRooms) GenerateRooms();
//    }

//    private void GenerateRooms()
//    {

//        for (int i = 0; i < directions.Length; i++)
//        {
//            Vector2 rndDir = directions[Random.Range(1, directions.Length + 1)];

//            foreach (Vector2 newRoomPos in newRoomPoss)
//                newRoomPoss = rndDir + rooms.transform.position;

//            if (generatedNumbers.Add(rndDir))
//            {
//                Debug.Log(rndDir);
//            }
//        }

//        if (roomPositions.Count < maxRooms)
//        {
//            GameObject rndRoomsInstantiated = Instantiate(rndRooms(), newRoomPoss.transform.position, Quaternion.identity, transform);
//            roomPositions.Add(rndRoomsInstantiated.transform.position);
//        }
//    }

//    private GameObject rndRooms()
//    {
//        int num = Random.Range(1, Random.Range(2, 10));

//        if (num == 1)
//            return rooms[0];
//        else
//            return rooms[0];

//    }
//}
#endregion
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    //public GameObject startRoom;
    public GameObject bossRoom;
    public GameObject exitRoom;
    public GameObject swordRoom;
    public int maxSwordRooms;
    //public GameObject accRoom;
    public GameObject[] randomRooms;
    public int maxRooms;


    private List<Vector2> roomPositions = new List<Vector2>();
    private List<Vector2> savedRoomPositions = new List<Vector2>();
    private Vector2 curRoomPos;

    [HideInInspector] public Vector2[] directions = {
    new Vector2(0, 42), // up
    new Vector2(0, -42), // down
    new Vector2(54, 0), // right
    new Vector2(-54, 0) // left
    };

    private void Start()
    {
        if (maxSwordRooms == 0) maxRooms = 1;
        GameObject startRoomInstantiated = Instantiate(rndRooms(), Vector2.zero, Quaternion.identity, transform);
        curRoomPos = startRoomInstantiated.transform.position;
        roomPositions.Add(startRoomInstantiated.transform.position);
        savedRoomPositions.Add(startRoomInstantiated.transform.position);
    }

    private void Update()
    {
        if (savedRoomPositions.Count < maxRooms) GenerateRooms();
    }

    private void GenerateRooms()
    {
        #region Method 1 (rejected rooms)
        //Difficult to implement but perfect idea

        //accPos = new List<Vector2>(directions);
        //rejPos = new List<Vector2>(directions);


        //for (int g = 0; g < rnd.Next(0, 5); g++)
        //{
        //    Vector2 rndRejDir = directions[rnd.Next(0, directions.Length)];
        //    if (!roomPositions.Contains(rndRejDir) && !roomPositions.Contains(curRoomPos))
        //    {
        //        rejPos.Add(rndRejDir);
        //        roomPositions.Add(rndRejDir);
        //        //GameObject rejRRoom = Instantiate(rejRoom, rndRejDir, Quaternion.identity, transform);
        //    }
        //}
        //for (int i = 0; i < directions.Length; i++)
        //{
        //    Vector2 accDirection = accPos[i];

        //    if (!savedRoomPositions.Contains(accDirection))
        //    {
        //        accPos.Add(accPos[i]);
        //        roomPositions.Add(curRoomPos);
        //        savedRoomPositions.Add(curRoomPos);
        //        GameObject accRRoom = Instantiate(accRoom, curRoomPos, Quaternion.identity, transform);
        //        curRoomPos += accDirection;
        //    }
        //}

        #endregion

        #region Method 2 (Simple but doubtful)
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
            }
            //Exit Room
            else if (roomPositions.Count == maxRooms - 1)
            {
                roomPositions.Add(curRoomPos);
                Instantiate(exitRoom, curRoomPos, Quaternion.identity, transform);
                savedRoomPositions.Add(curRoomPos);
            }
            //Sword Room
            else if (roomPositions.Count % (maxRooms / maxSwordRooms - 1) == 0)
            {
                roomPositions.Add(curRoomPos);
                Instantiate(swordRoom, curRoomPos, Quaternion.identity, transform);
                savedRoomPositions.Add(curRoomPos);
            }
            //Random Room
            else
            {
                roomPositions.Add(curRoomPos);
                Instantiate(rndRooms(), curRoomPos, Quaternion.identity, transform);
                savedRoomPositions.Add(curRoomPos);
            }
        }
        #endregion
    }

    private GameObject rndRooms()
    {
        int num = Random.Range(0, randomRooms.Length);
        return randomRooms[num];
    }
}
