using System.Collections;
using UnityEngine;

public class RoomOpener : MonoBehaviour
{
    public GameObject[] openings;
    public float untilWhen = 10;
    private float time = 0;

    private void Update()
    {
        //отведённое время за которое открываются двери в комнатах
        if (time < untilWhen) //пока время 1-9
        {
            time += Time.deltaTime; //увеличивай время каждый кадр/секунду (не помню)
            OpenTheOpenings();
        }
        else //пока время больше или равно 10 то вот да
        {
            time = untilWhen;
        }
    }

    public void OpenTheOpenings()
    {
        Collider2D[] roomColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(54 * 2, 42 * 2), 0);

        foreach (Collider2D roomCollider in roomColliders)
        {
            if (roomCollider.gameObject.CompareTag("Room") || roomCollider.gameObject.CompareTag("Exit Room") || roomCollider.gameObject.name == "Boss Room(Clone)" && !GameObject.Find("Boss Room(Clone)").GetComponent<BossRoom>().bossIsAlive)
            {
                Vector2 t = transform.position;

                if (t.x == roomCollider.gameObject.transform.position.x)
                {
                    if (t.y < roomCollider.gameObject.transform.position.y && Mathf.Abs(Mathf.Abs(t.y) - Mathf.Abs(roomCollider.gameObject.transform.position.y)) == 42)
                    {
                        openings[0].SetActive(false); // Top Opening
                    }
                    else if (t.y > roomCollider.gameObject.transform.position.y && Mathf.Abs(Mathf.Abs(t.y) - Mathf.Abs(roomCollider.gameObject.transform.position.y)) == 42)
                    {
                        openings[1].SetActive(false); // Bottom Opening
                    }
                }
                if (t.y == roomCollider.gameObject.transform.position.y)
                {
                    if (t.x < roomCollider.gameObject.transform.position.x && Mathf.Abs(Mathf.Abs(t.x) - Mathf.Abs(roomCollider.gameObject.transform.position.x)) == 54)
                    {
                        openings[2].SetActive(false); // Right Opening
                    }
                    else if (t.x > roomCollider.gameObject.transform.position.x && Mathf.Abs(Mathf.Abs(t.x) - Mathf.Abs(roomCollider.gameObject.transform.position.x)) == 54)
                    {
                        openings[3].SetActive(false); // Left Opening
                    }
                }
            }
        }
    }
}
