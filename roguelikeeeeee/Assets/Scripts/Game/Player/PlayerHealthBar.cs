using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{

    private void Update()
    {
        if (!PauseControls.pauseIsOn)
        {
            PlayerController.player.imageHealthBar.fillAmount = PlayerController.player.health; //health bar = hp
            if (PlayerController.player.health <= 1)
                PlayerController.player.health += Mathf.LerpUnclamped(0.000075f, 0.0001f / 3, Mathf.Clamp01(PlayerController.player.currentSpeed)); //regeneration
                                                                                                                                                    //player.health += 0.0001f;
        }
    }
}
