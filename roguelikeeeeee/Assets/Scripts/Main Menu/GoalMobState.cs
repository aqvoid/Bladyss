using UnityEngine;
using UnityEngine.UI;

public class GoalMobState : MonoBehaviour
{
    private GameObject[] mobs;
    public static bool mobsAreCompleted = false;

    void Update()
    {
        mobs = GameObject.FindGameObjectsWithTag("Mob");
        ChangeTextState();
    }

    private string ChangeTextState()
    {
        Text text = GetComponent<Text>();
        text.text = CheckMobsState() == "Completed"
            ? $"[{CheckMobsState()}] mobs are defeated" 
            : $"[{CheckMobsState()}] defeat {mobs.Length} mobs";
        return text.text;
    }

    public string CheckMobsState()
    {
        bool isAllDefeated = mobs.Length > 0 ? false : true;
        mobsAreCompleted = isAllDefeated;
        return isAllDefeated ? "Completed" : "Uncompleted";
    }


}
