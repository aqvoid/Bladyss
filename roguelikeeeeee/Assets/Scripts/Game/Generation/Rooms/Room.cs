using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] openings;
    RoomSpawner roomSpawnerScript;

    private void Awake()
    {
        roomSpawnerScript = GameObject.FindWithTag("Floor").GetComponent<RoomSpawner>() ? GameObject.FindWithTag("Floor").GetComponent<RoomSpawner>() : null;
    }

    public void Update()
    {
        Collider2D[] roomColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(roomSpawnerScript.roomWidth * 2, roomSpawnerScript.roomHeight * 2), 0);

        foreach (Collider2D roomCollider in roomColliders)
        {
            if (roomCollider.gameObject.CompareTag("Room"))
            {
                Vector2 t = transform.position;

                if (t.x == roomCollider.gameObject.transform.position.x)
                {
                    if (t.y < roomCollider.gameObject.transform.position.y && Mathf.Abs(Mathf.Abs(t.y) - Mathf.Abs(roomCollider.gameObject.transform.position.y)) == roomSpawnerScript.roomHeight)
                    {
                        openings[0].SetActive(false); // Top Opening
                    }
                    else if (t.y > roomCollider.gameObject.transform.position.y && Mathf.Abs(Mathf.Abs(t.y) - Mathf.Abs(roomCollider.gameObject.transform.position.y)) == roomSpawnerScript.roomHeight)
                    {
                        openings[1].SetActive(false); // Bottom Opening
                    }
                }
                if (t.y == roomCollider.gameObject.transform.position.y)
                {
                    if (t.x < roomCollider.gameObject.transform.position.x && Mathf.Abs(Mathf.Abs(t.x) - Mathf.Abs(roomCollider.gameObject.transform.position.x)) == roomSpawnerScript.roomWidth)
                    {
                        openings[2].SetActive(false); // Right Opening
                    }
                    else if (t.x > roomCollider.gameObject.transform.position.x && Mathf.Abs(Mathf.Abs(t.x) - Mathf.Abs(roomCollider.gameObject.transform.position.x)) == roomSpawnerScript.roomWidth)
                    {
                        openings[3].SetActive(false); // Left Opening
                    }
                }
            }
        }
    }
}