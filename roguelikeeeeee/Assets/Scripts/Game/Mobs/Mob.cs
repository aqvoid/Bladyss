using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

public class Mob : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private float wanderTimer;
    private float dashTimer;
    private bool isDashing = false;

    public float farRadius;
    public float closeRadius;

    /* ChasingWithVector2 variables
    public float closeChaseRadius;
    public float farChaseRadius;
    */

    public float attackRadius;
    public float attackAngle;
    public float rayCount;
    public LayerMask wallLayer;

    public float predictionTime = 1f;
    public float predictionFactor = 1.5f;

    public int health;
    public float initialSpeed;
    /* ChasingWithVector2 variables
    public CircleCollider2D mobFarDistance;
    public CircleCollider2D mobCloseDistance;
    */

    [HideInInspector] public float onMobDamageCooldownTimer = 0f;
    [HideInInspector] public float onMobDamageCooldown = 0.3f;
    [HideInInspector] public float deathParticlesDuration;
    [HideInInspector] public GameObject mobDeathParticlesPrefab;
    [HideInInspector] public ParticleSystem mobDeathParticleSystem;
    [HideInInspector] public float hitParticlesDuration;
    [HideInInspector] public GameObject mobHitParticlesPrefab;
    [HideInInspector] public ParticleSystem mobHitParticleSystem;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        /* ChasingWithVector2
        mobFarDistance = GameObject.Find("MobChaseRadius").GetComponent<CircleCollider2D>();
        mobCloseDistance = GameObject.Find("MobChaseRadius").GetComponent<CircleCollider2D>();

        if (mobFarDistance != null)
            Debug.Log(mobFarDistance.radius);
        if (mobCloseDistance != null)
            Debug.Log(mobCloseDistance.radius);

        farChaseRadius = mobFarDistance.radius;
        closeChaseRadius = mobCloseDistance.radius;
        */
        onMobDamageCooldown = 0.5f;

        attackRadius = transform.name.Substring(0, transform.name.Length >= 12 ? 12 : 0) == "Regular Boss" ? attackRadius * 1.5f : attackRadius;
        farRadius = transform.name.Substring(0, transform.name.Length >= 12 ? 12 : 0) == "Regular Boss" ? farRadius * 1.5f : farRadius;
        closeRadius = transform.name.Substring(0, transform.name.Length >= 12 ? 12 : 0) == "Regular Boss" ? closeRadius * 1.25f : closeRadius;
    }

    private void Update()
    {
        if (onMobDamageCooldownTimer > 0f)
        {
            onMobDamageCooldownTimer -= Time.deltaTime;
        }

        if (health <= 0f)
        {
            MobDeath();
        }

        ChasingWithRaycast();
    }

    //void ChasingWithVector2()
    //{
    //    Vector2 mobPos = transform.position;

    //    Vector2 playerVelocity = PlayerController.player.transform.GetComponent<Rigidbody2D>().velocity;
    //    Vector2 predictedPlayerPosition = (Vector2)PlayerController.player.transform.position + playerVelocity * predictionTime * predictionFactor;

    //    Vector2 directionToPredictedPosition = (predictedPlayerPosition - mobPos).normalized;

    //    float angle = Mathf.Atan2(directionToPredictedPosition.y, directionToPredictedPosition.x) * Mathf.Rad2Deg + 90f;

    //    RaycastHit2D hit = Physics2D.Raycast(mobPos, directionToPredictedPosition, farChaseRadius, wallLayer);

    //    if (mobFarDistance.gameObject.CompareTag("Player") && !mobFarDistance.gameObject.CompareTag("Block"))
    //    {
    //        transform.rotation = Quaternion.Euler(0, 0, angle);
    //        rb.velocity = directionToPredictedPosition * initialSpeed;
    //        Debug.DrawLine(mobPos, hit.point, Color.red);

    //        if (mobCloseDistance.gameObject.CompareTag("Player") && !mobCloseDistance.gameObject.CompareTag("Block"))
    //        {
    //            if (!isDashing)
    //            {
    //                dashTimer += Time.deltaTime;

    //                if (dashTimer >= Random.Range(0.25f, 1.25f))
    //                {
    //                    isDashing = true;
    //                    dashTimer = 0f;
    //                    initialSpeed = initialSpeed * 2;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            isDashing = false;
    //            initialSpeed = 4f;
    //        }

    //    }
    //    else
    //    {
    //        wanderTimer -= Time.deltaTime;

    //        if (wanderTimer <= 0f)
    //        {
    //            Wandering();
    //            wanderTimer = Random.Range(0.5f, 2.5f);
    //        }
    //    }
    //}

    void ChasingWithOverlap()
    {
        Vector2 origin = transform.position;
        Collider2D[] collidersInFar = Physics2D.OverlapCircleAll(origin, farRadius);
        Collider2D[] collidersInClose = Physics2D.OverlapCircleAll(origin, closeRadius);

        Vector2 playerVelocity = PlayerController.player.transform.GetComponent<Rigidbody2D>().velocity;
        Vector2 predictedPlayerPosition = (Vector2)PlayerController.player.transform.position + playerVelocity * predictionTime * predictionFactor;
        Vector2 directionToPredictedPosition = (predictedPlayerPosition - origin).normalized;
        float angle = Mathf.Atan2(directionToPredictedPosition.y, directionToPredictedPosition.x) * Mathf.Rad2Deg + 90f;


        foreach (Collider2D c1 in collidersInFar)
        {
            if (c1.gameObject.CompareTag("Player"))
            {
                //Debug.Log("you in far");
                rb.velocity = directionToPredictedPosition * initialSpeed;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            foreach (Collider2D c2 in collidersInClose)
            {
                if (c2.gameObject.CompareTag("Player"))
                {
                    //Debug.Log("you in close");
                    rb.velocity = directionToPredictedPosition * (initialSpeed * 2.25f);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        // Parameters of all areas (position, rotation, scale)
        Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, 1f, 0.01f));

        // Big Area of player detection and going towards player
        Gizmos.color = new(0f, 1f, 0f, 0.5f); // color of a big area (green, half transparent)
        Gizmos.DrawSphere(transform.position, farRadius);

        // Small Area that makes mob faster towards player
        Gizmos.color = new(1f, 0f, 0f, 0.85f); // color of a small area (red, a little transparent)
        Gizmos.DrawSphere(transform.position, closeRadius); // small area body
    }

    void ChasingWithRaycast()
    {
        Vector2 mobPos = transform.position;
        Vector2 playerPos = player.transform.position;
        //Vector2 playerDir = (playerPos - mobPos).normalized;
        

        for (int i = 0; i < rayCount; i++)
        {
            float angle = Mathf.Lerp(-attackAngle, attackAngle, i / (rayCount - 1f));
            Vector2 direction = Quaternion.Euler(0f, 0f, angle) * -transform.up;

            RaycastHit2D hit = Physics2D.Raycast(mobPos, direction, attackRadius, wallLayer);

            if (hit.collider != null)
            {
                if (hit.collider.name == "Player")
                {
                    Debug.DrawLine(mobPos, hit.point, new (1f, 0f, 0f, 0.75f));
                    //Debug.Log("Hit object: " + hit.collider.gameObject.name);
                    ChasingWithOverlap();
                }
                if (hit.collider.CompareTag("Block"))
                {
                    Debug.DrawLine(mobPos, hit.point, new(1f, 165f/255f, 0f, 0.75f));
                }
                if (hit.collider.CompareTag("Door"))
                {
                    Debug.DrawLine(mobPos, hit.point, new(255f/255f, 222f/255f, 173f/255f, 0.75f));
                }
            }
            else
            {
                Debug.DrawRay(mobPos, direction * attackRadius, new(0f, 1f, 0f, 0.125f/2f));
                //wanderTimer -= Time.deltaTime;

                //if (wanderTimer <= 0f)
                //{
                //    Wandering();
                //    wanderTimer = Random.Range(0.5f, 2.5f);
                //}
            }
        }
    }

    void Wandering()
    {
        Vector2 movementDirection = Random.insideUnitCircle.normalized;
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg + 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.velocity = movementDirection * initialSpeed;
    }

    private void MobDeath()
    {
        GameObject mobDeathParticlesPrefabClone = Instantiate(mobDeathParticlesPrefab, transform.position, Quaternion.identity);
        
        Destroy(mobDeathParticlesPrefabClone, deathParticlesDuration - 2f);

        Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        #region Doors
        if (other.gameObject.CompareTag("Door"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("Process", false);
        }
        #endregion
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        #region Doors
        if (other.gameObject.CompareTag("Door"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("Process", true);
        }
        #endregion
    }
}
