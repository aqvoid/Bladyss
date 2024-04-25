using UnityEngine;

public class SwordSortingOrder : MonoBehaviour
{
    SpriteRenderer swordRenderer;
    ParticleSystem particles;

    void Awake()
    {
        swordRenderer = GetComponentInChildren<SpriteRenderer>();
        particles = GetComponentInChildren<Transform>().GetComponentInChildren<ParticleSystem>();
    }
    
    void Update()
    {
        if (!PauseControls.pauseIsOn)
        {
            SwordSorting();
        }
    }

    void SwordSorting()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = (mousePos - transform.position).normalized;

        transform.right = lookDir;

        Vector3 scale = transform.localScale;
        Vector3 psScale = particles.transform.localScale;
        psScale.x = -1;

        if (lookDir.x < 0)
        {
            scale.y = -1;
            psScale.y = -1;
        }
        else if (lookDir.x > 0)
        {
            scale.y = 1;
            psScale.y = 1;
        }

        particles.transform.localScale = psScale;
        transform.localScale = scale;

        //if looking up = sword is behind player, else = sword is in front of player
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            swordRenderer.sortingOrder = 2;
        }
        else
        {
            swordRenderer.sortingOrder = 4;
        }
    }
}
