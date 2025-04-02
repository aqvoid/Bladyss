using UnityEngine;
using UnityEngine.UI;

public class EntitiesState : MonoBehaviour
{
    private Text entitiesState;

    private void Start()
    {
        entitiesState = GetComponent<Text>();
    }
    void Update()
    {
        entitiesState.text = GoalMobState.mobsAreCompleted && GoalBossState.bossIsCompleted ? "You defeated everyone, go to the next floor" : "";
    }
}
