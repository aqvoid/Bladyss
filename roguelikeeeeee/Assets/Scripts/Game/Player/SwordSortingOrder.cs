using UnityEngine;
using UnityEngine.UIElements;

public class SwordSortingOrder : MonoBehaviour
{
    SpriteRenderer swordRenderer;
    ParticleSystem particles;

    void Awake()
    {
        swordRenderer = GetComponentInChildren<SpriteRenderer>();
        particles = GetComponentInChildren<Transform>().GetComponentInChildren<ParticleSystem>() ? GetComponentInChildren<Transform>().GetComponentInChildren<ParticleSystem>() : null;
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

        if (particles != null) 
        {
            Vector3 psScale = particles.transform.localScale;
            psScale.x = -1;

            if (lookDir.x < 0)
            {
                psScale.y = -1;
            }
            else if (lookDir.x > 0)
            {
                psScale.y = 1;
            }

            particles.transform.localScale = psScale;
        }

        Vector3 scale = transform.localScale;

        if (lookDir.x < 0)
        {
            scale.y = -1;
        }
        else if (lookDir.x > 0)
        {
            scale.y = 1;
        }
        
        transform.localScale = scale;

        //if looking up = sword is behind player, down = sword is in front of player
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
