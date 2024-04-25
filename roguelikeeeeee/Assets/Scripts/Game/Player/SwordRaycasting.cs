using UnityEngine;

public class SwordRaycasting : MonoBehaviour
{
    //public Vector2 swordSize;
    public float attackCooldown;
    public float attackSpeed;
    public float swordWeight;
    public int swordDamage;
    public float attackRadius;
    public float attackAngle;
    public float rayCount;

    private SwordMechanic swordMechanicScript;

    public LayerMask enemyLayer;

    private void Awake()
    {
        swordMechanicScript = GetComponentInChildren<SwordMechanic>();
        //swordSize = GameObject.Find("SwordChildren").transform.localScale;
    }
    private void Update()
    {
        swordMechanicScript.attackCooldown = attackCooldown;
        swordMechanicScript.attackSpeed = attackSpeed;
        swordMechanicScript.swordWeight = swordWeight;
    }
    

    public void Raycast()
    {
        Vector2 playerPos = PlayerController.player.transform.position;
        float halfAttackAngle = attackAngle / 2;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = Mathf.Lerp(-halfAttackAngle, halfAttackAngle, i / (rayCount - 1f));

            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;

            RaycastHit2D hit = Physics2D.Raycast(playerPos, direction, attackRadius, enemyLayer);

            if (hit.collider != null)
            {
                Debug.DrawLine(playerPos, hit.point, Color.red);

                if (hit.collider.gameObject.CompareTag("Mob"))
                {
                    Mob mob = hit.collider.gameObject.GetComponent<Mob>();

                    if (mob.onMobDamageCooldownTimer <= 0f)
                    {
                        mob.health -= swordDamage;
                        mob.onMobDamageCooldownTimer = mob.onMobDamageCooldown;

                        GameObject mobHitParticlesPrefabClone = Instantiate(mob.mobHitParticlesPrefab, mob.transform.position, Quaternion.identity);
                        Destroy(mobHitParticlesPrefabClone, mob.hitParticlesDuration);
                    }
                }
            }
            else
            {
                Debug.DrawRay(playerPos, direction * attackRadius, Color.green);
            }
        }
    }
}
