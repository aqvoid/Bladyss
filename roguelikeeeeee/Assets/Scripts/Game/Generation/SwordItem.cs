using UnityEngine;
using UnityEngine.UI;

public class SwordItem : MonoBehaviour
{
    public Sprite hotbarSprite;
    public Sprite swordHotbarSprite;
    public GameObject spawnedSword;
    [HideInInspector] public Image hotbarImage;

    private void Awake()
    {
        hotbarImage = GameObject.Find("SwordOnHotbar").GetComponent<Image>();
        PlayerController.player.moveSpeedWithoutSword = PlayerController.player.maxSpeed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!PlayerController.player.swordEquipped && Input.GetKey(KeyCode.F))
            {
                Instantiate(spawnedSword, other.transform.position, Quaternion.identity, other.transform);

                //slowing down the movement speed because of picking up sword
                PlayerController.player.moveSpeedWithSword = Mathf.Abs(PlayerController.player.moveSpeedWithoutSword - GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordRaycasting>().swordWeight);
                PlayerController.player.maxSpeed = PlayerController.player.moveSpeedWithSword;
                PlayerSkills.lastMoveSpeed = PlayerController.player.moveSpeedWithSword;

                PlayerController.player.swordEquipped = true;
                hotbarImage.sprite = swordHotbarSprite;
                Destroy(gameObject);
            }
        }
    }
}
