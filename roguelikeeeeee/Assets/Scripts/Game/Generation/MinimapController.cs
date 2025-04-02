using Cinemachine.Utility;
using FirstGearGames.Utilities.Objects;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    //Every transition to next floor, update the view of camera
    //Проблема в том что камера почему то не уменьшается или не приближается ближе к другому центру этажа
    //
    public GameObject minimapTexture;
    public GameObject minimapCamera;

    public GameObject fullMinimapTexture;
    public GameObject fullMinimapCamera;

    //private Rooms roomsScript;

    //private void Start()
    //{
    //    roomsScript = GameObject.FindWithTag("Floor").GetComponent<Rooms>() ? GameObject.FindWithTag("Floor").GetComponent<Rooms>() : null;
    //}

    public string[] roomTags; // Теги для поиска комнат
    public float extraOffset; // Дополнительное смещение для карты

    private void Update()
    {
        UpdateMinimapCamera();
        minimapCamera.transform.position = PlayerController.player.transform.position + new Vector3(0, 0, minimapCamera.transform.position.z);
    }

    private void UpdateMinimapCamera()
    {
        Bounds bounds = CalculateBounds();

        if (bounds.size != Vector3.zero)
        {
            Camera cam = fullMinimapCamera.GetComponent<Camera>();
            cam.orthographicSize = Mathf.Max(bounds.size.x, bounds.size.y) / 2f + extraOffset;

            fullMinimapCamera.transform.position = new Vector3(bounds.center.x, bounds.center.y, fullMinimapCamera.transform.position.z);
        }
    }

    private Bounds CalculateBounds()
    {
        List<Transform> roomTransforms = new List<Transform>();

        foreach (string tag in roomTags)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objectsWithTag)
            {
                roomTransforms.Add(obj.transform);
            }
        }

        if (roomTransforms.Count == 0)
        {
            return new Bounds(Vector3.zero, Vector3.zero);
        }

        Bounds bounds = new Bounds(roomTransforms[0].position, Vector3.zero);
        foreach (Transform room in roomTransforms)
        {
            bounds.Encapsulate(room.position);
        }

        return bounds;
    }
}
