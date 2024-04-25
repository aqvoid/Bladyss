using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour
{
    public GameObject[] randomSwords;

    private void Start()
    {
        GameObject sword = Instantiate(rndSword(), transform.position, Quaternion.identity, transform);
    }

    private GameObject rndSword()
    {
        int num = Random.Range(1, 7);

        switch (num)
        {
            case <= 3:
                return randomSwords[0]; 
            case <= 5:
                return randomSwords[1]; 
            case 6:
                return randomSwords[2]; 
            default:
                return randomSwords[0];
        }
    }
}
