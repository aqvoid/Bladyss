using UnityEngine;

public class VariousFloorBlocks : MonoBehaviour
{
    public Sprite[] floorSprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = rndFloor();
    }

    private Sprite rndFloor()
    {
        int num = Random.Range(0, floorSprites.Length);
        return floorSprites[num];
    }
}
