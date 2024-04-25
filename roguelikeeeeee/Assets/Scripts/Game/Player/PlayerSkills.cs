using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    static public float lastMoveSpeed;
    private GameObject playerMoveTrails;

    private void Awake()
    {
        playerMoveTrails = GameObject.Find("PlayerMoveTrails");
    }
    private void Start()
    {
        lastMoveSpeed = PlayerController.player.initialStaticMoveSpeed;
    }

    private void Update()
    {
        if (!PauseControls.pauseIsOn)
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (Input.GetKey(KeyCode.Space) && !PlayerController.player.isDashing) PlayerController.player.isDashing = true;

        if (PlayerController.player.isDashing)
        {
            if (PlayerController.player.dashTime > 0)
            {
                PlayerController.player.initialStaticMoveSpeed = lastMoveSpeed * 2;
                PlayerController.player.dashTime -= Time.deltaTime;
                
                if (PlayerController.player.dashTime <= 1f/3f)
                    playerMoveTrails.SetActive(true);

                if (PlayerController.player.dashTime <= 0)
                    PlayerController.player.initialStaticMoveSpeed = lastMoveSpeed;
            }

            PlayerController.player.imageDashCooldown.fillAmount += 1 / PlayerController.player.dashCooldown * Time.deltaTime;
            if (PlayerController.player.imageDashCooldown.fillAmount >= 1)
            {
                PlayerController.player.imageDashCooldown.fillAmount = 0;
                PlayerController.player.isDashing = false;
            }
        }
        if (!PlayerController.player.isDashing) { PlayerController.player.dashTime = 1f / 3f; playerMoveTrails.SetActive(false); }
    }
}
