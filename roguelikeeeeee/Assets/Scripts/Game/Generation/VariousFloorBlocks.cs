using UnityEngine;

public class VariousFloorBlocks : MonoBehaviour
{
    public Sprite floorSprite1;
    public Sprite floorSprite2;
    public Sprite floorSprite3;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = rndFloor();
    }

    private Sprite rndFloor()
    {
        int num = Random.Range(1, 4);

        if (num == 1) return floorSprite1;
        if (num == 2) return floorSprite2;
        if (num == 3) return floorSprite3;
        else return floorSprite1;
    }
}
