using UnityEngine;
using UnityEngine.UI;

public class GoalBossState : MonoBehaviour
{
    private GameObject boss;
    public static bool bossIsCompleted = false;

    void Update()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        ChangeTextState();
    }

    private string ChangeTextState()
    {
        Text text = GetComponent<Text>();
        text.text = CheckBossState() == "Completed" 
            ? $"[{CheckBossState()}] boss is defeated" :
            $"[{CheckBossState()}] defeat the floor boss";
        return text.text;
    }

    public string CheckBossState()
    {
        bool isBossDefeated = boss != null ? false : true;
        bossIsCompleted = isBossDefeated;
        return isBossDefeated ? "Completed" : "Uncompleted";
    }
}
