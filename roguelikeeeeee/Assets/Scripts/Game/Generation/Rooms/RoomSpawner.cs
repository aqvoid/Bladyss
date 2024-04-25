using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] roomPrefab;
    public GameObject bossRoomPrefab;
    public GameObject swordRoomPrefab;
    public GameObject exitRoomPrefab;
    public int maxRooms = 15;
    public int maxSwordRooms = 3;
    public int roomWidth;
    public int roomHeight;

    private Vector2 spawnPosition;

    List<Vector2> spawnedRooms = new List<Vector2>();

    private void Start()
    {        
        // Generating start/first room if there are no rooms
        if (spawnedRooms.Count == 0)
        {
            GenerateStartingRoom();
        }

        // Generates other rooms with delay of 0.25 seconds
        InvokeRepeating("GenerateRooms", 0.001f, 0.001f);
    }

    private void GenerateStartingRoom()
    {
        // Spawn the starting room at position (0, 0)
        Vector3 playerPos = PlayerController.player.transform.position;

        GameObject room = Instantiate(roomPrefab[0], playerPos, Quaternion.identity, transform);

        spawnPosition = playerPos;
        spawnedRooms.Add(new Vector2(room.transform.position.x, room.transform.position.y));
    }

    private void GenerateRooms()
    {
        if (spawnedRooms.Count >= maxRooms)
        {
            CancelInvoke("GenerateRooms");
            return;
        }

        Vector2[] directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
        List<Vector2> availablePositions = new List<Vector2>();

        foreach (Vector2 direction in directions)
        {
            Vector2 newPosition = spawnPosition + direction * new Vector2(roomWidth, roomHeight);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(newPosition, new Vector2(roomWidth, roomHeight), 0);
            bool canSpawn = true;

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.name == "Block")
                {
                    canSpawn = false;
                    break;
                }
            }

            if (canSpawn && !spawnedRooms.Contains(newPosition))
            {
                availablePositions.Add(newPosition);
            }
        }

        if (availablePositions.Count > 0)
        {
            Vector2 newPosition = availablePositions[Random.Range(0, availablePositions.Count)];

            if (spawnedRooms.Count == maxRooms - 1)
            {
                GameObject bossRoom = Instantiate(bossRoomPrefab, newPosition, Quaternion.identity, transform);
                spawnedRooms.Add(new Vector2(bossRoom.transform.position.x, bossRoom.transform.position.y));
            }
            else if (spawnedRooms.Count == maxRooms - 2)
            {
                GameObject exitRoom = Instantiate(exitRoomPrefab, newPosition, Quaternion.identity, transform);
                spawnedRooms.Add(new Vector2(exitRoom.transform.position.x, exitRoom.transform.position.y));
            }
            else if (spawnedRooms.Count % (maxRooms / maxSwordRooms - 1) == 0)
            {
                GameObject swordRoom = Instantiate(swordRoomPrefab, newPosition, Quaternion.identity, transform);
                spawnedRooms.Add(new Vector2(swordRoom.transform.position.x, swordRoom.transform.position.y));
            }
            else
            {
                GameObject room = Instantiate(rndRoom(), newPosition, Quaternion.identity, transform);
                spawnedRooms.Add(new Vector2(room.transform.position.x, room.transform.position.y));
            }

            spawnPosition = newPosition;
        }
        if (availablePositions.Count == 0)
        {
            // Find the closest available position
            float closestDistance = float.MaxValue;
            Vector2 closestPosition = Vector2.zero;

            foreach (Vector2 position in spawnedRooms)
            {
                foreach (Vector2 direction in directions)
                {
                    Vector2 candidatePosition = position + direction * new Vector2(roomWidth, roomHeight);

                    if (!spawnedRooms.Contains(candidatePosition))
                    {
                        float distance = Vector2.Distance(spawnPosition, candidatePosition);

                        if (distance <= closestDistance)
                        {
                            closestDistance = distance;
                            closestPosition = candidatePosition;
                        }
                    }
                }
            }

            if (closestPosition != Vector2.zero)
            {
                spawnPosition = closestPosition;

                if (spawnedRooms.Count == maxRooms - 1)
                {
                    GameObject bossRoom = Instantiate(bossRoomPrefab, spawnPosition, Quaternion.identity, transform);
                    spawnedRooms.Add(new Vector2(bossRoom.transform.position.x, bossRoom.transform.position.y));
                }
                else if (spawnedRooms.Count == maxRooms - 2)
                {
                    GameObject exitRoom = Instantiate(exitRoomPrefab, spawnPosition, Quaternion.identity, transform);
                    spawnedRooms.Add(new Vector2(exitRoom.transform.position.x, exitRoom.transform.position.y));
                }
                else if (spawnedRooms.Count % (maxRooms / maxSwordRooms) == 0)
                {
                    GameObject swordRoom = Instantiate(swordRoomPrefab, spawnPosition, Quaternion.identity, transform);
                    spawnedRooms.Add(new Vector2(swordRoom.transform.position.x, swordRoom.transform.position.y));
                }
                else
                {
                    GameObject room = Instantiate(rndRoom(), spawnPosition, Quaternion.identity, transform);
                    spawnedRooms.Add(new Vector2(room.transform.position.x, room.transform.position.y));
                }
            }
            else
            {
                Debug.LogWarning("GENERATION ERROR!");
            }
        }
    }

    private GameObject rndRoom()
    {
        int num = Random.Range(1, 9);

        switch (num)
        {
            case <= 3:
                return roomPrefab[0]; //preferably more
            case <= 5:
                return roomPrefab[1]; //preferably more
            //case 8:
            //    return roomPrefab[2];
            //case 7:
            //    return roomPrefab[3];
            //case <= 6:
            //    return roomPrefab[4]; //preferably more
            //case 9:
            //    return roomPrefab[5];
            default:
                return roomPrefab[0];
        }
    }
}
