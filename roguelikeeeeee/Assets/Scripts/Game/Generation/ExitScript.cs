using UnityEngine;

public class ExitScript : MonoBehaviour
{ 
    private SpriteRenderer exitSpriteRenderer;
    [SerializeField] private Sprite exitClosedSprite;
    [SerializeField] private Sprite exitOpenedSprite;

    private FloorManager floorManagerScript;
    private bool floorIncremented = false;

    void Awake()
    {
        exitSpriteRenderer = GetComponent<SpriteRenderer>();
        floorManagerScript = GameObject.Find("FloorManager").GetComponent<FloorManager>();
    }

    private void FixedUpdate()
    {
        if (!PauseControls.pauseIsOn)
        {
            GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
            GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

            if (mobs.Length == 0 && bosses.Length == 0 && !floorIncremented)
            {
                exitSpriteRenderer.sprite = exitOpenedSprite;
                FloorManager.floorNum++;
                floorIncremented = true;
                //Debug.Log($"Floor Number: {FloorManager.floorNum}");
            }

            if (mobs.Length > 0 && bosses.Length > 0 && floorIncremented)
            {
                exitSpriteRenderer.sprite = exitClosedSprite;
                floorIncremented = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!PauseControls.pauseIsOn)
        {
            //go to the next floor
            //animation of going downstairs and appearing on the next floor
            if (collider.gameObject.CompareTag("Player"))
            {
                if (exitSpriteRenderer.sprite == exitOpenedSprite)
                {
                    if (floorManagerScript != null)
                    {
                        Vector2 playerPos = collider.transform.position;

                        floorManagerScript.GenerateFloor(FloorManager.floorNum, playerPos);
                        PlayerController.player.transform.position = Vector2.zero;
                    }
                }
            }
        }
    }
}
