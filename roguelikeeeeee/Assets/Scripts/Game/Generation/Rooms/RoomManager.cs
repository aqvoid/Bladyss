using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private bool playerInside = false;
    private bool isLit = false;
    private List<ToggleTorches> torches = new List<ToggleTorches>();

    private void Awake()
    {
        ToggleTorches torchSystem = GetComponentInChildren<ToggleTorches>();
        if (torchSystem != null)
            Debug.Log("__There is no torches__");
        else
            Debug.Log($"[RoomManager] Комната {gameObject.name} запущена, факела найдены: {torches.Count}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLit) return;

        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log($"[RoomManager] Игрок вошел в {gameObject.name}");
        }

        CheckRoomState();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isLit) return;

        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log($"[RoomManager] Игрок вышел из {gameObject.name}");
        }

        CheckRoomState();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isLit) return;

        CheckRoomState();
    }

    private void CheckRoomState()
    {
        if (isLit) return;

        Collider2D[] roomObjects = Physics2D.OverlapBoxAll(transform.position, new Vector2(52, 40), 0);
        bool hasMobs = false;

        foreach (Collider2D obj in roomObjects)
        {
            if (obj.CompareTag("Mob"))
            {
                hasMobs = true;
                break;
            }
        }

        Debug.Log($"[RoomManager] Проверка комнаты {gameObject.name}: Игрок внутри - {playerInside}, Есть мобы - {hasMobs}");

        if (playerInside && !hasMobs)
        {
            isLit = true;
            Debug.Log($"[RoomManager] Факела включены в {gameObject.name}");
            foreach (var torch in torches)
            {
                torch.TryLightTorches();
            }
        }
    }
}
