using UnityEngine;

public class BossRoom : MonoBehaviour
{
    private const string SAVEDOBJECTSTATESKEY = "SavedObjectStates";
    private bool[] savedObjectStates;
    private RoomOpener singleRoomScript;
    public GameObject swordSpawnerGameObject;

    [HideInInspector] public bool bossIsAlive;

    private void Start()
    {
        singleRoomScript = GetComponent<RoomOpener>();
        //2 seconds after scene start - save openings states 
        Invoke("SaveObjectStates", 4f);
    }

    private void Update()
    {
        bossIsAlive = GameObject.FindGameObjectWithTag("Boss") ? true : false;
    }

    void SaveObjectStates()
    {
        savedObjectStates = new bool[singleRoomScript.openings.Length];

        for (int i = 0; i < singleRoomScript.openings.Length; i++)
        {
            savedObjectStates[i] = singleRoomScript.openings[i].activeSelf;
        }

        for (int i = 0; i < savedObjectStates.Length; i++)
        {
            PlayerPrefs.SetInt($"{SAVEDOBJECTSTATESKEY}_{i}", savedObjectStates[i] ? 1 : 0);
        }

        PlayerPrefs.Save();
    }

    private void LoadObjectStatesFromPlayerPrefs()
    {
        savedObjectStates = new bool[singleRoomScript.openings.Length];

        for (int i = 0; i < savedObjectStates.Length; i++)
        {
            int savedValue = PlayerPrefs.GetInt($"{SAVEDOBJECTSTATESKEY}_{i}", 0);
            savedObjectStates[i] = savedValue == 1;
        }
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        //Change(close openings) when player and boss in room

        if (c.gameObject.CompareTag("Player") && bossIsAlive)
        {
            foreach (GameObject opening in singleRoomScript.openings)
            {
                opening.SetActive(true);
            }

            if (!PlayerController.player.swordEquipped)
                swordSpawnerGameObject.SetActive(true);
            else
                swordSpawnerGameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        //Return(return saved openings states(open openings)) when (player not in room) or (boss not in room) or (player not in room) and (boss not in room) or (boss in room) or (player in room)
        //if (c.gameObject.CompareTag("Player") && bossIsAlive == false || !c.gameObject.CompareTag("Player") && bossIsAlive == true || !c.gameObject.CompareTag("Player") && bossIsAlive == true)
        if (c.gameObject.CompareTag("Player") && !bossIsAlive || !c.gameObject.CompareTag("Player") && bossIsAlive || !c.gameObject.CompareTag("Player") && !bossIsAlive)
        {
            LoadObjectStatesFromPlayerPrefs();
            for (int i = 0; i < singleRoomScript.openings.Length; i++)
            {
                singleRoomScript.openings[i].SetActive(savedObjectStates[i]);
            }
        }
    }
}
