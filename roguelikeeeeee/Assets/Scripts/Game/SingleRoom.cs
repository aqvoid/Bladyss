//using System.Collections.Generic;
//using UnityEngine;

//public class SingleRoom : MonoBehaviour
//{
//    //public GameObject[] roomOpenings;
//    public GameObject roomSpawnPoint;
//    private List<Vector2> instantiatedPositions = new List<Vector2>();

//    Vector2[] directions = {
//    new Vector2(0, 42),    // top //opening[0]
//    new Vector2(0, -42),  // bottom //opening[1]
//    new Vector2(54, 0),  // right //opening[2]
//    new Vector2(-54, 0) // left //opening[3]
//    };

//    private void Start()
//    {
//        GenRooms.allInstantiatedPositions.Add(transform.position); //Choose next pos for room, save
//        GenerationAndOpening(); //instantiate room by conditions
//    }

//    void GenerationAndOpening()
//    {
//        //Before/When instantiating - choose directions and instantiate yourself.
//        //Check directions for the rooms to open openings between them. If there is room in direction Top - open opening Top

//        //if (!instantiatedPositions.Contains(transform.position) || !GenRooms.allInstantiatedPositions.Contains(transform.position))
//        //{
//        //    int rndNum = Random.Range(0, roomOpenings.Length + 1);
//        //    for (int i = 0; i < rndNum; i++)
//        //    {
//        //        // Choose a random direction
//        //        int rndDir = Random.Range(1, directions.Length);


//        //        // Disable the opening in the rndDirection
//        //        //roomOpenings[rndDir].SetActive(false);

//        //        // Disable the opening in the negative rndDirection
//        //        #region bebra
//        //        // for ex.: rndDir = 1; rndDir++; rndDir = 2; if top = bottom
//        //        // for ex.: rndDir = 2; rndDir--; rndDir = 1; if bottom = top

//        //        // for ex.: rndDir = 3; rndDir++; rndDir = 4; if right = left
//        //        // for ex.: rndDir = 4; rndDir--; rndDir = 3; if left = right
//        //        #endregion

//        //        roomOpenings[rndDir].SetActive(false); //open random openings
//        //        if (!roomOpenings[rndDir].activeSelf)
//        //        {
//        //            // Calculate the spawn position based on the chosen direction + this position
//        //            Vector2 spawnPosition = directions[rndDir] + (Vector2)transform.position;

//        //            // Check if the spawn position has already been instantiated
//        //            if (!instantiatedPositions.Contains(spawnPosition))
//        //            {
//        //                // Instantiate the room spawn point and add the position to the list
                        
//        //                instantiatedPositions.Add(spawnPosition);
//        //                //GenRooms.allInstantiatedPositions.Add(spawnPosition);
//        //            }
//        //        }
//        //    }
//        //}
//    }
//}
