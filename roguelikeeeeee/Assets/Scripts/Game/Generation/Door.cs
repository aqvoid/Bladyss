using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    void Start()
    {
        //Checking current position, if in this position already this gameObject = turn him off
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Door"))
            {
                gameObject.SetActive(false);
                return;
            }
        }
    }
}
