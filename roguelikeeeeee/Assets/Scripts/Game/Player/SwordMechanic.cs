using FirstGearGames.SmoothCameraShaker;
using UnityEngine;
using UnityEngine.UI;

public class SwordMechanic : MonoBehaviour
{
    private float attackCooldownTimer = 0f;
    private float attackSpeedTimer = 0f;
    private Animator animator;
    private TrailRenderer trail;

    [HideInInspector] public float attackCooldown;
    [HideInInspector] public float attackSpeed;
    [HideInInspector] public float swordWeight; 
    [HideInInspector] public ShakeData shakeEffect;
    [HideInInspector] public SwordItem swordItem;
    //[HideInInspector] public SwordSpawner swordSpawner;

    enum AttackState
    {
        Idle,
        Attack,
        Cooldown
    }
    private AttackState currentAttackState = AttackState.Idle;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
        
    }

    void Update()
    {
        //swordSpawner = GameObject.Find("Sword Spawner").GetComponent<SwordSpawner>();
        if (!PauseControls.pauseIsOn)
        {
            SwordDrop();
            switch (currentAttackState)
            {
                case AttackState.Idle:
                    Idle();
                    break;
                case AttackState.Attack:
                    Attack();
                    break;
                case AttackState.Cooldown:
                    Cooldown();
                    break;
            }
        }
    }

    private void Idle()
    {
        // Check for input to initiate the attack
        if (Input.GetKey(KeyCode.Mouse0) && attackCooldownTimer <= 0 && !PauseControls.pauseIsOn)
        {
            animator.Play("isAttack");
            currentAttackState = AttackState.Attack;
            CameraShakerHandler.Shake(shakeEffect); // Attack Effect

            // Start the attack speed timer
            attackSpeedTimer = attackSpeed;

            trail.enabled = true;
        }
    }

    void Attack()
    {
        float attackTimeNormalized = 1f - (attackSpeedTimer / attackSpeed);
        animator.SetFloat("AttackTimeNormalized", attackTimeNormalized);
        SwordRaycasting swordRaycastingScript = GetComponentInParent<SwordRaycasting>();
        swordRaycastingScript.Raycast();
        PlayerController.player.WeaponInertia();

        if (attackSpeedTimer <= 0f)
        { 
            currentAttackState = AttackState.Cooldown;

            // Start the attack cooldown timer
            attackCooldownTimer = attackCooldown;
        }
        else
        {
            attackSpeedTimer -= Time.deltaTime;
        }
    }

    private void Cooldown()
    {
        animator.Play("isCooldownAttack");
        float cooldownTimeNormalized = 1f - (attackCooldownTimer / attackCooldown);
        animator.SetFloat("CooldownTimeNormalized", cooldownTimeNormalized);

        if (attackCooldownTimer <= 0f)
        {
            currentAttackState = AttackState.Idle;
        }
        else
        {
            attackCooldownTimer -= Time.deltaTime;

            trail.enabled = false;
        }
    }

    void SwordDrop()
    {
        swordItem.hotbarImage = GameObject.Find("SwordOnHotbar").GetComponent<Image>();

        if (PlayerController.player.swordEquipped && Input.GetKey(KeyCode.Q))
        {
            Instantiate(swordItem.gameObject, transform.position, Quaternion.identity);

            //returning the movement speed because of dropping sword
            PlayerController.player.moveSpeedWithoutSword = Mathf.Abs(PlayerController.player.moveSpeedWithSword + GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordRaycasting>().swordWeight);
            PlayerController.player.maxSpeed = PlayerController.player.moveSpeedWithoutSword;
            PlayerSkills.lastMoveSpeed = PlayerController.player.moveSpeedWithoutSword;

            PlayerController.player.swordEquipped = false;
            swordItem.hotbarImage.sprite = swordItem.hotbarSprite;
            Destroy(transform.parent.gameObject);
        }
    }
}


