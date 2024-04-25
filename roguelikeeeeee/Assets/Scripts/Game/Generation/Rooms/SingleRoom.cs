using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRoom : MonoBehaviour
{
    public GameObject[] openings;
    private bool isRunning = false;

    private void Start()
    {
        StartCoroutine(ExecuteFunctionFor3Seconds());
    }

    private IEnumerator ExecuteFunctionFor3Seconds()
    {
        isRunning = true;
        yield return new WaitForSeconds(1f);
        isRunning = false;
    }

    private void Update()
    {
        if (isRunning)
        {
            OpenTheOpenings();
        }
    }

    void OpenTheOpenings()
    {
        Collider2D[] roomColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(54 * 2, 42 * 2), 0);

        foreach (Collider2D roomCollider in roomColliders)
        {
            if (roomCollider.gameObject.CompareTag("Room") || roomCollider.gameObject.name == "Boss Room(Clone)" && !GameObject.Find("Boss Room(Clone)").GetComponent<BossRoom>().bossIsAlive)
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
