using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Sprite[] blockSprites;

    private void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = rndBlock();

        //Checking current position, if in this position already this gameObject = turn him off
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Block"))
            {
                gameObject.SetActive(false);
                //Destroy(gameObject);
                return;
            }
        }
    }

    private Sprite rndBlock()
    {
        int num = Random.Range(0, blockSprites.Length);
        return blockSprites[num];
    }
}
