using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Sprite blockSprite1;
    public Sprite blockSprite2;
    public Sprite blockSprite3;

    private void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = rndBlock();

        //Checking current position, if in this position already this gameObject = turn him off
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Block"))
            {
                //gameObject.SetActive(false);
                Destroy(gameObject);
                return;
            }
        }
    }

    private Sprite rndBlock()
    {
        int num = Random.Range(1, 4);

        if (num == 1) return blockSprite1;
        if (num == 2) return blockSprite2;
        if (num == 3) return blockSprite3;
        else return blockSprite1;
    }
}
