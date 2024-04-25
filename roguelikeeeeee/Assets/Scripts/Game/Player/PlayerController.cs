using FirstGearGames.SmoothCameraShaker;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    static public PlayerController player;
    public float health;
    public float initialStaticMoveSpeed;
    public float currentSpeed;
    [HideInInspector] public float moveSpeedWithoutSword;
    [HideInInspector] public float moveSpeedWithSword;
    [HideInInspector] public bool isMoving = false;

    [HideInInspector] public bool isDashing;
    [HideInInspector] public float dashCooldown = 1.5f;
    [HideInInspector] public float dashTime = 1f / 3f;

    private float smoothTimeToStop; // время остановки, из вычисления ниже получается что чем меньше значение тем больше времени
    private float hitTime = 0.0001f;
    private float hitCooldown = 0.85f;
    private Rigidbody2D rb;
    private Vector2 moveDirection; // направление куда двигаться
    private Vector2 currentVelocity; // нынешняя скорость
    private SwordMechanic swordMechanicScript;
    private Animator anim;

    [HideInInspector] public bool swordEquipped = false;
    [HideInInspector] public Image imageDashCooldown;
    [HideInInspector] public Image imageHealthBar;
    [HideInInspector] public GameObject darknessGameObject;
    [HideInInspector] public ShakeData shakeEffect;

    [HideInInspector] public GameObject playerHitParticlesPrefab;
    [HideInInspector] public ParticleSystem playerHitParticleSystemPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
        Death();
        if (isMoving)
        {
            //------------------------- Calculation of the speed (плавный разгон)
            //Параметры: 1. от какой скорости, 2. До какой скорости, 3. За какое время
            currentVelocity = Vector2.Lerp(currentVelocity, moveDirection * initialStaticMoveSpeed, Time.deltaTime * smoothTimeToStop * initialStaticMoveSpeed);
            rb.velocity = currentVelocity;
        }
        else
        {
            //------------------------- Calculation of the speed (плавная остановка)
            //Параметры: 1. от какой скорости, 2. До какой скорости, 3. За какое время
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, Time.deltaTime * smoothTimeToStop * initialStaticMoveSpeed);
            rb.velocity = currentVelocity;
        }
    }

    private void Movement()
    {
        //вычесление движений в зависимости от клавиатуры
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction
        Vector2 movementDirection = new Vector2(horizontal, vertical).normalized;
        currentSpeed = rb.velocity.magnitude;
        smoothTimeToStop = Mathf.Lerp(0.35f, 0.75f, currentSpeed);

        if (horizontal != 0 && movementDirection != Vector2.zero || vertical != 0 && movementDirection != Vector2.zero) // если пользователь нажимает на кнопки движения на клавиатуре(если двигается)
        {
            isMoving = true;
            moveDirection = movementDirection;
        }
        else
        {
            isMoving = false;
        }

        #region Animations

        //if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        //    anim.Play("PlayerIdling");

        //else if (Input.GetKey(KeyCode.W))
        //    anim.Play("PlayerMovingUp");

        //else if (Input.GetKey(KeyCode.A))
        //    anim.Play("PlayerMovingLeft");

        //else if (Input.GetKey(KeyCode.S))
        //    anim.Play("PlayerMovingDown");

        //else if (Input.GetKey(KeyCode.D))
        //    anim.Play("PlayerMovingRight");

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Speed", movementDirection.sqrMagnitude);

        #endregion
    }

    private void Death()
    {
        if (health <= 0)
        {
            //what to do if dead
            //example: run result, floors completed {num}
            //                     mobs killed {other num}
            //                     and more... etc.
            //button to main menu
            //new run button
            SceneManager.LoadScene("Menu");
        }
    }

    public void WeaponInertia()
    {
        Vector2 attackDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        if (swordEquipped)
        {
            swordMechanicScript = GameObject.Find("SwordChildren").GetComponent<SwordMechanic>();
            float inertiaForce = swordMechanicScript.swordWeight + currentSpeed;
            rb.AddForce(attackDir * inertiaForce * 15, ForceMode2D.Force);

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        #region Doors
        if (other.gameObject.CompareTag("Door"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("Process", true);
        }
        #endregion
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        #region Mobs
        if (other.collider.gameObject.CompareTag("Mob"))
        {
            if (other.transform.name.Substring(0, other.transform.name.Length >= 7 ? 7 : 0) == "Regular" && other.gameObject.CompareTag("Mob"))
            {
                if (Time.time > hitTime)
                {
                    health -= 0.25f;
                    initialStaticMoveSpeed = Mathf.LerpUnclamped(0f, 10f, Mathf.Clamp01(health));
                    hitTime = Time.time + hitCooldown;
                    GameObject playerHitParticlesPrefabClone = Instantiate(playerHitParticlesPrefab, transform.position, Quaternion.identity, transform.parent);
                    Destroy(playerHitParticlesPrefabClone, 1.5f);
                }
            }
            if (other.transform.name.Substring(0, other.transform.name.Length >= 12 ? 12 : 0) == "Regular Boss" && other.gameObject.CompareTag("Mob"))
            {
                if (Time.time > hitTime)
                {
                    health -= 0.4f;
                    initialStaticMoveSpeed = Mathf.LerpUnclamped(0f, 10f, Mathf.Clamp01(health));
                    hitTime = Time.time + hitCooldown;
                    GameObject playerHitParticlesPrefabClone = Instantiate(playerHitParticlesPrefab, transform.position, Quaternion.identity, transform.parent);
                    Destroy(playerHitParticlesPrefabClone, 1.5f);
                }
            }
        }
        #endregion

        #region Doors
        if (other.gameObject.CompareTag("Door"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("Process", true);
        }
        #endregion
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        #region Darkness
        if (other.gameObject.CompareTag("Darkness"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("DarknessEnabled", false);
        }
        #endregion
        //Delete Room Mark On Enter
        if (other.gameObject.CompareTag("Room"))
        {
             //Проверяй есть ли у комнаты (дочерний)объект MinimapIcon, если есть то удаляй его
            if (other.gameObject.CompareTag("Room") )
            {

            }

            Destroy(GameObject.Find("MinimapIcon"));

        }
    }
}